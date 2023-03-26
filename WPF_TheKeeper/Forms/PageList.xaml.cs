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
using WPF_TheKeeper.Entities;

namespace WPF_TheKeeper.Forms
{
    /// <summary>
    /// Логика взаимодействия для PageList.xaml
    /// </summary>
    public partial class PageList : Page
    {
        public PageList(int role)
        {
            InitializeComponent();
            if (role == 2)
            {
                List<Request> requests = TheKeeperPRODataBaseEntities.GetContext().Requests.ToList();
                for (int i=0; i <requests.Count; i++)
                {
                    if (requests[i].StatusRequestCode != 2) requests.RemoveAt(i);
                }
                DGRequest.ItemsSource = requests;
            }
        }
        private void ButView_Click(object sender, RoutedEventArgs e)
        {
            Request request = (sender as Button).DataContext as Request;
            int code = request.id;
            using (var db = new TheKeeperPRODataBaseEntities())
            {
                Request requests = db.Requests.AsNoTracking().FirstOrDefault(u => u.id == code);
                if (requests.BlackList == false)
                {
                    MessageBox.Show("Заявка отсутсвует в чёрном списке!");
                    if (requests.TypeRequestCode == 1)
                    {
                        SoloRequestWindow soloRequestWindow = new SoloRequestWindow(requests, code);
                        soloRequestWindow.Show();
                    }
                    else
                    {
                        GroupRequestWindow groupRequestWindow = new GroupRequestWindow(requests, code);
                        groupRequestWindow.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Заявка находится в чёрном списке!");
                    return;
                }
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TheKeeperPRODataBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGRequest.ItemsSource = TheKeeperPRODataBaseEntities.GetContext().Requests.ToList();
            }
        }
    }
}
