using Payment.AliPay.Sdk.Model;
using System.Threading.Tasks;

namespace Payment.AliPay.Sdk.Interfaces
{
    public interface IAliPayService
    {
        Task<string> PagePay(PagePayModel payModel);

        Task<string> AppPay(PagePayModel payModel);

        Task<string> JsApiPay(PagePayModel payModel);

    }
}
