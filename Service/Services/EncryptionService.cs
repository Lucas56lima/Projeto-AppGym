using System.Security.Cryptography;
using System.Text;
using Domain.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;

namespace Service.Services
{
    public class EncryptionService : IEncryptionService
    {

        //private readonly byte[] key;
        //private readonly byte[] iv;
        //public EncryptionService()
        //{
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        key = new byte[32]; // 32 bytes para AES-256
        //        iv = new byte[16]; // 16 bytes para AES

        //        rng.GetBytes(key);
        //        rng.GetBytes(iv);
        //    }
        //}
        //public async Task<string> Decrypt(byte[] cipherText)
        //{
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = key;
        //        aesAlg.IV = iv;

        //        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        //        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        //        {
        //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //                {
        //                    return srDecrypt.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //}

        //public async Task<byte[]> Encrypt(string plainText)
        //{
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = key;
        //        aesAlg.IV = iv;

        //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                {
        //                    swEncrypt.Write(plainText);
        //                }
        //            }
        //            return msEncrypt.ToArray().ToString();
        //        }
        //    }
        //}
        public Task<string> Decrypt(byte[] encryptedText)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> Encrypt(string plainText)
        {
            throw new NotImplementedException();
        }
    }

}
