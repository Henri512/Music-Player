using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TagLib;
using File = TagLib.File;

namespace MusicPlayer.FileInfoManager
{
    public class MediaPropertiesService : IDisposable
    {
        private readonly FileInfo _fileInfo;
        private Tag _tag;
        private File _songFile;

        public MediaPropertiesService(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;

            using (var stream = new StreamReader(fileInfo.FullName))
            {
                SetAudioFileAndTag(stream.BaseStream, fileInfo.FullName);
            }
        }

        public void SetAudioFileAndTag(Stream stream, string fileName)
        {
            //Create a simple file and simple file abstraction
            var simpleFile = new SimpleFile(fileName, stream);
            var simpleFileAbstraction = new SimpleFileAbstraction(simpleFile);
            /////////////////////

            //Create a taglib file from the simple file abstraction
            _songFile = File.Create(simpleFileAbstraction);
            _tag = _songFile.Tag;
        }

        public string GetAuthor()
        {
            var author = string.Empty;
            if (_tag.Performers != null && _tag.Performers.ToList().Any(t => t != ""))
            {
                author = string.Join(", ", _tag.Performers);
            }
            else
            {
                author = _fileInfo.Directory.Parent.Name;
            }

            return author;
        }

        public DateTime GetAlbumYear(string albumName)
        {
            var albumYear = new DateTime(_tag.Year);

            DateTime yearFromAlbumString;

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

        public string GetAlbum()
        {
            var album = _tag.Album;
            if (string.IsNullOrEmpty(album))
            {
                album = _fileInfo.Directory.Name;
            }

            return album;
        }

        public int GetTrackNo()
        {
            return (int)_tag.Track;
        }

        public string GetBitRate()
        {
            var bitRate = _songFile.Properties.AudioBitrate.ToString();

            bitRate = bitRate.EndsWith("000") ?
                bitRate.Substring(0, bitRate.Length - 3)
                : bitRate;
            bitRate += "kbps";

            return bitRate;
        }

        public string GetGenre()
        {
            return string.Join(", ", _tag.Genres);
        }

        public TimeSpan GetDuration()
        {
            var duration = _songFile.Properties.Duration;

            return duration;
        }

        public string GetSongName(string author)
        {
            var title = _tag.Title;

            if(string.IsNullOrEmpty(title))
            {
                title = _fileInfo.Name;
            }

            return title;
        }

        public void Dispose()
        {
            _songFile.Dispose();
        }

        public string GetImagePath(string relativePath)
        {
            var directoryHelper = new DirectoryHelper();

            var filters = new string[] 
            { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };

            return string.Join(";", directoryHelper
                .GetFiles(_fileInfo.Directory, filters, true));
        }
    }
}
