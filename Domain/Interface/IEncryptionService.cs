using System.Security.Cryptography;

namespace Domain.Interface
{
    public interface IEncryptionService
    {
        Task<string> Encrypt(string plainText);
        Task<string>Decrypt(byte[] cipherText);
    }
}
