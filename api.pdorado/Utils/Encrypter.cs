using System.Text;
using System.Security.Cryptography;

namespace api.pdorado.Utils
{
    /// <summary>
    /// Clase de utilidad para Encriptar y Desencriptar cadenas de texto
    /// </summary>
    public class Encrypter
    {
        /// <summary>
        /// Array de bytes que se usa para alterar mismos resultados desde el mismo input (Aquí se usa siempre el mismo, no afecta)
        /// </summary>
        public static readonly byte[] _salt = new byte[] { 0x70, 0x64, 0x6f, 0x72, 0x61, 0x64, 0x6f, 0x2e };

        /// <summary>
        /// Encripta un string con el método AES
        /// </summary>
        /// <param name="text">Texto a encriptar</param>
        /// <param name="publicKey">Llave pública que se encuentra en App.config</param>
        /// <returns>Texto encriptado</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string EncryptStringAES(string text, string publicKey)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("Texto Plano");
            if (string.IsNullOrEmpty(publicKey))
                throw new ArgumentNullException("Clave publica");

            string res = null;                       // Encrypted string to return
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(publicKey, _salt);

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encrypter = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // prepend the IV
                    memoryStream.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    memoryStream.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(memoryStream, encrypter, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            writer.Write(text);
                        }
                    }
                    res = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return res;
        }

        /// <summary>
        /// Desencripta un string que se encriptó con el metodo EncryptStringAES utilizando la misma clave pública
        /// </summary>
        /// <param name="obscureText">Texto a desencriptar</param>
        /// <param name="publicKey">Llave pública</param>
        public static string DecryptStringAES(string obscureText, string publicKey)
        {
            if (string.IsNullOrEmpty(obscureText))
                throw new ArgumentNullException("Texto cifrado");
            if (string.IsNullOrEmpty(publicKey))
                throw new ArgumentNullException("Clave pública");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plainText = null;

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(publicKey, _salt);

                // Create the streams used for decryption.                
                byte[] bytes = Convert.FromBase64String(obscureText);
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(memoryStream);
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decrypter = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(memoryStream, decrypter, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plainText = reader.ReadToEnd();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plainText;
        }

        /// <summary>
        /// Método que saca un array de bytes a partir de un stream
        /// </summary>
        /// <param name="s">El stream que contiene el array de bytes</param>
        /// <returns>El array de bytes que contenia el stream</returns>
        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Error en el formato del stream");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("No se ha leido el array correctamente");
            }

            return buffer;
        }
    }
}