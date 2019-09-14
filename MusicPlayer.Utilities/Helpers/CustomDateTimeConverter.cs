using Newtonsoft.Json.Converters;

namespace MusicPlayer.Utilities.Helpers
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
