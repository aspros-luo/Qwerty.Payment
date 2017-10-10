using Payment.AliPay.Sdk.Configs;
using Payment.AliPay.Sdk.Infrastructure;
using Payment.AliPay.Sdk.Interfaces;
using Payment.AliPay.Sdk.Model;
using Payment.AliPay.Sdk.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Payment.AliPay.Sdk.Response;

namespace Payment.AliPay.Sdk.Services
{
    public class AliPayService : IAliPayService
    {
        public async Task<string> PagePay(AliPayModel payModel)
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
                return from;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        

        public async Task<string> AppPay(AliPayModel payModel)
        {
            payModel.SetProductCode("QUICK_MSECURITY_PAY");
            var common = new AliPayCommonModel();
            common.SetMethod("alipay.trade.app.pay");
            common.SetBizContent(payModel);
            var parameters = common.GetType().GetProperties().OrderBy(o => o.Name).ToDictionary(item => item.Name, item => item.GetValue(common).ToString());
            var str = BuildData.BuildParamStr(parameters);
            var sign = GenerateRsaAssist.RasSign(str, AliPayConfig.PrivateKey, SignType.Rsa2);
            sign= UrlEncoder.Default.Encode(sign);
            
            return UrlEncoder.Default.Encode(str)+$"&sign={sign}";
        }

        public async Task<string> JsApiPay(AliPayModel payModel)
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
                return from;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
            var response=  await Util.HttpUtil.CreatePostHttpResponse(AliPayConfig.Gateway, parameters);
            var result= await response.Content.ReadAsStringAsync();
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
            var response = await Util.HttpUtil.CreatePostHttpResponse(AliPayConfig.Gateway, parameters);
            var result = await response.Content.ReadAsStringAsync();
            var jsonResult= JsonConvert.DeserializeObject<AliRefundQueryResponse>(result);
            return jsonResult;
        }

        public async Task<string> AliNotify(Dictionary<string, string> alipayReturnData)
        {
            try
            {
                //var alipayReturnData = new Dictionary<string, string>();
                //var keys = new string[Request.Form.Keys.Count];
                //Request.Form.Keys.CopyTo(keys, 0);
                //for (var i = 0; i < Request.Form.Count; i++)
                //{
                //    Request.Form.TryGetValue(keys[i], out StringValues value);
                //    alipayReturnData.Add(keys[i], value.ToString());
                //}
                var sign = alipayReturnData["sign"];
                alipayReturnData.Remove("sign");
                alipayReturnData.Remove("sign_type");

                var dic = alipayReturnData.ToDictionary(d => d.Key, d => d.Value);
               
                var preSign = BuildData.BuildParamStr(dic); 

                if (GenerateRsaAssist.VerifySign(preSign, AliPayConfig.AliPublicKey, sign, SignType.Rsa2))
                {
                    //1.验证是否是平台订单
                    var tradeIds = alipayReturnData["out_trade_no"].Split(',').Select(tradeId => Convert.ToInt64(tradeId)).ToList();
                    //2.验证金额是否正确
                    var totalAmount = alipayReturnData["total_amount"];
                    //3.验证sellerId是否是该交易的操作方
                    var sellerId = alipayReturnData["seller_id"];
                    //4.验证appId是否是商户本身
                    var appId = alipayReturnData["app_id"];
                    var tradeNo = alipayReturnData["trade_no"];
                    var gmtPayment = Convert.ToDateTime(alipayReturnData["gmt_create"]);
                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
