using Newtonsoft.Json;
using Payment.AliPay.Sdk.Configs;
using Payment.AliPay.Sdk.Infrastructure;
using Payment.AliPay.Sdk.Interfaces;
using Payment.AliPay.Sdk.Model;
using Payment.AliPay.Sdk.Request;
using Payment.AliPay.Sdk.Response;
using Payment.AliPay.Sdk.Util;
using Payment.AliPay.Sdk.ValueObjects;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Payment.AliPay.Sdk.Services
{
    public class AliPayService : IAliPayService
    {
        public AliPayRequest NativePay(AliPayModel payModel)
        {
            payModel.SetProductCode("FAST_INSTANT_TRADE_PAY");

            var common = new AliPayCommonModel();
            common.SetMethod("alipay.trade.page.pay");
            common.SetBizContent(payModel);

            var parameters = common.GetType().GetProperties().OrderBy(o => o.Name).ToDictionary(item => item.Name, item => item.GetValue(common).ToString());
            var str = BuildData.BuildParamStr(parameters);

            var sign = GenerateRsaAssist.RasSign(str, AliPayConfig.PrivateKey, SignType.Rsa2);
            parameters.Add("sign", sign);

            try
            {
                var from = BuildData.BuildHtmlRequest(parameters, "post", "post");
                return new AliPayRequest { IsSuccess = true, PreSign = str, Sign = sign, Result = from };
            }
            catch (Exception e)
            {
                return new AliPayRequest { IsSuccess = false, PreSign = str, Sign = sign, Result = e.Message };
            }
        }

        public AliPayRequest AppPay(string preSign)
        {
            try
            {
                //payModel.SetProductCode("QUICK_MSECURITY_PAY");
                //var common = new AliPayCommonModel();
                //common.SetMethod("alipay.trade.app.pay");
                //common.SetBizContent(payModel);
                //var parameters = common.GetType().GetProperties().OrderBy(o => o.Name).ToDictionary(item => item.Name, item => item.GetValue(common).ToString());
                //var str = BuildData.BuildParamStr(parameters);
                var sign = GenerateRsaAssist.RasSign(preSign, AliPayConfig.PrivateKey, SignType.Rsa2);
                //return UrlEncoder.Default.Encode(str)+$"&sign={sign}";
                sign = UrlEncoder.Default.Encode(sign);
                return new AliPayRequest { IsSuccess = true, PreSign = preSign, Sign = sign, Result = sign };
            }
            catch (Exception e)
            {
                return new AliPayRequest { IsSuccess = false, PreSign = preSign, Sign = "", Result = e.Message };
            }

        }

        public AliPayRequest JsApiPay(AliPayModel payModel)
        {
            payModel.SetProductCode("QUICK_WAP_WAY");

            var common = new AliPayCommonModel();
            common.SetMethod("alipay.trade.wap.pay");
            common.SetBizContent(payModel);

            var parameters = common.GetType().GetProperties().OrderBy(o => o.Name).ToDictionary(item => item.Name, item => item.GetValue(common).ToString());
            var str = BuildData.BuildParamStr(parameters);

            var sign = GenerateRsaAssist.RasSign(str, AliPayConfig.PrivateKey, SignType.Rsa2);
            parameters.Add("sign", sign);

            try
            {
                var from = BuildData.BuildHtmlRequest(parameters, "post", "post");
                return new AliPayRequest { IsSuccess = true, PreSign = str, Sign = sign, Result = from };
            }
            catch (Exception e)
            {
                return new AliPayRequest { IsSuccess = false, PreSign = str, Sign = sign, Result = e.Message };
            }
        }

        public async Task<AliRefundResponse> AliRefund(AliRefundModel refundModel)
        {
            var common = new AliPayCommonModel();
            common.SetMethod("alipay.trade.refund");
            common.SetBizContent(refundModel);

            var parameters = common.GetType().GetProperties().OrderBy(o => o.Name).ToDictionary(item => item.Name, item => item.GetValue(common).ToString());
            var str = BuildData.BuildParamStr(parameters);

            var sign = GenerateRsaAssist.RasSign(str, AliPayConfig.PrivateKey, SignType.Rsa2);
            parameters.Add("sign", sign);

            var response = await HttpUtil.CreatePostHttpResponse(AliPayConfig.Gateway, parameters);
            var result = await response.Content.ReadAsStringAsync();

            var jsonResult = JsonConvert.DeserializeObject<AliRefundResponse>(result);
            return jsonResult;
        }

        public async Task<AliRefundQueryResponse> AliRefundQuery(AliRefundQueryModel refundQueryModel)
        {
            var common = new AliPayCommonModel();
            common.SetMethod("alipay.trade.fastpay.refund.query");
            common.SetBizContent(refundQueryModel);
            var parameters = common.GetType().GetProperties().OrderBy(o => o.Name).ToDictionary(item => item.Name, item => item.GetValue(common).ToString());
            var str = BuildData.BuildParamStr(parameters);
            var sign = GenerateRsaAssist.RasSign(str, AliPayConfig.PrivateKey, SignType.Rsa2);
            parameters.Add("sign", sign);
            var response = await HttpUtil.CreatePostHttpResponse(AliPayConfig.Gateway, parameters);
            var result = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<AliRefundQueryResponse>(result);
            return jsonResult;
        }

        public AliNotifyRequest AliNotify(Stream aliReturnData)
        {
            try
            {
                //获取回调参数
                var s = aliReturnData;
                int count;
                var buffer = new byte[1024];
                var builder = new StringBuilder();
                while ((count = s.Read(buffer, 0, 1024)) > 0)
                {
                    builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
                s.Flush();
                s.Dispose();
                //request 接收的字符串含有urlencode，这里需要decode一下
                var alipayReturnData = builder.ToString().Split('&').ToDictionary(a => a.Split('=')[0], a => System.Net.WebUtility.UrlDecode(a.Split('=')[1]));
                //获取sign
                var sign = alipayReturnData["sign"];
                //去除sign及signtype
                alipayReturnData.Remove("sign");
                alipayReturnData.Remove("sign_type");
                //获取支付宝订单号及商户交易订单号
                var tradeNo = alipayReturnData["trade_no"];
                var tradeIds = alipayReturnData["out_trade_no"];

                var dic = alipayReturnData.ToDictionary(d => d.Key, d => d.Value);

                var preSign = BuildData.BuildParamStr(dic);
                //验签
                var result = GenerateRsaAssist.VerifySign(preSign, AliPayConfig.AliPublicKey, sign, SignType.Rsa2);

                return result
                    ?
                    new AliNotifyRequest { IsVerify = true, PayNo = tradeNo, TradeIds = tradeIds, PayTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Sign = sign, Content = preSign }
                    :
                    new AliNotifyRequest { IsVerify = false, PayNo = tradeNo, TradeIds = "", PayTime = "", Sign = sign, Content = preSign };
            }
            catch (Exception e)
            {
                return new AliNotifyRequest { IsVerify = false, PayNo = "", TradeIds = "", PayTime = "", Sign = "", Content = e.Message };
            }
        }
    }
}
