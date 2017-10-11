using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.WxPay.Sdk.Model
{
    /// <summary>
    /// 微信jsapi支付模型
    /// </summary>
    public class JsApiWxPayModel
    {
        /// <summary>
        /// 用户的openid
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public int TotalFee { get; set; }
    }
}
