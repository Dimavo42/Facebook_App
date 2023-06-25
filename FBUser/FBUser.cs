using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace MyUser
{
    public class FBUser : Exit, IReport, IUser
    {
        private User m_User;
        private readonly AppSettings r_myAppSetting;
        public string Birthday { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string Link { get; private set; }
        public string Locale { get; private set; }
        public string Location { get; private set; }
        public Image ImageNormal { get; private set; }
        public bool LoggedInUser { get; private set; }

        private readonly Dictionary<string, int> r_UserReports;
        public FBUser(User i_User)
        {
            m_User = i_User;
            r_UserReports = new Dictionary<string, int>();
            r_myAppSetting = AppSettings.LoadDefaultSettings();
        }

        public AppSettings GetDefaultSettings()
        {
            return r_myAppSetting;
        }

        public void LogoutFBUser()
        {
            ClearDataFields();
            FacebookService.LogoutWithUI();
            r_myAppSetting.DeleteFile();
        }
        public string GetUserName()
        {
            return FirstName;
        }

        public void SetUserFileds()
        {
            Birthday = m_User.Birthday;
            Email = m_User.Email;
            FirstName = m_User.FirstName;
            Link = m_User.Link;
            Locale = m_User.Locale;
            Location = m_User.Location?.Name ?? "Tel Aviv";
            ImageNormal = m_User.ImageNormal;
        }

        public override void ClearDataFields()
        {
            Birthday = null;
            Email = null;
            FirstName = null;
            Link = null;
            Locale = null;
            ImageNormal = null;
            Location = null;
            r_myAppSetting.LastAccessToken = null;
            m_User = null;
        }
        public void Login()
        {
            LoginResult logingResult = FacebookService.Login(
                   
                   "YourFaceBookLoginID",
                   "email",
                   "public_profile",
                   "user_age_range",
                   "user_birthday",
                   "user_events",
                   "user_friends",
                   "user_gender",
                   "user_hometown",
                   "user_likes",
                   "user_link",
                   "user_location",
                   "user_photos",
                   "user_posts",
                   "user_videos");
            m_User = logingResult?.LoggedInUser ?? null;
            LoggedInUser = m_User != null;
            r_myAppSetting.LastAccessToken = logingResult?.AccessToken ?? null;
            if (LoggedInUser)
            {
                SetUserFileds();
            }
        }
        public override void Logout(Point LastWindowLocation, Size LastWindowSize)
        {
            if (!r_myAppSetting.RememberUser)
            {
                return;
            }
            r_myAppSetting.LastWindowLocation = LastWindowLocation;
            r_myAppSetting.LastWindowSize = LastWindowSize;

        }

        public List<Album> GetAlbums()
        {
            List<Album> albums = new List<Album>(m_User.Albums);
            MakeSelfReport("Number of times clicks on albums");
            return albums;
        }

        public void ConnectToFacebook(string i_AcessesToken)
        {
            LoginResult loginResult = FacebookService.Connect(i_AcessesToken);
            m_User = loginResult.LoggedInUser ?? null;
            LoggedInUser = m_User != null;
            if (LoggedInUser)
            {
                SetUserFileds();
            }
        }

        public List<User> GetFriendes()
        {
            List<User> friends = new List<User>(m_User.Friends);
            MakeSelfReport("Number of friends clicks on events");
            return friends;
        }

        public bool HasReport() => r_UserReports.Count > 0;

        private HttpResponseMessage getResponseMessage(string i_UserLocation)
        {
            string apiKey = "YourApIKEY";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={i_UserLocation}&units=metric&appid={apiKey}").Result;
                return response;
            }
        }

        private List<KeyValuePair<string, string>> weatherSerializer(string i_ResponseWeather)
        {
            List<KeyValuePair<string, string>> weatherItems = new List<KeyValuePair<string, string>>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            WeatherObject jsonObject = serializer.Deserialize<WeatherObject>(i_ResponseWeather);
            WeatherMainObject mainObj = jsonObject.main;
            PropertyInfo[] properties = mainObj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(mainObj, null);
                string stringValue = value != null ? value.ToString() : string.Empty;
                weatherItems.Add(new KeyValuePair<string, string>(property.Name, stringValue));
            }
            return weatherItems;
        }

        public List<KeyValuePair<string, string>> FetchWeatherByFacebookLocation()
        {
            List<KeyValuePair<string, string>> weatherItems = new List<KeyValuePair<string, string>>();
            HttpResponseMessage response = getResponseMessage(Location);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
            else
            {
                weatherItems = weatherSerializer(response.Content.ReadAsStringAsync().Result);
            }
            if (weatherItems.Count > 0)
            {
                MakeSelfReport("Weather API number Of uses");
            }
            return weatherItems;
        }

        public List<Event> GetEvents()
        {
            List<Event> events = new List<Event>(m_User.Events);
            MakeSelfReport("Number of times clicks on events");
            return events;
        }

        public void MakeSelfReport(object i_Sender)
        {
            if (i_Sender is string message)
            {
                r_UserReports[message] = r_UserReports.ContainsKey(message) ? r_UserReports[message] + 1 : 1;

            }
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> pair in r_UserReports)
            {
                sb.AppendLine($"{pair.Key} {pair.Value}");
            }
            return sb.ToString();
        }

        public void ClearReports() => r_UserReports.Clear();

        public override void Searlize()
        {
            r_myAppSetting.SaveToFile();
        }
    }
}
