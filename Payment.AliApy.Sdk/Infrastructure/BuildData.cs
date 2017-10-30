using Payment.AliPay.Sdk.Configs;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payment.AliPay.Sdk.Infrastructure
{
    internal static class BuildData
    {
        public static string BuildHtmlRequest(IDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            //待请求参数数组
            var dicPara = sParaTemp;
            var sbHtml = new StringBuilder();
            //sbHtml.Append("<head><meta http-equiv=\"Content-Type\" content=\"text/html\" charset= \"" + charset + "\" /></head>");
            sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='"+AliPayConfig.Gateway+"?charset=utf-8' method='" + strMethod + "'>");
            foreach (var temp in dicPara)
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
