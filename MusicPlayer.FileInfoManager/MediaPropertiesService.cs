using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace MusicPlayer.FileInfoManager
{
    public class MediaPropertiesService
    {
        private readonly FileInfo _fileInfo;
        private readonly ShellProperties _properties;

        public MediaPropertiesService(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;

            _properties = ShellFile.FromFilePath(fileInfo.FullName).Properties;
        }

        public string GetAuthor()
        {
            var author = _fileInfo.Directory.Parent.Name;

            return author;
        }

        public DateTime GetAlbumYear(string albumName)
        {
            var yearPropertyNames = new[]
            {
                "System.DateModified",
                "System.DateCreated",
                "System.Document.DateCreated",
                "System.Document.DateSaved",
                "System.ItemDate",
                "System.DateImported"
            };

            var albumYear = _properties.DefaultPropertyCollection
                .Where(p => yearPropertyNames.Contains(p.CanonicalName))
                .Select(t => DateTime.Parse(t.ValueAsObject.ToString()))
                .Min();

            var yearFromAlbumString = DateTime.MinValue;

            if (DateTime.TryParse(Regex
                .Match(albumName, @"\(([^)]*)\)")
                .Groups[1]
                .Value, out yearFromAlbumString))
            {
                if (yearFromAlbumString < albumYear)
                {
                    albumYear = yearFromAlbumString;
                }
            }

            return albumYear;
        }

        private string GetFileProperty(string[] propertyNames)
        {
            var propertyValue = string.Empty;

            var property = _properties.DefaultPropertyCollection
                    .FirstOrDefault(p => propertyNames.Contains(p.CanonicalName));
            if (property != null)
            {
                propertyValue = property.ValueType == typeof(string[]) ?
                    string.Join(",", property.ValueAsObject as string[])
                    : property.ValueAsObject?.ToString();
            }

            return propertyValue;
        }

        public string GetAlbum()
        {
            var album = GetFileProperty(new [] {"System.Music.AlbumTitle" });
            if (string.IsNullOrEmpty(album))
            {
                album = _fileInfo.Directory.Name;
            }

            return album;
        }

        public int GetTrackNo()
        {
            var trackNo = GetFileProperty(new [] { "System.Music.TrackNumber" });

            return int.Parse(trackNo);
        }

        public string GetBitRate()
        {
            var trackNo = GetFileProperty(new[] { "System.Audio.EncodingBitrate" });

            return trackNo;
        }

        public string GetGenre()
        {
            var genre = GetFileProperty(new[] { "System.Music.Genre" });

            return genre;
        }

        public TimeSpan GetDuration()
        {
            var duration = GetFileProperty(new[] { "System.Media.Duration" });

            return new TimeSpan(long.Parse(duration));
        }

        public string GetSongName(string author)
        {
            var songName = GetFileProperty(new[] { "System.ItemNameDisplay" });

            return songName
                .Replace(author, "")
                .Replace('-', ' ')
                .Trim();
        }
    }
}
