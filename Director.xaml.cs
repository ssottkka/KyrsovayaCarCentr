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
    public partial class Director : Window
    {
        public Director()
        {
            InitializeComponent();
            LoadCarsData();
        }
        private void LoadCarsData()
        {
            using (var db = new АвтосалонEntities1())
            {
                var deals = db.Сделка
                    .Where(d => d.Код_сотрудника == 2)
                    .Join(db.Сотрудники, d => d.Код_сотрудника, s => s.Код_сотрудника, (d, s) => new { Deal = d, Employee = s })
                    .Join(db.Клиенты, ds => ds.Deal.Код_клиента, c => c.Код_клиента, (ds, c) => new { ds.Deal, ds.Employee, Client = c })
                    .Join(db.Способ_оплаты, dsc => dsc.Deal.Код_Способ_оплаты, p => p.Код_Способ_оплаты, (dsc, p) => new
                    {
                        Сумма_сделки = dsc.Deal.Сумма_сделки,
                        Сотрудник_фамилия = dsc.Employee.Фамилия,
                        Клиент_фамилия = dsc.Client.Фамилия,
                        Дата_сделки = dsc.Deal.Дата_сделки,
                        Способ_оплаты = p.Название
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
            private void Button_Click(object sender, RoutedEventArgs e)
        {
                MainWindow main = new MainWindow();
            main.Show();    
            this.Close();
        }
        private void CarsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void DealsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void showHistory_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnBuyCar_Click(object sender, RoutedEventArgs e)
        {
        }
        private void DirectorAddEmploye_Click(object sender, RoutedEventArgs e)
        {
            DirectorAddEmploye directorAddEmploye = new DirectorAddEmploye();   
            directorAddEmploye.Show();
            this.Close();
        }
        private void DirectorDeleteEmploye_Click(object sender, RoutedEventArgs e)
        {
            DirectorDeleteEmploye directorDeleteEmploye = new DirectorDeleteEmploye();  
            directorDeleteEmploye.Show();
            this.Close ();  
        }
        private void Employes_Click(object sender, RoutedEventArgs e)
        {
            DirectorShowEmployes directorShowEmployes = new DirectorShowEmployes();
            directorShowEmployes.Show();    
            this.Close();
        }
        private void DirectorChangeEmploye_Click(object sender, RoutedEventArgs e)
        {
            DirectorChangeEmploye  directorChangeEmploye = new DirectorChangeEmploye(); 
            directorChangeEmploye.Show();
            this.Close();
        }
    }
}
