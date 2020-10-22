using ONLINEAPP.GENERIC.MODEL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ONLINEAPP.GENERIC.INTERFACE
{
    public interface IDBOperation
    {
        List<Request> GetAllRequestDB(string token , string user, string tableName);

        SqlConnection CreateConnection();
    }
}
