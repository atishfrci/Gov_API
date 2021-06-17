using Newtonsoft.Json.Linq;
using ONLINEAPP.GENERIC.MODEL;
using ONLINEAPP.MODEL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ONLINEAPP.GENERIC.INTERFACE
{
    public interface IDBOperation
    {
        List<Request> GetAllRequestDB(string token , string user, string tableName);

        List<Request> InsertEservicesApplicationInDB(string token, JArray valueKey, string tableName, List<FieldsList> fields,string ID, string usr);
    }
}
