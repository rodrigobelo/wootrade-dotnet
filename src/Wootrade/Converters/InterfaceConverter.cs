using System;
using Newtonsoft.Json;

namespace Wootrade.Converters
{
    internal class InterfaceConverter<TImp> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            try
            {
                return serializer.Deserialize<TImp>(reader);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}