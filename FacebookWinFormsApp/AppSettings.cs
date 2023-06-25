using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace MyFBApp
{
    [Serializable]
    public class ItemKeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    [XmlRoot(ElementName = "AppSettings")]
    public sealed class AppSettings
    {
        private readonly string r_FilePath;
        private static AppSettings s_Instance;
        public Point LastWindowLocation { get; set; }
        public Size LastWindowSize { get; set; }
        public bool RememberUser { get; set; }
        public string LastAccessToken { get; set; }
        public List<ItemKeyValue> WeatherItems { get; set; }
        public bool ShowingUserAlbums { get; set; }
        public bool ShowingUserFriends { get; set; }
        public bool ShowingUserEvents { get; set; }
        public bool WeatherByFacebookOrDefault { get; set; }

        private AppSettings()
        {
            LastWindowLocation = new Point(20, 50);
            LastWindowSize = new Size(1000, 500);
            RememberUser = false;
            LastAccessToken = null;
            r_FilePath = getFileDir();
        }

        private string getFileDir()
        {
            return Path.Combine(Environment.CurrentDirectory, "AppSettings.xml"); ;
        }

        public void SaveToFile()
        {
            FileMode mode = File.Exists(r_FilePath) ? FileMode.Truncate : FileMode.Create;
            using (Stream stream = new FileStream(r_FilePath, mode))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(GetType());
                xmlSerializer.Serialize(stream, this);
            }

        }

        public static AppSettings LoadDefaultSettings()
        {
            if (s_Instance == null)
            {
                s_Instance = new AppSettings();
                if (File.Exists(s_Instance.r_FilePath))
                {
                    using (Stream stream = new FileStream(s_Instance.r_FilePath, FileMode.Open))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppSettings));
                        s_Instance = (AppSettings)xmlSerializer.Deserialize(stream);
                    }
                }
            }
            return s_Instance;
        }

        public void DeleteFile()
        {
            if (File.Exists(r_FilePath))
            {
                File.Delete(r_FilePath);
            }
        }
    }

}

