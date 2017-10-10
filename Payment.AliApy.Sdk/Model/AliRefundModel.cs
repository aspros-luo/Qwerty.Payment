using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.AliPay.Sdk.Model
{
    public class AliRefundModel
    {
        public string out_trade_no { get; set; }
        //public string trade_no { get; set; }
        public decimal refund_amount { get; set; }
    }
}
