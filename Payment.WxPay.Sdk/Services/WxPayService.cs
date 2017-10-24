using Newtonsoft.Json;
using Payment.WxPay.Sdk.Business;
using Payment.WxPay.Sdk.Interfaces;
using Payment.WxPay.Sdk.Lib;
using Payment.WxPay.Sdk.Request;
using Payment.WxPay.Sdk.Response;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Payment.WxPay.Sdk.Services
{
    public class WxPayService : IWxPayService
    {
        public async Task<Tuple<bool, string>> PagePay(string body, string outTradeNo, int totalFee, string productId)
        {
            try
            {
                var nativePay = new NativePay();
                //生成扫码支付模式二url
                var url2 = await nativePay.GetPayUrl(body, outTradeNo, totalFee, productId);
                //将url生成二维码图片
                var writerSvg = new BarcodeWriterSvg
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions
                    {
                        ErrorCorrection = ErrorCorrectionLevel.H
                    }
                };
                var svgImageData = writerSvg.Write(url2);
                return svgImageData == null
                    ? new Tuple<bool, string>(false, "返回数据为空！")
                    : new Tuple<bool, string>(true, svgImageData.ToString());
            }
            catch (Exception e)
            {
                return new Tuple<bool, string>(false, e.Message);
            }
        }

        public async Task<Tuple<bool, string>> AppPay(string body, string outTradeNo, int totalFee)
        {
            try
            {
                var appPay = new AppPay();
                var result = await appPay.Run(body, outTradeNo, totalFee);
                return result == null
                    ? new Tuple<bool, string>(false, "返回数据为空！")
                    : new Tuple<bool, string>(true, result.ToJson());
            }
            catch (Exception e)
            {
                return new Tuple<bool, string>(false, e.Message);
            }
        }

        public async Task<Tuple<bool, string>> JsApiPay(string body, string outTradeNo, int totalFee, string openId)
        {
            try
            {
                var jsApiPay = new JsApiPay();
                await jsApiPay.GetUnifiedOrderResult(body, outTradeNo, totalFee, openId);
                var wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数    
                return wxJsApiParam == null
                    ? new Tuple<bool, string>(false, "返回数据为空！")
                    : new Tuple<bool, string>(true, wxJsApiParam);
            }
            catch (Exception e)
            {
                return new Tuple<bool, string>(false, e.Message);
            }
        }

        public async Task<Tuple<bool, string>> MwebPay(string body, string outTradeNo, int totalFee, string ip)
        {
            try
            {
                var mwebPay = new MwebPay();
                var data = await mwebPay.GetPayUrl(body, outTradeNo, totalFee, ip);
                return data == null
                    ? new Tuple<bool, string>(false, "返回数据为空！")
                    : new Tuple<bool, string>(true, data.GetValue("mweb_url").ToString());
            }
            catch (Exception e)
            {
                return new Tuple<bool, string>(false, e.Message);
            }
        }

        public async Task<WeChatRefundResponse> WeChatRefund(string transactionId, string outTradeNo, int totalFee, int refundFee, string outRefundNo)
        {
            try
            {
                var result = await WxRefund.Run(transactionId, outTradeNo, totalFee, refundFee, outRefundNo);
                var response = JsonConvert.DeserializeObject<WeChatRefundResponse>(result);
                return response;
            }
            catch (Exception e)
            {
                return new WeChatRefundResponse { result_code = "Fail", return_msg = e.Message };
            }
        }

        public async Task<WeChatRefundQueryResponse> WeChatRefundQuery(string refundId, string outRefundNo, string transactionId, string outTradeNo)
        {
            try
            {
                var result = await WxRefundQuery.Run(refundId, outRefundNo, transactionId, outTradeNo);
                var response = JsonConvert.DeserializeObject<WeChatRefundQueryResponse>(result);
                return response;
            }
            catch (Exception e)
            {
                return new WeChatRefundQueryResponse { result_code = "Fail", return_msg = e.Message };
            }
        }

        public async Task<WeChatNotifyRequest> WeChatNotify(Stream weChatReturnData)
        {
            //接收从微信后台POST过来的数据
            var s = weChatReturnData;
            int count;
            var buffer = new byte[1024];
            var builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Dispose();
            var notifyData = new WxPayData();
            try
            {
                notifyData.FromXml(builder.ToString());
            }
            catch (Exception e)
            {
                var res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", e.Message);
                return new WeChatNotifyRequest { IsVerify = false, PayNo = "", TradeIds = "", PayTime = "", Sign = "", Content = res.ToXml() };
            }

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                var res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                return new WeChatNotifyRequest { IsVerify = false, PayNo = "", TradeIds = "", PayTime = "", Sign = "", Content = res.ToXml() };
            }

            var transactionId = notifyData.GetValue("transaction_id").ToString();
            var tradeIds = notifyData.GetValue("out_trade_no").ToString();
            var payTime = notifyData.GetValue("time_end").ToString();

            //查询订单，判断订单真实性
            if (!await QueryOrder(transactionId))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                var res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                return new WeChatNotifyRequest { IsVerify = false, PayNo = "", TradeIds = "", PayTime = "", Sign = "", Content = res.ToXml() };
            }
            //查询订单成功
            else
            {
                var res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                return new WeChatNotifyRequest { IsVerify = true, PayNo = transactionId, TradeIds = tradeIds, PayTime = payTime, Sign = "", Content = res.ToXml() };
            }
        }
        //查询订单
        private static async Task<bool> QueryOrder(string transactionId)
        {
            var req = new WxPayData();
            req.SetValue("transaction_id", transactionId);
            var res = await WxPayApi.OrderQuery(req);
            return res.GetValue("return_code").ToString() == "SUCCESS" && res.GetValue("result_code").ToString() == "SUCCESS";
        }
    }
}
