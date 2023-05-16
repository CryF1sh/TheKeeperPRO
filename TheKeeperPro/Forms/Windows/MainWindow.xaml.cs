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
using TheKeeperPro.Forms.Pages;

namespace TheKeeperPro.Forms.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Employer authorizedEmployer) //Принял авторизированного пользователя
        {
            InitializeComponent();
            employer = authorizedEmployer; //Жёстко прировнял всё это добро к стаическому полю
            passingEmployee.Id = employer.employeeId;
            frmMain.Navigate(new ListRequestPage());
            Manager.MainFrame = frmMain;
        }
        public static Employer Employer { get => employer; } //Статическое поля (Это важно)
        static Employer employer;
        public static PassingEmployee PassingEmployee { get => passingEmployee; }
        static PassingEmployee passingEmployee;
    }
}
