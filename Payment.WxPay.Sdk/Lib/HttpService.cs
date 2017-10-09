using OSS.Http;
using OSS.Http.Mos;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Payment.WxPay.Sdk.Lib
{
    public class HttpService
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        public static string Post(string xml, string url, bool isUseCert, int timeout)
        {

            //System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
            using (var client = new HttpClient())
            {
                var req = new OsHttpRequest
                {
                    HttpMothed = HttpMothed.POST,
                    AddressUrl = url,
                    CustomBody = xml
                };
                var resp = req.RestSend(client).Result;
                var result = resp.Content.ReadAsStringAsync().Result;
                return result;
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
