namespace DeferredRegistration.Pipelines.DeferredRegistration
{
    using Sitecore.Configuration;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class StringCipher
    {
        private readonly string hash = Settings.GetSetting("DeferredRegistration.EncryptHash");

        public string Encrypt(string rawString)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
                using (var tripleDes = new TripleDESCryptoServiceProvider
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    var data = Encoding.UTF8.GetBytes(rawString);
                    var transform = tripleDes.CreateEncryptor();
                    var results = transform.TransformFinalBlock(data, 0, data.Length);

                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        public string Decrypt(string encryptedString)
        {
            var data = Convert.FromBase64String(encryptedString);

            using (var md5 = new MD5CryptoServiceProvider())
            {
                var keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
                using (var tripDes = new TripleDESCryptoServiceProvider
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    var transform = tripDes.CreateDecryptor();
                    var results = transform.TransformFinalBlock(data, 0, data.Length);

                    return Encoding.UTF8.GetString(results);
                }
            }
        }
    }
}