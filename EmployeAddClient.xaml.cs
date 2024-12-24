using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    public partial class EmployeAddClient : Window
    {
        private int _employeId;
        private decimal _price;
        private int _paymentMethodId;
        private int _carId;
        public EmployeAddClient(int employeId, decimal price, int paymentMethodId, int carId)
        {
            InitializeComponent();
            _employeId = employeId;
            _price = price;
            _paymentMethodId = paymentMethodId;
            _carId = carId;
        }
        private void btnSaveCar_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new АвтосалонEntities1())
            {
                var сотрудник = context.Сотрудники.Find(_employeId);
                if (сотрудник == null)
                {
                    MessageBox.Show("Указанный код сотрудника не существует.");
                    return;
                }
                var паспорт = new Паспорт
                {
                    Серия = txtСерияПаспорта.Text,
                    Номер = txtНомерПаспорта.Text,
                    Кем_выдан = txtКемВыдан.Text,
                    Дата_выдачи = dpДатаВыдачи.SelectedDate.Value,
                    Дата_рождения = dpДатаРождения.SelectedDate.Value,
                    Место_прописки = txtМестоПрописки.Text,
                    Место_рождения = txtМестоРождения.Text
                };
                context.Паспорт.Add(паспорт);
                context.SaveChanges();
                var клиент = new Клиенты
                {
                    Имя = txtИмяКлиента.Text,
                    Фамилия = txtФамилияКлиента.Text,
                    Телефон = txtТелефонКлиента.Text,
                    Email = txtEmailКлиента.Text,
                    Код_паспорта = паспорт.Код,
                    Отчество = txtОтчествоКлиента.Text
                };
                context.Клиенты.Add(клиент);
                context.SaveChanges();
                var сделка = new Сделка
                {
                    Код_сотрудника = _employeId, 
                    Код_клиента = клиент.Код_клиента,
                    Дата_сделки = DateTime.Now,
                    Сумма_сделки = _price,
                    Код_Способ_оплаты = _paymentMethodId,
                    Срок_гарантии = 12, 
                    Код_авто = _carId
                };
             
                context.Сделка.Add(сделка);
                context.SaveChanges();
                var финансоваяОперация = new Финансовые_операции
                {
                    Код_сделки = сделка.Код_сделки,
                    Сумма = сделка.Сумма_сделки,
                    Дата = сделка.Дата_сделки
                };

            
                context.Финансовые_операции.Add(финансоваяОперация);
                // Сохранение изменений
                context.SaveChanges();
                MessageBox.Show("Клиент успешно добавлен.");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Employe employe = new Employe(_employeId,_price,_carId,_paymentMethodId);
            employe.Show();
            this.Close();
        }
    }
}
