using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models.Util
{
    public class NumberToStringConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jt = JValue.ReadFrom(reader);

            return jt.Value<long>();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(System.Int64).Equals(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }

    public class JsonDateConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jt = JValue.ReadFrom(reader);

            return jt.Value<DateTime>();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(System.DateTime).Equals(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = ObjectToDateTime(value, DateTime.Now);
            serializer.Serialize(writer, date.ToString("MM/dd/yyyy"));
        }

        public DateTime ObjectToDateTime(object o, DateTime defaultValue)
        {
            DateTime result;
            if (DateTime.TryParse((o ?? "").ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
