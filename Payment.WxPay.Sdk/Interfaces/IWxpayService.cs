using Payment.WxPay.Sdk.Model;
using System.Threading.Tasks;

namespace Payment.WxPay.Sdk.Interfaces
{
    public interface IWxPayService
    {
        Task<string> PagePay(NativeWxPayModel wxPayModel);

        Task<string> AppPay();

        Task<string> JsApiPay(JsApiWxPayModel wxPayModel);

        Task<string> MwebPay(NativeWxPayModel wxPayModel);

        Task<string> AliRefund(WxRefundModel refundModel);

        Task<string> AliRefundQuery(WxRefundQueryModel refundQueryModel);

        //Task<string> AliNotify(Dictionary<string, string> alipayReturnData);
    }
}
