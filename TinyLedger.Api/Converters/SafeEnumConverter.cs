using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TinyLedger.Api.Converters
{
    public class SafeEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumString = reader.GetString();

            if (!Enum.TryParse(enumString, ignoreCase: true, out T value))
            {
                throw new JsonException($"Invalid value '{{enumString}}' for enum type {typeof(T).Name}.");
            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
