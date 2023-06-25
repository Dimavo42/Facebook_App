using System.Collections.Generic;

namespace MyUser
{
    public interface IUser
    {
        void Login();
        string GetUserName();
        void SetUserFileds();
        void ConnectToFacebook(string i_AcessesToken);
        List<KeyValuePair<string, string>> FetchWeatherByFacebookLocation();
        bool LoggedInUser { get; }

    }
}
