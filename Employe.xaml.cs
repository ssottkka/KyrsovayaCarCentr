using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
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
    public partial class Employe : Window
    {
        private int _employeId;
        private decimal _price;
        private int _paymentMethodId;
        private int _carId;
        public Employe(int employeeId, decimal price, int paymentMethodId, int carId)
        {
            InitializeComponent();
            LoadCarsData();
            _price = price;
            _paymentMethodId = paymentMethodId;
            _carId = carId;
            _employeId = employeeId;
        }
        private void LoadCarsData()
        {
            using (var db = new АвтосалонEntities1())
            {
                var deals = db.Сделка
                    .Where(d => d.Код_сотрудника == 2)
                    .Select(d => new
                    {
                        d.Дата_сделки,
                        d.Сумма_сделки
                    })
                    .ToList();
                DealsList.ItemsSource = deals;
            }
            using (var context = new АвтосалонEntities1())
            {
                var cars = context.Автомобили
                    .Where(a => a.Код_состояния != context.Состояние.FirstOrDefault(s => s.Название == "Продан").Код_состояния)
                    .Select(a => new
                    {
                        Марка = context.Марка.FirstOrDefault(m => m.Код_марки == a.Код_марки).Название,
                        Модель = context.Модель.FirstOrDefault(m => m.Код_марки == a.Код_марки).Название,
                        СТС = a.СТС,
                        Комплектация = context.Комплектация.FirstOrDefault(k => k.Код_комплектации == a.Код_комплектации).Название,
                        Цена = a.Цена,
                        Состояние = context.Состояние.FirstOrDefault(s => s.Код_состояния == a.Код_состояния).Название,
                        Цвет = context.Цвет.FirstOrDefault(c => c.Код_цвета == a.Код_цвета).Название,
                        Количество_владельцев = a.Количество_владельцев,
                        Год_выпуска = a.Год_выпуска
                    })
                    .ToList();

                CarsList.ItemsSource = cars;
            }
        }
        private void btnBuyCar_Click(object sender, RoutedEventArgs e)
        {
           
            EmployeBuyCar employeBuyCar = new EmployeBuyCar(_employeId);
            employeBuyCar.Show();   
            this.Close();
        }
        private void showHistory_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnsellCar_Click(object sender, RoutedEventArgs e)
        {
            EmployeSellCar employeSellCar = new EmployeSellCar(_employeId);
            employeSellCar.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeExit employeExit = new EmployeExit();
            employeExit.Show();
            this.Close();
        }
        private void CarsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}