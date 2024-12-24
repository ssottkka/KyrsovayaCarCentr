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
    /// Логика взаимодействия для EmployeBuyCarExit.xaml
    /// </summary>
    public partial class EmployeBuyCarExit : Window
    {
        private int _employeeId;
        private decimal _price;
        private int _paymentMethodId;
        private int _carId;
        public EmployeBuyCarExit(int employeeId, decimal price, int paymentMethodId, int carId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            _price = price;
            _paymentMethodId = paymentMethodId;
            _carId = carId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        
            this.Close();   

        }
    }
}
