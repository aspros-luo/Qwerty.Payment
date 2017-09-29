using Payment.AliApy.Sdk.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Payment.AliApy.Sdk.Configs;
using Payment.AliApy.Sdk.Infrastructure;
using Payment.AliApy.Sdk.Model;
using Payment.AliApy.Sdk.Util;
using Payment.AliApy.Sdk.ValueObjects;

namespace Payment.AliApy.Sdk.Services
{
    public class AliPayService:IAliPayService
    {

        public async Task<string> Pay(PagePayModel payModel)
        {
            var common=new AliPageModel();
            common.SetBizContent(payModel);
            var parameters= common.GetType().GetProperties().OrderBy(o => o.Name).ToDictionary(item => item.Name, item => item.GetValue(common).ToString());
            var str= BuildParamStr(parameters);
            var sign = GenerateRsaAssist.RasSign(str, Configs.AliPayConfig.PrivateKey, SignType.Rsa2);
            parameters.Add("sign",sign);
            try
            {
               var from=  BuildHtmlRequest(parameters, "post", "post");
                //var response = await HttpUtil.CreatePostHttpResponse("https://openapi.alipaydev.com/gateway.do?charset=utf-8", parameters, Encoding.UTF8);
                //return await response.Content.ReadAsStringAsync();
                return from;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string BuildHtmlRequest(IDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            //待请求参数数组
            IDictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = sParaTemp;

            StringBuilder sbHtml = new StringBuilder();
            //sbHtml.Append("<head><meta http-equiv=\"Content-Type\" content=\"text/html\" charset= \"" + charset + "\" /></head>");

            sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='https://openapi.alipaydev.com/gateway.do?charset=utf-8' method='" + strMethod + "'>");
            ;
            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input  name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            // sbHtml.Append("<input type='submit' value='" + strButtonValue + "'></form></div>");

            //表单实现自动提交
            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }

        public static string BuildParamStr(Dictionary<string, string> param)
        {
            if (param == null || param.Count == 0)
            {
                return "";
            }
            var ascDic = param.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            var sb = new StringBuilder();
            foreach (var item in ascDic)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    sb.Append(item.Key).Append("=").Append(item.Value).Append("&");
                }
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 1);
        }
    }
}
