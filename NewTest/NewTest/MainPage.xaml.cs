using NewTest.Entity;
using NewTest.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NewTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private static string API_LOGIN = "http://2-dot-backup-server-002.appspot.com/_api/v2/members/authentication";
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<String, String> LoginInfor = new Dictionary<string, string>();
            LoginInfor.Add("email", this.Email.Text);
            LoginInfor.Add("password", this.Password.Password);

            // Lay token
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(LoginInfor), System.Text.Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(API_LOGIN, content).Result;
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseContent);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                // save file...
                Debug.WriteLine(responseContent);
                // Doc token
                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                // Luu token
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile file = await folder.CreateFileAsync("token.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, responseContent);

                // Lay thong tin ca nhan bang token.
                HttpClient client2 = new HttpClient();
                client2.DefaultRequestHeaders.Add("Authorization", "Basic " + token.token);
                var resp = client2.GetAsync("http://2-dot-backup-server-002.appspot.com/_api/v2/members/information").Result;
                Debug.WriteLine(await resp.Content.ReadAsStringAsync());
                this.Frame.Navigate(typeof(View.ViewPage));
            }
            else
            {
                // Xu ly loi.
                ErrorResponse errorObject = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                if (errorObject != null && errorObject.error.Count > 0)
                {
                    foreach (var key in errorObject.error.Keys)
                    {
                        var textMessage = this.FindName(key);
                        if (textMessage == null)
                        {
                            continue;
                        }
                        TextBlock textBlock = textMessage as TextBlock;
                        textBlock.Text = errorObject.error[key];
                        textBlock.Visibility = Visibility.Visible;
                    }
                }
            }

            if (this.Email.Text == "")
            {
                this.Message_Email.Text = "Please enter email";
            }
            if (this.Password.ToString() == "")
            {
                this.Message_Password.Text = "Please enter password";
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Accounts.Register));
        }
    }
}
