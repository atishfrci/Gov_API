using ONLINEAPP.GENERIC.MODEL;
using ONLINEAPP.GENERIC.INTERFACE;
using System;
using System.Collections.Generic;
using System.Reflection;
using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using System.Data.SqlClient;

namespace ONLINEAPP.GENERIC.BL.Operations
{
    public class DBOperation : IDBOperation
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Request> GetAllRequestDB(string token , string user , string tableName) 
        {
            try 
            {
                List<Request> ListReq = new List<Request>();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Server=localhost;Database=Mauritius_Eservices;Trusted_Connection=true";
                    conn.Open();

                    //using (SqlCommand cmd = new SqlCommand("SELECT * FROM " + tableName + " WHERE UserName = @UserName", conn))
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Mauritius_Eservices.ActiveApplications WHERE UserName = @UserName", conn))
                    {
                        cmd.Parameters.AddWithValue("UserName", user);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Read advances to the next row.
                                int CreatedDate = reader.GetOrdinal("CreatedDate");
                                int Title = reader.GetOrdinal("Title");
                                while (reader.Read())
                                {
                                    Request p = new Request();
                                    // To avoid unexpected bugs access columns by name.
                                    /*if (!reader.IsDBNull(RequestId))
                                    {
                                        p.GenericId = reader.GetString(RequestId);
                                    }*/
                                    if (!reader.IsDBNull(Title))
                                    {
                                        p.Status = reader.GetString(Title);
                                    }
                                    if (!reader.IsDBNull(CreatedDate))
                                    {
                                        p.Created = reader.GetDateTime(CreatedDate).ToString();
                                    }

                                    ListReq.Add(p);
                                    // If a column is nullable always check for DBNull...
                                    /* if (!reader.IsDBNull(middleNameIndex))
                                     {
                                         p.MiddleName = reader.GetString(middleNameIndex);
                                     }
                                     p.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                     persons.Add(p);*/
                                }
                            }
                        }
                    }
                    conn.Close();
                }

                
                return ListReq;

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
           
        }

        public SqlConnection CreateConnection() {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString ="Server=localhost;Database=Mauritius_Eservices;Trusted_Connection=true";
                    return conn;
                }
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }

        }
    }
}
