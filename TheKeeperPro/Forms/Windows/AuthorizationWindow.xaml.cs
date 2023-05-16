using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TheKeeperPro.Class;
//using Newtonsoft.Json;
using System.Net.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.IO;

namespace TheKeeperPro.Forms.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();

        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Autorizait();
        }

        private async Task Autorizait()
        {
            PassingEmployee passingEmployee = new PassingEmployee();
            passingEmployee.Id = int.Parse(passbxEmployeeCode.Password);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(passingEmployee);
            if(UserAuthenticationTest.CodeAuthentication(await AddressingAPIService(json)))
            {
                MainWindow mainWindow = new MainWindow(authorizedEmployer);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Как видишь такого пользователя нет");
            }
        }

        public Employer authorizedEmployer;
        static HttpClient client = new HttpClient();
        private async Task<bool> AddressingAPIService(string json)
        {
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Properties.Resources.ConnectionString.ToString() + "User/UserVerification";
            var response = await client.PostAsync(url, data);
            var strem = await response.Content.ReadAsStreamAsync();
            var result = new StreamReader(strem, Encoding.UTF8);
            string str = result.ReadToEnd();
            var employer = JsonSerializer.Deserialize<Employer>(str); //JsonConvert.DeserializeObject<PassingEmployee>(result);
            if (employer != null)
            {
                authorizedEmployer = employer;
                return true;                
            }
            else { return false; }
        }

        private void btnKiriill_Click(object sender, RoutedEventArgs e)
        {
            KirillWindow kirillWindow = new KirillWindow();
            kirillWindow.ShowDialog(); 
        }
    }
}
