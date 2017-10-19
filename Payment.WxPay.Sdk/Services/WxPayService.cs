using Payment.WxPay.Sdk.Business;
using Payment.WxPay.Sdk.Interfaces;
using Payment.WxPay.Sdk.Lib;
using Payment.WxPay.Sdk.Model;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Payment.WxPay.Sdk.Response;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Payment.WxPay.Sdk.Services
{
    public class WxPayService : IWxPayService
    {
        public async Task<string> PagePay(NativeWxPayModel wxPayModel)
        {
            var nativePay = new NativePay();
            //生成扫码支付模式一url
            //string url1 = nativePay.GetPrePayUrl("123456789");
            //生成扫码支付模式二url
            var url2 = nativePay.GetPayUrl(wxPayModel);
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
            return svgImageData.ToString();
        }

        public async Task<string> AppPay(AppPayModel wxPayModel)
        {
            var appPay = new AppPay();
            var result = appPay.Run(wxPayModel);

            return result.ToJson();
        }

        public async Task<string> JsApiPay(JsApiWxPayModel wxPayModel)
        {
            var jsApiPay = new JsApiPay();
            jsApiPay.GetUnifiedOrderResult(wxPayModel);
            var wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数    
            return wxJsApiParam;
        }

        public async Task<string> MwebPay(NativeWxPayModel wxPayModel)
        {
            var mwebPay = new MwebPay();
            var data = mwebPay.GetPayUrl(wxPayModel);
            return data.GetValue("mweb_url").ToString();
        }

        public async Task<WeChatRefundResponse> WeChatRefund(WxRefundModel refundModel)
        {
            var result = WxRefund.Run(refundModel);
            var response = JsonConvert.DeserializeObject<WeChatRefundResponse>(result.ToJson());
            return response;
        }

        public async Task<WeChatRefundQueryResponse> WeChatRefundQuery(WxRefundQueryModel refundQueryModel)
        {
            var result = WxRefundQuery.Run(refundQueryModel);
            var response = JsonConvert.DeserializeObject<WeChatRefundQueryResponse>(result.ToJson());

            return response;
        }

        public async Task<string> WeChatNotify(Stream weChatReturnData)
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
                return res.ToXml();
            }


            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                var res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                return res.ToXml();
            }

            var transactionId = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transactionId))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                var res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                return res.ToXml();
            }
            //查询订单成功
            else
            {
                var res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                return res.ToXml();
            }
        }

        //查询订单
        private bool QueryOrder(string transactionId)
        {
            var req = new WxPayData();
            req.SetValue("transaction_id", transactionId);
            var res = WxPayApi.OrderQuery(req);
            return res.GetValue("return_code").ToString() == "SUCCESS" && res.GetValue("result_code").ToString() == "SUCCESS";
        }
    }
}
