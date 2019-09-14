using Newtonsoft.Json.Converters;

namespace MusicPlayer.Helpers
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter(string format)
        {
            base.DateTimeFormat = format;
        }
    }
}
