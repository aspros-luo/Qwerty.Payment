using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Payment.AliPay.Sdk.ValueObjects;
using System;
using System.Text;

namespace Payment.AliPay.Sdk.Infrastructure
{
    /// <summary>
    /// Rsa 工具类
    /// </summary>
    public static class GenerateRsaAssist
    {
        /// <summary>
        /// 加签
        /// </summary>
        /// <returns></returns>
        public static string RasSign(string content, string privateKey, SignType signType)
        {
            var singerType = "";
            if (signType == SignType.Rsa2)
            {
                singerType = "SHA256WithRSA";
            }
            if (signType == SignType.Rsa)
            {
                singerType = "SHA1withRSA";
            }
            var signer = SignerUtilities.GetSigner(singerType);
            var privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
            signer.Init(true, privateKeyParam);
            var plainBytes = Encoding.UTF8.GetBytes(content);
            signer.BlockUpdate(plainBytes, 0, plainBytes.Length);
            var signBytes = signer.GenerateSignature();
            return Convert.ToBase64String(signBytes);
        }
        /// <summary>
        /// 验签
        /// </summary>
        /// <returns></returns>
        public static bool VerifySign(string content, string publicKey, string signData, SignType signType)
        {
            var singerType = "";
            if (signType == SignType.Rsa2)
            {
                singerType = "SHA256WithRSA";
            }
            if (signType == SignType.Rsa)
            {
                singerType = "SHA1withRSA";
            }
            var signer = SignerUtilities.GetSigner(singerType);
            var publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            signer.Init(false, publicKeyParam);
            var signBytes = Convert.FromBase64String(signData);
            var plainBytes = Encoding.UTF8.GetBytes(content);
            signer.BlockUpdate(plainBytes, 0, plainBytes.Length);
            var ret = signer.VerifySignature(signBytes);
            return ret;
        }
    }
}
