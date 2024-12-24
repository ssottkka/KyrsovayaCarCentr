using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    public partial class EmployeSellCarAddClient : Window
    {
        private string _carName;
        private int _employeeId;
        private int _carId;
        private decimal _price;
        private int _paymentMethodId;
        private Автомобили _selectedCar;
        public EmployeSellCarAddClient(int employeeId, int carId, decimal price, int paymentMethodId, Автомобили selectedCar)
        {
            InitializeComponent();
            _employeeId = employeeId;
            _carId = carId;
            _price = price; 
            _paymentMethodId = paymentMethodId;
            _selectedCar = selectedCar;
        }

        private void addClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new АвтосалонEntities1())
                {
                    var passport = new Паспорт
                    {
                        Серия = txtСерияПаспорта.Text,
                        Номер = txtНомерПаспорта.Text,
                        Дата_выдачи = dpДатаВыдачи.SelectedDate.Value,
                        Кем_выдан = txtКемВыдан.Text,
                        Дата_рождения = dpДатаРождения.SelectedDate.Value,
                        Место_рождения = "Москва",
                        Место_прописки = "Москва"
                    };
                    context.Паспорт.Add(passport);
                    context.SaveChanges();
                    var client = new Клиенты
                    {
                        Имя = txtИмяКлиента.Text,
                        Фамилия = txtФамилияКлиента.Text,
                        Отчество = txtОтчествоКлиента.Text,
                        Телефон = txtТелефонКлиента.Text,
                        Email = txtEmailКлиента.Text,
                        Код_паспорта = passport.Код
                    };
                    context.Клиенты.Add(client);
                    context.SaveChanges();
                    var deal = new Сделка
                    {
                        Код_сотрудника = _employeeId,
                        Код_клиента = client.Код_клиента,
                        Дата_сделки = DateTime.Now,
                        Сумма_сделки = _selectedCar.Цена,
                        Код_Способ_оплаты = _paymentMethodId, 
                        Срок_гарантии = 12,
                        Код_авто = _selectedCar.Код_авто
                    };
                    context.Сделка.Add(deal);
                    context.SaveChanges();
                    var financialOperation = new Финансовые_операции
                    {
                        Код_сделки = deal.Код_сделки,
                        Сумма = deal.Сумма_сделки,
                        Дата = DateTime.Now
                    };
                    context.Финансовые_операции.Add(financialOperation);
                    context.SaveChanges();
                    MessageBox.Show("Данные клиента успешно сохранены и автомобиль продан!");
                    Employe employe = new Employe(_employeeId,_price,_paymentMethodId,_carId);
                    employe.Show();   
                    this.Close();
                }
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder errors = new StringBuilder();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errors.AppendLine($"Свойство: {validationError.PropertyName}, Ошибка: {validationError.ErrorMessage}");
                    }
                }
                MessageBox.Show($"Ошибка валидации:\n{errors.ToString()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
