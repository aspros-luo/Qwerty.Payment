namespace Payment.AliPay.Sdk.Response
{
    public class AliRefundResponse
    {
        public AlipayRefundResponse alipay_trade_refund_response { get; set; }
        public string sign { get; set; }
    }
    public class AlipayRefundResponse
    {
        /// <summary>
        /// 本笔退款对应的退款请求号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 网关返回码描述
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 用户的登录id
        /// </summary>
        public string buyer_logon_id { get; set; }
        /// <summary>
        /// 买家在支付宝的用户id
        /// </summary>
        public string buyer_user_id { get; set; }
        /// <summary>
        /// 本次退款是否发生了资金变化
        /// </summary>
        public string fund_change { get; set; }
        /// <summary>
        /// 退款支付时间
        /// </summary>
        public string gmt_refund_pay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string open_id { get; set; }
        /// <summary>
        /// 创建交易传入的商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 退款总金额
        /// </summary>
        public string refund_fee { get; set; }
        /// <summary>
        /// 实际退款金额
        /// </summary>
        public string send_back_fee { get; set; }
        /// <summary>
        /// 支付宝交易号
        /// </summary>
        public string trade_no { get; set; }
    }
}
