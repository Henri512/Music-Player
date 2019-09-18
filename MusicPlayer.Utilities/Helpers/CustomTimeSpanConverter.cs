using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MusicPlayer.Utilities.Helpers
{
    public class CustomTimeSpanConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((TimeSpan)value).ToString(@"mm\:ss"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = TimeSpan.Zero;

            if (reader.Value != null)
                TimeSpan.TryParse(reader.Value.ToString(), out result);

            return result;
        }
    }
}
