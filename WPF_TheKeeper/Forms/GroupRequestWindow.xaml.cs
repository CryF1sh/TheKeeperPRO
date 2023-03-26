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
using WPF_TheKeeper.Entities;
using System.Data.Entity.Migrations;
using System.Media;

namespace WPF_TheKeeper.Forms
{
    /// <summary>
    /// Логика взаимодействия для GroupRequestWindow.xaml
    /// </summary>
    public partial class GroupRequestWindow : Window
    {
        private Request _request = new Request();
        public GroupRequestWindow(Request request, int role)
        {
            InitializeComponent();
            _request = request;
            List<PurposeOfVisit> purposeOfVisits = TheKeeperPRODataBaseEntities.GetContext().PurposeOfVisits.ToList();
            cbxCelPosesheniy.ItemsSource = purposeOfVisits;
            cbxCelPosesheniy.SelectedIndex = purposeOfVisits[purposeOfVisits.Count - 1].id;
            List<Division> divisions = TheKeeperPRODataBaseEntities.GetContext().Divisions.ToList();
            cbxPodrazdeleniy.ItemsSource = divisions;
            cbxPodrazdeleniy.SelectedIndex = divisions[purposeOfVisits.Count - 1].id;
            List<StatusRequest> statusRequests = TheKeeperPRODataBaseEntities.GetContext().StatusRequests.ToList();
            CbxStatus.ItemsSource = statusRequests;
            CbxStatus.SelectedIndex = statusRequests[purposeOfVisits.Count - 1].id;
            DataContext = _request;
            if (role == 2)
            {
                BtnSave.Visibility = Visibility.Hidden;
                BtnCancel.Visibility = Visibility.Hidden;
            }
            if (role == 1)
            {
                BtnOpen.Visibility = Visibility.Hidden;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заявка была обновлена!");
            TheKeeperPRODataBaseEntities.GetContext().Requests.AddOrUpdate(_request);
            TheKeeperPRODataBaseEntities.GetContext().SaveChanges();
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Изменения были отменены!");
            this.Close();
        }
        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            Request request = TheKeeperPRODataBaseEntities.GetContext().Requests.FirstOrDefault();
            SystemSounds.Exclamation.Play();
            MessageBox.Show("Турникет открыт");
            request.ArrivalDate = DateTime.Now;
            TheKeeperPRODataBaseEntities.GetContext().Requests.AddOrUpdate(_request);
            TheKeeperPRODataBaseEntities.GetContext().SaveChanges();
            this.Close();
        }
    }
}
