using System;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace CloudMusic.UWP.Common
{
    class EncryptDecryptHeper
    {

        public static async Task<string> EncryptAsync(string toEncrypt)
        {
            DataProtectionProvider Provider = new DataProtectionProvider("LOCAL=user");
            var encoding = BinaryStringEncoding.Utf8;
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(toEncrypt, encoding);
            IBuffer buffProtected = await Provider.ProtectAsync(buffMsg);
            return CryptographicBuffer.EncodeToBase64String(buffProtected);
        }
        public static async Task<string> DecryptAsync(string toDecrypt)
        {
            DataProtectionProvider Provider = new DataProtectionProvider();
            var encoding = BinaryStringEncoding.Utf8;
            var buffer = CryptographicBuffer.DecodeFromBase64String(toDecrypt);
            IBuffer buffUnprotected = await Provider.UnprotectAsync(buffer);
            return CryptographicBuffer.ConvertBinaryToString(encoding, buffUnprotected);
        }
    }
}
