using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Payment.WxPay.Sdk.Interfaces
{
    public interface IWxPayService
    {
        Task<string> PagePay();

        Task<string> AppPay();

        Task<string> JsApiPay();

        //Task<AliRefundResponse> AliRefund(AliRefundModel refundModel);

        //Task<AliRefundQueryResponse> AliRefundQuery(AliRefundQueryModel refundQueryModel);

        //Task<string> AliNotify(Dictionary<string, string> alipayReturnData);
    }
}
