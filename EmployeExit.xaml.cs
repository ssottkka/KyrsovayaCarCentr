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

namespace CARCENTRdb
{
    /// <summary>
    /// Логика взаимодействия для EmployeExit.xaml
    /// </summary>
    public partial class EmployeExit : Window
    {
        public EmployeExit()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            
            main.Show();
            this.Close();           
        }
    }
}
