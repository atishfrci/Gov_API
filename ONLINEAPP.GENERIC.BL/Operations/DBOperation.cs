using ONLINEAPP.GENERIC.MODEL;
using ONLINEAPP.GENERIC.INTERFACE;
using System;
using System.Collections.Generic;
using System.Reflection;
using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ONLINEAPP.GENERIC.BL.Operations
{
    public class DBOperation : IDBOperation
    {
        public List<Request> InsertEservicesApplicationInDB(string token, JArray valueKey, string tableName, List<FieldsList> fieldsList,string ID, string usr)
        {
            try
            {
                List<Request> requestList = new List<Request>();
                using (SqlConnection connection = new SqlConnection())
                {

                    string keys = ""; string values = "";
                    foreach (FieldsList fields in fieldsList)
                    {
                        if (!fields.InternalName.ToLower().Contains("requestid") && !fields.InternalName.ToLower().Contains("status") && !fields.InternalName.ToLower().Contains("internalstatus"))
                        {
                            keys = string.Concat(keys + ",", fields.InternalName);
                            values = string.Concat(values + ",", "@" + fields.InternalName);
                        }
                    }

                    keys = keys.Substring(1);
                    values = values.Substring(1);
                   

                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["MauritiusDBEservices"].ConnectionString;
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO " + tableName + "(ID,UserName,"+keys+ ") VALUES(@ID,@UserName," + values +")", connection))
                    {
                        foreach (FieldsList fields in fieldsList)
                        {
                            if (!fields.InternalName.ToLower().Contains("requestid") && !fields.InternalName.ToLower().Contains("status") && !fields.InternalName.ToLower().Contains("internalstatus"))
                            {
                                string key = "@" + fields.InternalName;
                                sqlCommand.Parameters.AddWithValue(key, valueKey[0][fields.InternalName].ToString());
                            }
                        }
                        sqlCommand.Parameters.AddWithValue("@UserName", DecryptStringAES(usr));
                        sqlCommand.Parameters.AddWithValue("@ID", ID);
                        var result = sqlCommand.ExecuteScalar();
  
                    }

                    using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM " + tableName + " WHERE ID = @ID", connection))
                    {
                        sqlCommand.Parameters.AddWithValue("ID", ID);
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.HasRows)
                            {
                                int ordinal1 = sqlDataReader.GetOrdinal("RequestId");
                               
                                while (sqlDataReader.Read())
                                {
                                    Request request = new Request();
                                    if (!sqlDataReader.IsDBNull(ordinal1))
                                    {
                                        request.GenericId = sqlDataReader.GetString(ordinal1).ToString();
                                        request.SID = sqlDataReader.GetString(ordinal1).ToString();
                                    }
                                    requestList.Add(request);
                                }
                            }
                        }
                    }
                    connection.Close();
                    return requestList;
                }
            }
            catch (Exception ex)
            {
                string name1 = MethodBase.GetCurrentMethod().Name;
                string name2 = MethodBase.GetCurrentMethod().DeclaringType.Name;
                //throw new Exception(string.Format("An error occured while reading data. GUID: {0}", (object)CRUDOperations.WriteException(ex, name1, name2)));
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", ex.ToString()));
            }
        }

        public static string DecryptStringAES(string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes("9061737324613234");
            var iv = Encoding.UTF8.GetBytes("9061737324613234");

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check for null arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            string plaintext = null;

            // Create an RijndaelManaged object  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }

        public List<Request> GetAllRequestDB(string token, string user, string tableName)
        {
            try
            {
                List<Request> requestList = new List<Request>();
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["MauritiusDBEservices"].ConnectionString;
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM " + tableName + " WHERE Username = @UserName", connection))
                    {
                        sqlCommand.Parameters.AddWithValue("UserName", (object)user);
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.HasRows)
                            {
                                int ordinal1 = sqlDataReader.GetOrdinal("GenericId");
                                int ordinal2 = sqlDataReader.GetOrdinal("RequestId");
                                int ordinal3 = sqlDataReader.GetOrdinal("CreatedDate");
                                int ordinal4 = sqlDataReader.GetOrdinal("Status");
                                while (sqlDataReader.Read())
                                {
                                    Request request = new Request();
                                    if (!sqlDataReader.IsDBNull(ordinal1) && !sqlDataReader.IsDBNull(ordinal2))
                                    {
                                        request.GenericId = sqlDataReader.GetString(ordinal1).ToString();
                                        request.SID = sqlDataReader.GetString(ordinal2).ToString();
                                    }
                                    if (!sqlDataReader.IsDBNull(ordinal3))
                                        request.Created = sqlDataReader.GetDateTime(ordinal3).ToString("dd/MM/yyyy hh:mm tt");
                                    if (!sqlDataReader.IsDBNull(ordinal4))
                                        request.Status = sqlDataReader.GetString(ordinal4).ToString();
                                    requestList.Add(request);
                                }
                            }
                        }
                    }
                    connection.Close();
                }
                return requestList;
            }
            catch (Exception ex)
            {
                string name1 = MethodBase.GetCurrentMethod().Name;
                string name2 = MethodBase.GetCurrentMethod().DeclaringType.Name;
                //throw new Exception(string.Format("An error occured while reading data. GUID: {0}", (object)CRUDOperations.WriteException(ex, name1, name2)));
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", ex.ToString()));
            }
        }
    }
}
