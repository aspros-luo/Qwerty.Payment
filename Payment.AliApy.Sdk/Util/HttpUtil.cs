using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payment.AliPay.Sdk.Util
{
    public class HttpUtil
    {
        //private static readonly string _defaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        //private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        //{
        //    return true; //总是接受     
        //}

        public static async Task<HttpResponseMessage> CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Encoding charset)
        {
            var listKeyValues = parameters.Keys.Select(key => new KeyValuePair<string, string>(key, parameters[key])).ToList();
            using (var client = new HttpClient())
            {
                var httpContent = new FormUrlEncodedContent(listKeyValues);
                var response = await client.PostAsync(url, httpContent);
                return response;
            }
        }

        //public string DoPost(string url, IDictionary<string, string> parameters, string charset)
        //{
        //    var req = GetWebRequest(url, "POST");
        //    req.ContentType = "application/x-www-form-urlencoded;charset=" + charset;

        //    var postData = Encoding.GetEncoding(charset).GetBytes(BuildQuery(parameters, charset));
        //    var reqStream = req.GetRequestStream();
        //    reqStream.Write(postData, 0, postData.Length);
        //    reqStream.Close();

        //    var rsp = (HttpWebResponse)req.GetResponse();
        //    var encoding = Encoding.GetEncoding(rsp.CharacterSet);
        //    return GetResponseAsString(rsp, encoding);
        //}
    }
}
