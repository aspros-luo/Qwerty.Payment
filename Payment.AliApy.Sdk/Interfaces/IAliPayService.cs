using System.Collections.Generic;
using Payment.AliPay.Sdk.Model;
using System.Threading.Tasks;
using Payment.AliPay.Sdk.Response;

namespace Payment.AliPay.Sdk.Interfaces
{
    public interface IAliPayService
    {
        Task<string> PagePay(AliPayModel payModel);

        Task<string> AppPay(string signParams);

        Task<string> JsApiPay(AliPayModel payModel);

        Task<AliRefundResponse> AliRefund(AliRefundModel refundModel);

        Task<AliRefundQueryResponse> AliRefundQuery(AliRefundQueryModel refundQueryModel);

        Task<string> AliNotify(Dictionary<string,string> alipayReturnData);
    }
}
