using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheKeeperPro.Class;
using TheKeeperPro.Forms.Windows;
using TheKeeperPro.InteractionWithAPI;
using TheKeeperProWebAPI.ResponesEntities;
using static System.Net.Mime.MediaTypeNames;

namespace TheKeeperPro.Forms.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListRequestPage.xaml
    /// </summary>
    public partial class ListRequestPage : Page
    {
        public ListRequestPage()
        {
            InitializeComponent();
        }
        private async void ListRequestPageOnLoaded(object sender, RoutedEventArgs e)
        {
            lvRequest.ItemsSource = await GetSimpleDataFromAPI.GetAllRequestFromAPI();
            cbbxType.ItemsSource = GetSimpleDataFromAPI.GetAllType();
            cbbxDivision.ItemsSource = await GetSimpleDataFromAPI.GetAllDivisions();
            cbbxStatus.ItemsSource = await GetSimpleDataFromAPI.GetAllStatuses();
        }

        private void ListViewOnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RequestToView request = (sender as ListViewItem).Content as RequestToView;

            if (request.groupRequest) Manager.MainFrame.Navigate(new GroupPage(request));
            else Manager.MainFrame.Navigate(new SoloRequestPage());
        }
    }
}