using Payment.WxPay.Sdk.Business;
using Payment.WxPay.Sdk.Interfaces;
using System;
using System.Threading.Tasks;
using Payment.WxPay.Sdk.Model;
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

        public Task<string> AppPay()
        {
            throw new NotImplementedException();
        }

        public async Task<string> JsApiPay(JsApiWxPayModel wxPayModel)
        {
            var jsApiPay = new JsApiPay();
            var unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(wxPayModel);
            var wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数    
            return wxJsApiParam;
        }
    }
}
