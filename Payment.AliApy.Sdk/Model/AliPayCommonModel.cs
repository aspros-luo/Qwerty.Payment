using Payment.AliPay.Sdk.Configs;
using System;
using System.Linq;
using System.Reflection;

namespace Payment.AliPay.Sdk.Model
{
    internal class AliPayCommonModel
    {
        public string app_id { get; private set; } = AliPayConfig.AppId;
        public string method { get; private set; }
        public string format { get; private set; } = "JSON";
        public string return_url { get; private set; } = AliPayConfig.ReturnUrl;
        public string charset { get; private set; } = "utf-8";
        public string sign_type { get; private set; } = "RSA2";
        public string timestamp { get; private set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        public string version { get; private set; } = "1.0";
        public string notify_url { get;private set; }= AliPayConfig.NotifyUrl;
        public string biz_content { get; private set; }
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
    }
}
