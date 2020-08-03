using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.DAL
{
    public class DateConverterToLocalTime: JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
                return null;
            if (string.IsNullOrEmpty(Convert.ToString(reader.Value)))
                return null;
            DateTime obj = Convert.ToDateTime(reader.Value);
            if (obj == null)
                return null;
            DateTime dt = obj.ToLocalTime();
            return dt;

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
