using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace AES_Demo
{
    internal class AES_Demo
    {
        public enum KeySize
        {
            Bits128 = 128,
            Bits192 = 192,
            Bits256 = 256
        }

        public enum ChainMode
        {
            ECB = 0,
            CBC,
            CFB,
            OFB,
            CTR
        }

        public static Encoding PlainText_Encoding = Encoding.ASCII;
        public static Encoding KeyIV_Encoding = Encoding.ASCII;
        public static PaddingMode paddingMode = PaddingMode.PKCS7;

        public static int blockSize = 128;

        public static byte[] getBytes_Text(string plainText)
        {
            return PlainText_Encoding.GetBytes(plainText);
        }

        public static byte[] getBytes_Key(string key)
        {
            return KeyIV_Encoding.GetBytes(key);
        }

        public static string getString_Text(byte[] plainBytes)
        {
            return PlainText_Encoding.GetString(plainBytes);
        }

        public static string getString_Key(byte[] keyBytes)
        {
            return KeyIV_Encoding.GetString(keyBytes);
        }

        public static string randomKey(int keySize)
        {
            byte[] key = new byte[keySize / (8 * 2)];
            new RNGCryptoServiceProvider().GetBytes(key);
            return bytesArrayToString(key);
        }

        public static CipherMode mapCipherMode(ChainMode mode)
        {
            switch (mode)
            {
                case ChainMode.ECB:
                    return CipherMode.ECB;
                case ChainMode.CBC:
                    return CipherMode.CBC;
                case ChainMode.CFB:
                    return CipherMode.CFB;
                default:
                    throw new Exception("Invalid ChainMode");
            }
        }

        public static ChainMode mapCipherMode(CipherMode mode)
        {
            switch (mode)
            {
                case CipherMode.ECB:
                    return ChainMode.ECB;
                case CipherMode.CBC:
                    return ChainMode.CBC;
                case CipherMode.CFB:
                    return ChainMode.CFB;
                default:
                    throw new Exception("Invalid ChainMode");
            }
        }

        public static string modeToString(ChainMode mode)
        {
            switch (mode)
            {
                case ChainMode.ECB:
                    return "ECB";
                case ChainMode.CBC:
                    return "CBC";
                case ChainMode.CFB:
                    return "CFB";
                case ChainMode.OFB:
                    return "OFB";
                case ChainMode.CTR:
                    return "CTR";
                default:
                    return "Unknown";
            }
        }

        public static string bytesArrayToString(byte[] bytes)
        {
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += bytes[i].ToString("X2");
            }
            return str;
        }

        public static byte[] stringToBytesArray(string str)
        {
            byte[] bytes = new byte[str.Length / 2];
            for (int i = 0; i < str.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            }
            return bytes;
        }

        /// <summary>
        /// Encrypts the plainText using the specified chainMode, keySize, key and iv.
        /// </summary>
        /// <param name="chainMode"></param>
        /// <param name="keySize"></param>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>
        /// Returns the encrypted text as a byte array.
        /// </returns>
        public static byte[] Encrypt(ChainMode chainMode, KeySize keySize, byte[] plainText, byte[] key, byte[] iv = null)
        {
            if (chainMode == ChainMode.ECB)
            {
                return Encrypt_Helper(chainMode, keySize, plainText, key);
            }

            else if (chainMode >= ChainMode.CBC && chainMode <= ChainMode.CFB)
            {
                return Encrypt_Helper(chainMode, keySize, plainText, key, iv);
            }
            else
            {
                return Encrypt_BoucyCastle(chainMode, keySize, plainText, key, iv);
            }

        }

        // Helper function for encryption using ECB, CBC, CFB modes
        private static byte[] Encrypt_Helper(ChainMode chainMode, KeySize keySize, byte[] plainText, byte[] key, byte[] iv = null)
        {
            byte[] encrypted = null;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = (int)keySize;
                aes.BlockSize = 128;
                aes.Mode = mapCipherMode(chainMode);
                aes.Padding = paddingMode;
                aes.Key = key;
                if (iv != null)
                {
                    aes.IV = iv;
                }
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    encrypted = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
                }
            }
            return encrypted;

        }

        // BoucyCastle implementation of encryption using OFB and CTR modes
        private static byte[] Encrypt_BoucyCastle(ChainMode chainMode, KeySize keySize, byte[] plainText, byte[] key, byte[] iv = null)
        {
            AesEngine aesEngine = new AesEngine();
            PaddedBufferedBlockCipher cipher;
            switch (chainMode)
            {
                case ChainMode.OFB:
                    cipher = new PaddedBufferedBlockCipher(new OfbBlockCipher(aesEngine, blockSize), new Pkcs7Padding()); // OFB, block size is in bit
                    break;
                case ChainMode.CTR:
                    cipher = new PaddedBufferedBlockCipher(new SicBlockCipher(aesEngine), new Pkcs7Padding()); // CTR
                    break;
                default:
                    throw new Exception("How did you even get here?!!");
            }

            KeyParameter keyParameter = new KeyParameter(key);
            ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParameter, iv);

            cipher.Init(true, keyParamWithIV);
            byte[] encrypted = new byte[cipher.GetOutputSize(plainText.Length)];

            int length = cipher.ProcessBytes(plainText, 0, plainText.Length, encrypted, 0);

            try
            {
                cipher.DoFinal(encrypted, length);

            }
            catch (CryptoException ce)
            {
                MessageBox.Show("Error: " + ce.ToString());
            }

            return encrypted;
        }

        /// <summary>
        /// Decrypts the cipherText using the specified chainMode, keySize, key and iv.
        /// </summary>
        /// <param name="chainMode"></param>
        /// <param name="keySize"></param>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>
        /// Returns the decrypted text as a byte array.
        /// </returns>
        public static byte[] Decrypt(ChainMode chainMode, KeySize keySize, byte[] cipherText, byte[] key, byte[] iv = null)
        {
            if (chainMode == ChainMode.ECB)
            {
                return Decrypt_Helper(chainMode, keySize, cipherText, key);
            }

            else if (chainMode >= ChainMode.CBC && chainMode <= ChainMode.CFB)
            {
                return Decrypt_Helper(chainMode, keySize, cipherText, key, iv);
            }
            else
            {
                return Decrypt_BoucyCastle(chainMode, keySize, cipherText, key, iv);
            }
        }

        // Helper function for decryption using ECB, CBC, CFB modes
        private static byte[] Decrypt_Helper(ChainMode chainMode, KeySize keySize, byte[] cipherText, byte[] key, byte[] iv = null)
        {
            byte[] decrypted = null;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = (int)keySize;
                aes.BlockSize = 128;
                aes.Mode = mapCipherMode(chainMode);
                aes.Padding = paddingMode;
                aes.Key = key;
                if (iv != null)
                {
                    aes.IV = iv;
                }
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    decrypted = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                }
            }
            return decrypted;
        }

        // BoucyCastle implementation of decryption using OFB and CTR modes
        private static byte[] Decrypt_BoucyCastle(ChainMode chainMode, KeySize keySize, byte[] cipherText, byte[] key, byte[] iv = null)
        {
            AesEngine aesEngine = new AesEngine();
            PaddedBufferedBlockCipher cipher;
            switch (chainMode)
            {
                case ChainMode.OFB:
                    cipher = new PaddedBufferedBlockCipher(new OfbBlockCipher(aesEngine, blockSize), new Pkcs7Padding()); // OFB, block size is in bit
                    break;
                case ChainMode.CTR:
                    cipher = new PaddedBufferedBlockCipher(new SicBlockCipher(aesEngine), new Pkcs7Padding()); // CTR
                    break;
                default:
                    throw new Exception("How did you even get here?!!");
            }

            KeyParameter keyParameter = new KeyParameter(key);
            ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParameter, iv);

            cipher.Init(false, keyParamWithIV);

            byte[] decrypted = new byte[cipher.GetOutputSize(cipherText.Length)];

            int length = cipher.ProcessBytes(cipherText, 0, cipherText.Length, decrypted, 0);

            try
            {
                cipher.DoFinal(decrypted, length);
            }
            catch (CryptoException ce)
            {
                MessageBox.Show("Error: " + ce.ToString());
            }

            return decrypted;
        }
    }
}
