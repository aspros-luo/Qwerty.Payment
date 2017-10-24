using System;
using OSS.Http;
using OSS.Http.Mos;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Payment.WxPay.Sdk.Lib
{
    internal class HttpService
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        public static async Task<string> Post(string xml, string url, bool isUseCert, int timeout)
        {
            if (isUseCert)
            {
                //商户私钥证书，用于对请求报文进行签名
                var tempSignCert = new X509Certificate2(WxPayConfig.SSLCERT_PATH, WxPayConfig.SSLCERT_PASSWORD);

                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    SslProtocols = SslProtocols.Tls12
                };
                handler.ClientCertificates.Add(tempSignCert);
                handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var client = new HttpClient(handler))
                {
                    var httpContent = new StringContent(xml, Encoding.UTF8, "application/xml");
                    var response = await client.PostAsync(url, httpContent);
                    return await response.Content.ReadAsStringAsync();
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(xml, Encoding.UTF8, "application/xml");
                    var response = await client.PostAsync(url, httpContent);
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
