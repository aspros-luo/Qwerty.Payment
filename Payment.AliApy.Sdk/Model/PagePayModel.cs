using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Payment.AliApy.Sdk.Model
{
    public class PagePayModel
    {
        public string out_trade_no { get; set; }
        public string product_code { get; private set; } = "FAST_INSTANT_TRADE_PAY";
        public string total_amount { get; set; }
        public string subject { get; set; }
     
    }
}
