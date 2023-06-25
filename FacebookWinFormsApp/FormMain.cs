using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using MyUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MyFBApp
{
    public partial class FormMain : Form
    {
        private readonly AppSettings r_myAppSetting;
        private FBUser m_User;
        private readonly CompositeReport r_Reporter;

        private void checkWeatherStatus()
        {
            itemKeyValueDataGridView.Visible = r_myAppSetting.ShowingWeather;
        }

        public FormMain()
        {
            InitializeComponent();
            FacebookService.s_CollectionLimit = 100;
            StartPosition = FormStartPosition.Manual;
            m_User = new FBUser(new User());
            r_myAppSetting = m_User.GetDefaultSettings();
            Size = r_myAppSetting.LastWindowSize;
            Location = r_myAppSetting.LastWindowLocation;
            saveCheckBox.Checked = r_myAppSetting.RememberUser;
            itemKeyValueDataGridView.Visible = false;
            r_Reporter = new CompositeReport();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (!r_myAppSetting.RememberUser || string.IsNullOrEmpty(r_myAppSetting.LastAccessToken))
            {
                buttonLogin.Enabled = true;
                return;
            }
            new Thread(() =>
            {
                m_User.ConnectToFacebook(r_myAppSetting.LastAccessToken);
                Invoke(new Action(() => { restoreUserOldSettings(); }));
            }).Start();
        }



        private void restoreUserOldSettings()
        {
            fetchUserInformation();
            if (r_myAppSetting.WeatherItems.Count > 0)
            {
                initWeatherListView();
            }
            if (r_myAppSetting.ShowingUserFirends)
            {
                fetchFriends();
            }
            if (r_myAppSetting.ShowingUserAlbums)
            {
                fetchAlbums();
            }
            if (r_myAppSetting.ShowingUserEvents)
            {
                fetchEvents();
            }
        }

        private void initWeatherListView()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            foreach (ItemKeyValue item in r_myAppSetting.WeatherItems)
            {
                list.Add(new KeyValuePair<string, string>(item.Key, item.Value));
            }
            weatherItemsBindingSource.DataSource = list;
            checkWeatherStatus();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            r_myAppSetting.RememberUser = saveCheckBox.Checked;
            m_User?.LogoutAndSearlize(Location, Size);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            new Thread(() =>
            {
                Invoke(new Action(() => m_User.Login()));
                if (m_User.LoggedInUser)
                {
                    fetchUserInformation();
                }
            }).Start();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Pressing accept will result of exiting the app are you sure?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }
            m_User.LogoutFBUser();
            cleanForm();
        }

        private void cleanForm()
        {
            fBUserBindingSource.Clear();
            albumsListBox.Items.Clear();
            userFriendsListBox.Items.Clear();
            eventListBox.Items.Clear();
            itemKeyValueDataGridView.DataSource = null;
            eventsBindingSource.DataSource = null;
            userPanel.Visible = false;
            m_User = null;
            saveCheckBox.Checked = false;
            Close();
        }

        private void rememberUserCheckBox_Click(object sender, EventArgs e)
        {
            r_myAppSetting.RememberUser = !r_myAppSetting.RememberUser;
        }

        private void fetchUserInformation()
        {
            userPanel.Invoke(new Action(() =>
            {
                fBUserBindingSource.DataSource = m_User;
                r_Reporter.Add(m_User);
                userPanel.Visible = true;
                buttonLogin.Text = string.Format("Logged in as {0}", m_User.GetUserName());
                buttonLogout.Enabled = true;
                buttonLogin.Enabled = false;
            }));
        }

        private bool isValidText(string i_Text)
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(i_Text))
            {
                Regex regex = new Regex("^[a-zA-Z]+([\\s][a-zA-Z]+)?$");
                isValid = regex.IsMatch(i_Text);
            }
            return isValid;
        }

        private void fetchAlbums()
        {
            new Thread(() =>
            {
                List<Album> userAlbums = m_User?.GetAlbums();
                if (userAlbums?.Count <= 0)
                {
                    MessageBox.Show("No Albums to retrieve :(");
                }
                else
                {
                    albumsListBox.Invoke(new Action(() =>
                    {
                        albumsListBox.DisplayMember = "Name";
                        albumLabel.Visible = true;
                        r_myAppSetting.ShowingUserAlbums = true;
                        foreach (Album album in userAlbums)
                        {
                            if (!albumsListBox.Items.Contains(album))
                            {
                                albumsListBox.Items.Add(album);
                            }
                        }
                    }));
                }
            }).Start();
        }

        private void onAlbumPictures_Click(object sender, EventArgs e)
        {
            if (!m_User.LoggedInUser)
            {
                MessageBox.Show("Error: please log in~!");
                return;
            }
            fetchAlbums();
        }

        private void albumsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (albumsListBox.SelectedItems.Count != 1)
            {
                return;
            }
            albumLabel.Visible = true;
            albumPanel.Visible = true;
            Album selectedAlbum = (Album)albumsListBox.SelectedItem;
            albumBindingSource.DataSource = selectedAlbum;
        }

        private void albumsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (albumsListBox.SelectedItems.Count == 1)
            {
                Album selectedAlbum = (Album)albumsListBox.SelectedItem;
                Thread thread = new Thread(() =>
                {
                    UserImages images = new UserImages(selectedAlbum);
                    Invoke(new Action(() =>
                    {
                        if (images.Count > 0)
                        {
                            ImageListForm imageList = ImageListForm.CreateInstance(images);
                            if (!r_Reporter.CheckIfSubScribed(imageList))
                            {
                                r_Reporter.Add(imageList);
                            }
                            imageList.Show();
                        }
                    }));
                });
                thread.Start();
            }
        }

        private void showTemperatureButton_Click(object sender, EventArgs e)
        {
            if (!m_User.LoggedInUser)
            {
                MessageBox.Show("Error: please log in~!");
                return;
            }
            r_myAppSetting.ShowingWeather = !r_myAppSetting.ShowingWeather;
            checkWeatherStatus();
            if (r_myAppSetting.ShowingWeather && isValidText(locationTextBox.Text))
            {
                new Thread(() =>
                {
                    List<KeyValuePair<string, string>> currentWeather = m_User.FetchWeatherByFacebookLocation();
                    Invoke(new Action(() =>
                    {
                        if (currentWeather.Count > 0)
                        {
                            weatherItemsBindingSource.DataSource = currentWeather;
                            r_myAppSetting.WeatherItems = currentWeather.Select(item => new ItemKeyValue { Key = item.Key, Value = item.Value }).ToList();
                        }
                    }));
                }).Start();
            }
            else if (!isValidText(locationTextBox.Text))
            {
                MessageBox.Show("Cant show");
            }
            else
            {
                r_myAppSetting.WeatherItems.Clear();
            }
        }

        private void report_Click(object sender, EventArgs e)
        {
            if (r_Reporter.HasReport())
            {

                MessageBox.Show(r_Reporter.Report());
                r_Reporter.ClearReports();
            }
            else
            {
                MessageBox.Show("No reports to show");
            }
        }

        private void fetchFriends()
        {
            new Thread(() =>
            {
                List<User> userFriends = m_User?.GetFriendes();
                if (userFriends?.Count <= 0)
                {
                    MessageBox.Show("No friends to retrieve :(");
                }
                userFriendsListBox.Invoke(new Action(() =>
                {
                    userFriendsListBox.DisplayMember = "Name";
                    foreach (User friend in userFriends)
                    {
                        if (!userFriendsListBox.Items.Contains(friend))
                        {
                            userFriendsListBox.Items.Add(friend);
                        }
                    }
                    r_myAppSetting.ShowingUserFirends = true;
                }));
            }).Start();
        }

        private void showFriendsButton_Click(object sender, EventArgs e)
        {
            if (!m_User.LoggedInUser)
            {
                MessageBox.Show("Error: please log in~!");
                return;
            }
            fetchFriends();
        }

        private void userFriendsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userFriendsListBox.SelectedItems.Count != 1)
            {
                return;
            }
            userFriendsPanel.Visible = true;
            userBindingSource.DataSource = (User)userFriendsListBox.SelectedItem;

        }

        private void showEvents_Click(object sender, EventArgs e)
        {
            if (!m_User.LoggedInUser)
            {
                MessageBox.Show("Error: please log in~!");
                return;
            }
            fetchEvents();
        }

        private void fetchEvents()
        {
            new Thread(() =>
            {
                List<Event> userEvents = m_User?.GetEvents();
                if (userEvents.Count <= 0)
                {
                    MessageBox.Show("No Events to retrieve :(");
                    return;
                }
                eventListBox.Invoke(new Action(() =>
                {
                    r_myAppSetting.ShowingUserEvents = true;
                    foreach (Event myEvent in userEvents)
                    {
                        if (!eventListBox.Items.Contains(myEvent))
                        {
                            eventListBox.Items.Add(myEvent);
                        }
                    }
                }));
            }).Start();
        }

        private void eventListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventListBox.SelectedItems.Count != 1)
            {
                return;
            }
            eventListBoxPanel.Visible = true;
            eventsBindingSource.DataSource = (Event)eventListBox.SelectedItem;
        }
    }
}
