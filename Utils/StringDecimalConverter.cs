using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ESAPrizes.Utils {
    public class StringDecimalConverter : JsonConverter<decimal?>
    {
        public StringDecimalConverter() {}

        public override bool CanConvert(Type typeToConvert)
        {
            return  typeToConvert == typeof(decimal) || 
                    typeToConvert == typeof(decimal?);
        }

        public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions serializer)
        {
            string strValue = reader.GetString();
            if (strValue == null) {
                return GetDefaultValue(typeToConvert);
            }
            
            var success = Decimal.TryParse(strValue, NumberStyles.Currency, new CultureInfo("en-US"), out decimal value);
            if (!success) {
                //Invalid decimal string.
                return GetDefaultValue(typeToConvert);
            }
            return value;
        }

        public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions serializer)
        {
            string str = (value.GetType() == typeof(decimal) ? "$0.00" : null);
            var dec = value as decimal?;
            if (dec != null) {
                str = dec.Value.ToString("C", new CultureInfo("en-US"));
            }
            writer.WriteStringValue(JsonEncodedText.Encode(str));
        }

        private decimal? GetDefaultValue(Type objectType) {
            if (objectType == typeof(decimal?)) {
                return null;
            }
            return 0m;
        }
    }
}