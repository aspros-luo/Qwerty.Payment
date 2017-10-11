using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.WxPay.Sdk.Model
{
    /// <summary>
    /// 微信扫码支付模型
    /// </summary>
    public class NativeWxPayModel
    {
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
        /// <summary>
        /// trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义
        /// </summary>
        public string ProductId { get; set; }
    }
}
