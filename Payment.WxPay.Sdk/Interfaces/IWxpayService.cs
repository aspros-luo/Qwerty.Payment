using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Payment.WxPay.Sdk.Model;

namespace Payment.WxPay.Sdk.Interfaces
{
    public interface IWxPayService
    {
        Task<string> PagePay(NativeWxPayModel wxPayModel);

        Task<string> AppPay();

        Task<string> JsApiPay();

        //Task<AliRefundResponse> AliRefund(AliRefundModel refundModel);

        //Task<AliRefundQueryResponse> AliRefundQuery(AliRefundQueryModel refundQueryModel);

        //Task<string> AliNotify(Dictionary<string, string> alipayReturnData);
    }
}
