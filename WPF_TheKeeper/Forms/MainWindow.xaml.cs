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

namespace WPF_TheKeeper.Forms
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Request _currentRequest = new Request();

        public MainWindow(Employee employee)
        {
            InitializeComponent();
            MainFrame.Navigate(new PageList(employee.AccessCode));
            Manager.MainFrame = MainFrame;
            using (var db = new TheKeeperPRODataBaseEntities())
            {
                Role role = db.Roles.AsNoTracking().FirstOrDefault(u => u.id == employee.id);
            }
            DataContext = _currentRequest;
        }

        
    }
}
