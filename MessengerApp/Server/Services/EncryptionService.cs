﻿using MessengerApp.Client.Sevices.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MessengerApp.Client.Sevices
{
    public class EncryptionService : IEncryptionService
    {
        private byte[] _initializationVector =
        {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };
        public async Task<string> EncryptAsync(string textToEncrypt, string passphrase)
        {
            using Aes aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(passphrase);
            aes.IV = _initializationVector;
            using MemoryStream output = new();
            using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);
            await cryptoStream.WriteAsync(Encoding.Unicode.GetBytes(textToEncrypt));
            await cryptoStream.FlushFinalBlockAsync();
            return String.Join("-",output.ToArray());
        }
        public async Task<string> DecryptAsync(string encrypted, string passphrase)
        {
            using Aes aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(passphrase);
            aes.IV = _initializationVector;
            var encryptedBytes = encrypted.Split("-").Select(e=>Convert.ToByte(e)).ToArray();
            using MemoryStream input = new(encryptedBytes);
            using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using MemoryStream output = new();
            await cryptoStream.CopyToAsync(output);
            return Encoding.Unicode.GetString(output.ToArray());
        }
        private byte[] DeriveKeyFromPassword(string password)
        {
            var emptySalt = Array.Empty<byte>();
            var iterations = 1000;
            var desiredKeyLength = 16;
            var hashMethod = HashAlgorithmName.SHA384;
            return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
                                             emptySalt,
                                             iterations,
                                             hashMethod,
                                             desiredKeyLength);
        }
    }
}
