using Payment.AliPay.Sdk.Configs;
using System;
using System.Linq;
using System.Reflection;

namespace Payment.AliPay.Sdk.Model
{
    internal class AliPayCommonModel
    {
        internal string app_id { get; private set; } = "2017101909381258";
        internal string method { get; private set; } = "alipay.trade.page.pay";
        internal string format { get; private set; } = "JSON";
        internal string return_url { get; private set; } = AliPayConfig.ReturnUrl;
        internal string charset { get; private set; } = "utf-8";
        internal string sign_type { get; private set; } = "RSA2";
        internal string timestamp { get; private set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        internal string version { get; private set; } = "1.0";
        internal string notify_url { get;private set; }= AliPayConfig.NotifyUrl;
        internal string biz_content { get; private set; }
        /// <summary>
        /// 设置支付方式
        /// </summary>
        /// <param name="payMethod"></param>
        internal void SetMethod(string payMethod)
        {
            method = payMethod;
        }
        /// <summary>
        /// 设置支付主题内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pay"></param>
        internal void SetBizContent<T>(T pay)
        {
            var str = pay.GetType().GetProperties().OrderBy(o=>o.Name).Aggregate("", (current, item) => current + $"\"{item.Name}\":\"{item.GetValue(pay)}\",");
            biz_content ="{"+ str.Substring(0,str.Length-1)+"}";
        }

        //public void SetBizContent(AliRefundModel refund)
        //{
        //    var str = refund.GetType().GetProperties().OrderBy(o => o.Name).Aggregate("", (current, item) => current + $"\"{item.Name}\":\"{item.GetValue(refund)}\",");
        //    biz_content = "{" + str.Substring(0, str.Length - 1) + "}";
        //}

        //public void SetBizContent(AliRefundQueryModel refundQuery)
        //{
        //    var str = refundQuery.GetType().GetProperties().OrderBy(o => o.Name).Aggregate("", (current, item) => current + $"\"{item.Name}\":\"{item.GetValue(refundQuery)}\",");
        //    biz_content = "{" + str.Substring(0, str.Length - 1) + "}";
        //}

    }
}
