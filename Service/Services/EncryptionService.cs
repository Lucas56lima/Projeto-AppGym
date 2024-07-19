using System.Security.Cryptography;
using Domain.Interface;

namespace Service.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public EncryptionService()
        {
            var keyBase64 = Environment.GetEnvironmentVariable("ENCRYPTION_KEY");
            var ivBase64 = Environment.GetEnvironmentVariable("ENCRYPTION_IV");

            if (string.IsNullOrEmpty(keyBase64) || string.IsNullOrEmpty(ivBase64))
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    key = new byte[32]; // 32 bytes para AES-256
                    iv = new byte[16];  // 16 bytes para AES

                    rng.GetBytes(key);
                    rng.GetBytes(iv);

                    Console.WriteLine(key);
                    Console.WriteLine(iv);
                    Environment.SetEnvironmentVariable("ENCRYPTION_KEY", Convert.ToBase64String(key));
                    Environment.SetEnvironmentVariable("ENCRYPTION_IV", Convert.ToBase64String(iv));
                }
            }
            else
            {
                key = Convert.FromBase64String(keyBase64);
                iv = Convert.FromBase64String(ivBase64);
            }
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



