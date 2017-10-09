using Payment.AliPay.Sdk.Model;
using System.Threading.Tasks;

namespace Payment.AliPay.Sdk.Interfaces
{
    public interface IAliPayService
    {
        Task<string> PagePay(AliPayModel payModel);

        Task<string> AppPay(AliPayModel payModel);

        Task<string> JsApiPay(AliPayModel payModel);

    }
}
