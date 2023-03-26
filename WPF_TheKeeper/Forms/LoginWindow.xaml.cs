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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxCode.Text))
            {
                MessageBox.Show("Введите код");
                return;
            }

            int textcode = int.Parse(TbxCode.Text);
            using (var db = new TheKeeperPRODataBaseEntities()) 
            {
                var code = db.Employees.AsNoTracking().FirstOrDefault(u => u.AccessCode == textcode);
                if (code == null)
                {
                    MessageBox.Show("Такого кода не существует!"); return;
                }

                MainWindow mainWindow = new MainWindow(code);
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
