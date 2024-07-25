using System.Configuration;
using System.Security.Cryptography;
using Domain.Interface;
using Microsoft.Extensions.Configuration;

namespace Service.Services
{
    public class EncryptionService : IEncryptionService
    {
        public IConfiguration Configuration { get; }
        private readonly byte[] iv;
        private readonly byte[] key;
        public EncryptionService(IConfiguration configuration)
        {
            Configuration = configuration;
            key = Convert.FromBase64String(Configuration["Encryption:Key"] ?? string.Empty);
            iv = Convert.FromBase64String(Configuration["Encryption:Iv"] ?? string.Empty);
        }

        public async Task<string> Encrypt(string plainText)
        {
            
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            await swEncrypt.WriteAsync(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public async Task<string> Decrypt(string cipherText)
        {
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return await srDecrypt.ReadToEndAsync();
                            }
                        }
                    }
                }
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine($"An error occurred during decryption: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
        }
    }
}



