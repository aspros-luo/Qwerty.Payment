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

namespace Payment.AliPay.Sdk.Services
{
    public class AliPayService : IAliPayService
    {
        public async Task<string> PagePay(PagePayModel payModel)
        {
            payModel.SetProductCode("FAST_INSTANT_TRADE_PAY");

            var common = new AliPageModel();
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
        

        public async Task<string> AppPay(PagePayModel payModel)
        {
            payModel.SetProductCode("QUICK_MSECURITY_PAY");
            var common = new AliPageModel();
            common.SetMethod("alipay.trade.app.pay");
            common.SetBizContent(payModel);
            var parameters = common.GetType().GetProperties().OrderBy(o => o.Name).ToDictionary(item => item.Name, item => item.GetValue(common).ToString());
            var str = BuildData.BuildParamStr(parameters);
            var sign = GenerateRsaAssist.RasSign(str, AliPayConfig.PrivateKey, SignType.Rsa2);
            sign= UrlEncoder.Default.Encode(sign);
            
            return UrlEncoder.Default.Encode(str)+$"&sign={sign}";
        }

        public async Task<string> JsApiPay(PagePayModel payModel)
        {
            payModel.SetProductCode("QUICK_WAP_WAY");
            var common = new AliPageModel();
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
    }
}
