using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MusicPlayer.FileInfoManager
{
    public class DateHelper
    {
        public DateTime GetDateTimeFromString(string data)
        {
            var rules = new Regex(@"\d{4}");
            var match = rules.Match(data);
            DateTime dateTime = DateTime.MinValue;
            if (match.Success)
            {
                dateTime = DateTime
                    .ParseExact(match.Value, "yyyy",
                                CultureInfo.InvariantCulture);
            }

            return dateTime;
        }
    }
}
