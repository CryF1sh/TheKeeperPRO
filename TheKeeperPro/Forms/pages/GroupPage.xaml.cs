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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheKeeperPro.Class;
using TheKeeperPro.InteractionWithAPI;

namespace TheKeeperPro.Forms.Pages
{
    /// <summary>
    /// Логика взаимодействия для GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        private RequestToView requestToView;
        public GroupPage(RequestToView request)
        {
            InitializeComponent();
            this.DataContext = request;
            requestToView = request;
        }

        private async void FillingForm(object sender, RoutedEventArgs e)
        {
            List<VisitPurpose> purposes = (await GetSimpleDataFromAPI.GetAllVisitPurpose()).ToList();
            cmbxPurpous.ItemsSource = purposes;
            cmbxPurpous.SelectedIndex = purposes.FindIndex(a => a.visitPurposeId == requestToView.visitPurpouseId);

            List<Division> divisions = (await GetSimpleDataFromAPI.GetAllDivisions()).ToList();
            cmbxDevision.ItemsSource = divisions;
            cmbxDevision.SelectedIndex = divisions.FindIndex(a => a.divisionId == requestToView.employeeDivisionId);

            List<Employer> employers = (await GetSimpleDataFromAPI.GetAllEmployers()).ToList();
            cmbxEmployers.ItemsSource = employers;
            cmbxEmployers.SelectedIndex = employers.FindIndex(a => a.employeeId == requestToView.employeeId);
        }
    }
}
