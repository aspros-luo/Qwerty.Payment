using System;
using System.Linq;
using System.Reflection;
using Payment.AliPay.Sdk.Configs;

namespace Payment.AliPay.Sdk.Model
{
    public class AliPayCommonModel
    {
        public string app_id { get; private set; } = "2017101909381258";
        public string method { get; private set; } = "alipay.trade.page.pay";
        public string format { get; private set; } = "JSON";
        public string return_url { get; private set; } = AliPayConfig.NotifyUrl;
        public string charset { get; private set; } = "utf-8";
        public string sign_type { get; private set; } = "RSA2";
        public string timestamp { get; private set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        public string version { get; private set; } = "1.0";
        public string notify_url { get;private set; }= AliPayConfig.NotifyUrl;
        public string biz_content { get; private set; }

        public void SetMethod(string payMethod)
        {
            method = payMethod;
        }

        public void SetBizContent(AliPayModel pay)
        {
            var str = pay.GetType().GetProperties().OrderBy(o=>o.Name).Aggregate("", (current, item) => current + $"\"{item.Name}\":\"{item.GetValue(pay)}\",");
            biz_content ="{"+ str.Substring(0,str.Length-1)+"}";
        }

        public void SetBizContent(AliRefundModel refund)
        {
            var str = refund.GetType().GetProperties().OrderBy(o => o.Name).Aggregate("", (current, item) => current + $"\"{item.Name}\":\"{item.GetValue(refund)}\",");
            biz_content = "{" + str.Substring(0, str.Length - 1) + "}";
        }

        public void SetBizContent(AliRefundQueryModel refundQuery)
        {
            var str = refundQuery.GetType().GetProperties().OrderBy(o => o.Name).Aggregate("", (current, item) => current + $"\"{item.Name}\":\"{item.GetValue(refundQuery)}\",");
            biz_content = "{" + str.Substring(0, str.Length - 1) + "}";
        }

        public void SetReturn(string url)
        {
            return_url = url;
        }

        public void SetNotifyUrl(string url)
        {
            notify_url = url;
        }
    }
}
