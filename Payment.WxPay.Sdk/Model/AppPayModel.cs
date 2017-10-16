using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.WxPay.Sdk.Model
{
    public class AppPayModel
    {
        public string Body { get; set; }
        public string OutTradeNo { get; set; }
        public int TotalFee { get; set; }
    }
}
