using Payment.WxPay.Sdk.Model;
using System.IO;
using System.Threading.Tasks;

namespace Payment.WxPay.Sdk.Interfaces
{
    public interface IWxPayService
    {
        Task<string> PagePay(NativeWxPayModel wxPayModel);

        Task<string> AppPay(AppPayModel wxPayModel);

        Task<string> JsApiPay(JsApiWxPayModel wxPayModel);

        Task<string> MwebPay(NativeWxPayModel wxPayModel);

        Task<string> WeChatRefund(WxRefundModel refundModel);

        Task<string> WeChatRefundQuery(WxRefundQueryModel refundQueryModel);

        Task<string> WeChatNotify(Stream weChatReturnData);
    }
}
