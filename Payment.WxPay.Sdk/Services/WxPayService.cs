using Payment.WxPay.Sdk.Business;
using Payment.WxPay.Sdk.Interfaces;
using Payment.WxPay.Sdk.Model;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Payment.WxPay.Sdk.Services
{
    public class WxPayService:IWxPayService
    {
        public async Task<string> PagePay(NativeWxPayModel wxPayModel)
        {
            var nativePay = new NativePay();
            //生成扫码支付模式一url
            //string url1 = nativePay.GetPrePayUrl("123456789");
            //生成扫码支付模式二url
            var url2 = nativePay.GetPayUrl(wxPayModel);
            //将url生成二维码图片
            var writerSvg = new BarcodeWriterSvg
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    ErrorCorrection = ErrorCorrectionLevel.H
                }
            };
            var svgImageData = writerSvg.Write(url2);
            return svgImageData.ToString();
        }

        public async Task<string> AppPay(AppPayModel wxPayModel)
        {
            var appPay=new AppPay();
            var result= appPay.Run(wxPayModel);
            
            return result.ToJson();
        }

        public async Task<string> JsApiPay(JsApiWxPayModel wxPayModel)
        {
            var jsApiPay = new JsApiPay();
            jsApiPay.GetUnifiedOrderResult(wxPayModel);
            var wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数    
            return wxJsApiParam;
        }

        public async Task<string> MwebPay(NativeWxPayModel wxPayModel)
        {
            var mwebPay=new MwebPay();
            var data= mwebPay.GetPayUrl(wxPayModel);
            return data.GetValue("mweb_url").ToString();
        }

        public async Task<string> AliRefund(WxRefundModel refundModel)
        {
            var result= WxRefund.Run(refundModel);
            return result;
        }

        public async Task<string> AliRefundQuery(WxRefundQueryModel refundQueryModel)
        {
            var result = WxRefundQuery.Run(refundQueryModel);
            return result;
        }
    }
}
