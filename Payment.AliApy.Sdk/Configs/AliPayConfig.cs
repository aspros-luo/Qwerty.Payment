namespace Payment.AliPay.Sdk.Configs
{
    public static class AliPayConfig
    {
        public static void Init(string appId, string privateKey, string aliPublicKey, string returnUrl, string notifyUrl)
        {
            AppId = appId;
            PrivateKey = privateKey;
            AliPublicKey = aliPublicKey;
            ReturnUrl = string.IsNullOrWhiteSpace(returnUrl) ? notifyUrl : returnUrl;
            NotifyUrl = notifyUrl;
        }

        public static string AppId { get; private set; }
        //public static string Gateway { get; private set; } = "https://openapi.alipay.com/gateway.do";
        internal static string Gateway { get; private set; } = "https://openapi.alipaydev.com/gateway.do";
        public static string PrivateKey { get; private set; }
        public static string AliPublicKey { get; private set; }
        public static string ReturnUrl { get; private set; }
        public static string NotifyUrl { get; private set; }
    }
}
