namespace Payment.AliPay.Sdk.Model
{
    /// <summary>
    /// 用于生成预支付单的
    /// </summary>
    public class AliPrePayModel
    {
        /// <summary>
        /// 商户交易订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public string discountable_amount { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public string timeout_express { get; private set; } = "30m";
    }
}
