using ONLINEAPP.MODEL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.DAL
{
    public class GenerateJsonFromObject
    {
        public static string GenerateJson<T>(T obj, out string id)
        {
            string identity = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("{'__metadata':{'type':'{0}'}");
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.CanRead && !string.IsNullOrEmpty(Convert.ToString(prop.GetValue(obj))))
                {
                    if (prop.Name.Equals("ID"))
                    {
                        identity = Convert.ToString(prop.GetValue(obj));
                    }
                    else
                    {
                        sb.Append(",'");
                        if (prop.PropertyType == typeof(List<User>))
                        {
                            sb.Append(GetJsonPropertyName<T>(prop.Name));
                            sb.Append("Id':{'results': [");
                            List<User> collection = prop.GetValue(obj) as List<User>;
                            List<BaseID> idcoll = collection.Select(item => new BaseID { ID = item.ID }).ToList<BaseID>();
                            int count = 0;
                            foreach (BaseID idprop in idcoll)
                            {
                                sb.Append(idprop.ID);
                                count++;
                                if (count != idcoll.Count)
                                {
                                    sb.Append(",");
                                }
                            }
                            sb.Append("]}");
                        }
                        else if ((prop.PropertyType.GetProperty("ID") != null && prop.PropertyType.GetProperty("Name") != null)
                                  || (prop.PropertyType.GetProperty("ID") != null && prop.PropertyType.GetProperty("Value") != null)
                                  || (prop.PropertyType.GetProperty("ID") != null && prop.PropertyType.GetProperty("UserName") != null)
                                  || (prop.PropertyType.GetProperty("ID") != null && prop.PropertyType.GetProperty("EMail") != null)
                                  || (prop.PropertyType.GetProperty("ID") != null && prop.PropertyType.GetProperty("Title") != null))
                        {
                            sb.Append(GetJsonPropertyName<T>(prop.Name));
                            sb.Append("Id':'");
                            object collection = prop.GetValue(obj);
                            var dataProperty = prop.PropertyType.GetProperty("ID");
                            sb.Append(dataProperty.GetValue(collection));
                            sb.Append("'");
                        }
                        else
                        {
                            sb.Append(GetJsonPropertyName<T>(prop.Name));
                            sb.Append("':'");
                            if (prop.PropertyType == typeof(DateTime?))
                                sb.Append(Convert.ToDateTime(prop.GetValue(obj)).ToString("yyyy-MM-ddTHH:mm:ssZ"));
                            else
                                sb.Append(Convert.ToString(prop.GetValue(obj)).Trim().Replace("\\", "").Replace("'", "\\'"));
                            sb.Append("'");
                        }

                    }
                }
            }
            sb.Append("}");
            id = identity;
            return Convert.ToString(sb);
        }

        public static string GetJsonPropertyName<T>(string propertyName)
        {
            string jsonPropertyName = string.Empty;

            PropertyInfo propInfo = typeof(T).GetProperty("_" + propertyName);

            if (propInfo == null)
            {
                propInfo = typeof(T).GetProperty(propertyName);
            }

            foreach (object attr in propInfo.GetCustomAttributes(true))
            {
                if ((attr as JsonPropertyAttribute) != null)
                {
                    jsonPropertyName = (attr as JsonPropertyAttribute).PropertyName;
                    break;
                }
            }

            return jsonPropertyName;
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }
    }
}
