using System.Security.Cryptography;

namespace Domain.Interface
{
    public interface IEncryptionService
    {
        Task<byte[]> Encrypt(string plainText);
        Task<string>Decrypt(byte[] encryptedText);        
    }
}
