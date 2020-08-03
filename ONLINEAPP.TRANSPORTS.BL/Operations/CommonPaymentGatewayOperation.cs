using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.BL.Operations
{
    public class CommonPaymentGatewayOperation
    {
        //class to generate secure Hash 

        public string generateSecureHash(string secureKey, string messageToHash)
        {
            string hmacResult = string.Empty;
            //Encoding asciiEncoding = Encoding.ASCII;

            try
            {
                int NumberChars = secureKey.Length / 2;
                byte[] key = new byte[NumberChars];

                using (var sr = new StringReader(secureKey))
                {
                    for (int i = 0; i < NumberChars; i++)
                    {
                        key[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
                    }
                }

                byte[] message = new byte[messageToHash.Length];
                Encoding.UTF8.GetBytes(messageToHash, 0, messageToHash.Length, message, 0);
                //asciiEncoding.GetBytes(messageToHash, 0, messageToHash.Length, message, 0);

                var hash = new HMACSHA256(key);
                byte[] result = hash.ComputeHash(message);

                var hex = new StringBuilder(result.Length * 2);
                foreach (var b in result)
                {
                    hex.AppendFormat("{0:x2}", b);
                }

                hmacResult = hex.ToString().ToUpper();
            }
            catch (Exception)
            {

                throw;
            }

            return hmacResult;
        }
    }
}
