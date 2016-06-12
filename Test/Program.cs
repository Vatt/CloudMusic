using System;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.ReadKey();
        }

        static async void Test()
        {
            string message = "Test protection data";
            String strDescriptor = "LOCAL=user";
            BinaryStringEncoding encoding = BinaryStringEncoding.Utf8;
            DataProtectionProvider Provider = new DataProtectionProvider(strDescriptor);
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(message, encoding);
            IBuffer buffProtected = await Provider.ProtectAsync(buffMsg);
        }

    }
    
}

