using System.Threading.Tasks;
using Payment.AliApy.Sdk.Model;

namespace Payment.AliApy.Sdk.Interfaces
{
    public interface IAliPayService
    {
        Task<string> Pay(PagePayModel payModel);
    }
}
