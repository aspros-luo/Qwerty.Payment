namespace Payment.AliPay.Sdk.Model
{
    public class AliPayModel
    {
        /// <summary>
        /// 商户交易订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public string product_code { get; private set; } = "FAST_INSTANT_TRADE_PAY";
        /// <summary>
        /// 支付金额
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public string timeout_express { get; set; } = "30m";
        /// <summary>
        /// 设置支付方式
        /// </summary>
        /// <param name="code"></param>
        internal void SetProductCode(string code)
        {
            product_code = code;
        }
    }
}
