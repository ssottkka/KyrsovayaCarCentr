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
    /// Логика взаимодействия для DirectorShowEmployes.xaml
    /// </summary>
    public partial class DirectorShowEmployes : Window
    {
        public DirectorShowEmployes()
        {
            InitializeComponent();
            LoadEmployeesData();
        }
        private void LoadEmployeesData()
        {
            using (var context = new АвтосалонEntities1())
            {
                var employees = context.Сотрудники
                    .Join(context.Должности, s => s.Код_должности, p => p.Код_должности, (s, p) => new { Employee = s, Position = p })
                    .Join(context.Паспорт, sp => sp.Employee.Код_паспорта, ps => ps.Код, (sp, ps) => new
                    {
                        Фамилия = sp.Employee.Фамилия,
                        Имя = sp.Employee.Имя,
                        Отчество = sp.Employee.Отчество,
                        Должность = sp.Position.Название,
                        Логин = sp.Employee.Логин,
                        Серия_паспорта = ps.Серия,
                        Номер_паспорта = ps.Номер,
                        Дата_рождения = ps.Дата_рождения
                    })
                    .ToList();

                EmployeesList.ItemsSource = employees;
            }
        }
        private void CarsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Director director = new Director();
            director.Show();
            this.Close();
        }
    }
}
