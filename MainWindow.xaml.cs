using System;
using System.Collections.Generic;
using System.Diagnostics;
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
namespace CARCENTRdb
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal _price;
        private int _paymentMethodId;
        private int _carId;
        private Автомобили _selectedCar;
        АвтосалонEntities1 db = new АвтосалонEntities1();
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(int price, int paymentMethodId, int carId, Автомобили selectedCar)
        {
            InitializeComponent();
            _price = price;
            _paymentMethodId = paymentMethodId;
            _carId = carId;
            _selectedCar = selectedCar;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Директор
            var director = db.Сотрудники.FirstOrDefault(x => x.Логин == login.Text && x.Пароль == password.Text && x.Код_должности == 3);
            if(director != null)
            {
                Director director1 = new Director();    
                director1.Show();                          
                this.Close();
                using (var db = new АвтосалонEntities1())
                {
                    var historyEntry = new История
                    {
                        Логин = login.Text,
                        Дата = DateTime.Now,
                        Cтатус = "Успешно"
                    };
                    db.История.Add(historyEntry);
                    db.SaveChanges();
                }
            }
            else
            {
                var historyEntry = new История
                {
                    Логин = login.Text,
                    Дата = DateTime.Now,
                    Cтатус = "Неуспешно"
                };
                db.История.Add(historyEntry);
                db.SaveChanges();
            } 
            // Админ
            var admin = db.Сотрудники.FirstOrDefault(x => x.Логин == login.Text && x.Пароль == password.Text && x.Код_должности == 2);
            if (admin != null)
            {
                Admin admin1 = new Admin();
                admin1.Show();                                  
                this.Close();       
                using (var db = new АвтосалонEntities1())
                {
                    var historyEntry = new История
                    {
                        Логин = login.Text,
                        Дата = DateTime.Now,
                        Cтатус = "Успешно"
                    };
                    db.История.Add(historyEntry);
                    db.SaveChanges();
                }
            }
            else
            {
                var historyEntry = new История
                {
                    Логин = login.Text,
                    Дата = DateTime.Now,
                    Cтатус = "Неуспешно"
                };
                db.История.Add(historyEntry);
                db.SaveChanges();
            }
            var employe = db.Сотрудники.FirstOrDefault(x => x.Логин == login.Text && x.Пароль == password.Text && x.Код_должности == 1);
            if (employe != null)
            {
                int employeeId = employe.Код_сотрудника;
                Employe employe1 = new Employe(employeeId, _price, _paymentMethodId, _carId);
                EmployeAddClient employeAddClient = new EmployeAddClient(employeeId, _price, _paymentMethodId, _carId);
                EmployeSellCarAddClient employeSellCarAddClient = new EmployeSellCarAddClient(employeeId,Convert.ToInt32( _price), _paymentMethodId, _carId, _selectedCar);
                employe1.Show();                                                   
                this.Close();
                using (var db = new АвтосалонEntities1())
                {
                    var historyEntry = new История
                    {
                        Логин = login.Text,
                        Дата = DateTime.Now,
                        Cтатус = "Успешно"
                    };
                    db.История.Add(historyEntry);
                    db.SaveChanges();
                }
            }
            else
            {
                var historyEntry = new История
                {
                    Логин = login.Text,
                    Дата = DateTime.Now,
                    Cтатус = "Неуспешно"
                };
                db.История.Add(historyEntry);
                db.SaveChanges();

            }
            if(admin == null && director == null && employe == null)
            {
                MessageBox.Show("Неверный Логин или пароль");                    
            }    
        }
    }
}

