namespace Plugin.Toolkit.Security.Services
{
    public class SHA256Service
    {
        private byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
        private byte[] key;

        /// <summary>
        /// Initializes a new instance of the SecurityToolkitSHA256 class.
        /// </summary>
        /// <param name="secret">The secret key to use for encryption and decryption.</param>
        public SHA256Service(string secret)
        {
            key = Encoding.ASCII.GetBytes(secret);
        }

        /// <summary>
        /// Encrypts a given string value using the Advanced Encryption Standard (AES) algorithm.
        /// </summary>
        /// <param name="value">The string value to be encrypted.</param>
        /// <returns>The encrypted string value as a base64-encoded string.</returns>
        public string Encryption(string value)
        {
            try
            {
                Aes encryptor = Aes.Create();
                encryptor.Mode = CipherMode.CBC;
                encryptor.Key = key.Take(32).ToArray();
                encryptor.IV = iv;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.ASCII.GetBytes(value);
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        byte[] cipherBytes = memoryStream.ToArray();
                        return Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex);
#endif
            }
            return string.Empty;
        }

        /// <summary>
        /// Decrypts a given string using the AES encryption algorithm.
        /// </summary>
        /// <param name="value">The encrypted string to be decrypted.</param>
        /// <returns>The decrypted string, or an empty string if decryption fails.</returns>
        public string Decryption(string value)
        {
            try
            {
                Aes encryptor = Aes.Create();
                encryptor.Mode = CipherMode.CBC;
                encryptor.Key = key.Take(32).ToArray();
                encryptor.IV = iv;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write))
                    {
                        byte[] cipherBytes = Convert.FromBase64String(value);
                        cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        byte[] plainBytes = memoryStream.ToArray();
                        return Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex);
#endif
            }
            return string.Empty;
        }
    }
}
