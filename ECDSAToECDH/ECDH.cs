using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace ECDSAToECDH
{
    public class ECDH
    {
        /// <summary>
        /// This is where a private key is stored. Private key must be saved as hex format.
        /// </summary>
        public string PrivateKey { get; set; }
        /// <summary>
        /// This is where the public key is stored. Public key must be saved as hex format. This is the public key that belongs to private key.
        /// </summary>
        public string PublicKey { get; set; }
        /// <summary>
        /// This is the public key to which you are creating the shared secret with. 
        /// </summary>
        public string SharedPublicKey { get; set; }
        /// <summary>
        /// This is the public key that was generated to create shared secret. 
        /// </summary>
        public string SharedGeneratedKey { get; set; }
        /// <summary>
        /// This is for assigning a unique ID to the variable.
        /// </summary>
        public string? UniqueIdentifier { get; set; }
        /// <summary>
        /// This is the shared secret produced from PrivateKey and SharedPublicKey
        /// </summary>
        public string SharedSecretHex { get { return GenerateSharedSecret(); } }
        /// <summary>
        /// This is the shared secret produced from PrivateKey and SharedPublicKey
        /// </summary>
        public byte[] SharedSecretByte { get { return HexStringToByteArray(SharedSecretHex); } }

        private string GenerateSharedSecret()
        {
            BigInteger P, a, y, ka;

            BigInteger a1 = BigInteger.Parse(PrivateKey, NumberStyles.AllowHexSpecifier);//converts hex private key into big int.
            BigInteger a2 = BigInteger.Parse(PublicKey, NumberStyles.AllowHexSpecifier);//converts hex public key into big int.
            BigInteger b2 = BigInteger.Parse(SharedGeneratedKey, NumberStyles.AllowHexSpecifier);//converts hex shared public key into big int.

            P = a2;
            a = a1;
            y = b2;

            ka = power(y, a, P);

            string sharedSecretHex = ka.ToString("X").Substring(0, 64);

            return sharedSecretHex;
        }

        public byte[] Encrypt(string plainText)
        {
            byte[] key = SharedSecretByte;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = key;
                aes.GenerateIV();

                ICryptoTransform encryptor = aes.CreateEncryptor();

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                byte[] encryptedData = new byte[aes.IV.Length + encryptedBytes.Length];
                Array.Copy(aes.IV, 0, encryptedData, 0, aes.IV.Length);
                Array.Copy(encryptedBytes, 0, encryptedData, aes.IV.Length, encryptedBytes.Length);

                return encryptedData;
            }
        }
        public string Decrypt(byte[] encryptedData)
        {
            byte[] key = SharedSecretByte;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = key;

                byte[] iv = new byte[aes.BlockSize / 8];
                byte[] encryptedBytes = new byte[encryptedData.Length - iv.Length];

                Array.Copy(encryptedData, 0, iv, 0, iv.Length);
                Array.Copy(encryptedData, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor();

                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                return decryptedText;
            }
        }
        private byte[] HexStringToByteArray(string hexString)
        {
            int byteCount = hexString.Length / 2;
            byte[] bytes = new byte[byteCount];
            for (int i = 0; i < byteCount; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        private string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
        private BigInteger power(BigInteger a, BigInteger b, BigInteger P)
        {
            return BigInteger.ModPow(a, b, P);
        }
    }
}
