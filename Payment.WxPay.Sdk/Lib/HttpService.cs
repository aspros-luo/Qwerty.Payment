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

        public static string Post(string xml, string url, bool isUseCert, int timeout)
        {
            if (isUseCert)
            {
                var str = @"\Cert\apiclient_cert.p12";
                //商户私钥证书，用于对请求报文进行签名
                var tempSignCert = new X509Certificate2(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")) + str, "password");

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
                    var response =  client.PostAsync(url, httpContent).Result;
                    return  response.Content.ReadAsStringAsync().Result;
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(xml, Encoding.UTF8, "application/xml");
                    var response =  client.PostAsync(url, httpContent).Result;
                    return  response.Content.ReadAsStringAsync().Result;
                }
            }
            //System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
            //using (var client = new HttpClient())
            //{
            //    var req = new OsHttpRequest
            //    {
            //        HttpMothed = HttpMothed.POST,
            //        AddressUrl = url,
            //        CustomBody = xml
            //    };
            //    var resp = req.RestSend(client).Result;
            //    var result = resp.Content.ReadAsStringAsync().Result;
            //    return result;
            //}
        }

        public static async Task<string> PostXml(string xml, string url, bool isUserCert, int timeout)
        {
            if (isUserCert)
            {
                var str = @"\Cert\apiclient_cert.p12";
                //商户私钥证书，用于对请求报文进行签名
                var tempSignCert = new X509Certificate2(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")) + str, "1340703201");

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

        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static string Get(string url)
        {
            using (var client = new HttpClient())
            {
                var req = new OsHttpRequest
                {
                    HttpMothed = HttpMothed.GET,
                    AddressUrl = url
                };
                var resp = req.RestSend(client).Result;
                var result = resp.Content.ReadAsStringAsync().Result;

                return result;
            }
        }
    }
}
