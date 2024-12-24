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
    public partial class AdminChangeEmploye : Window
    {
        public AdminChangeEmploye()
        {
            InitializeComponent(); LoadComboBoxData();
        }
        private void LoadComboBoxData()
        {
            using (var context = new АвтосалонEntities1())
            {
                TypeBox.ItemsSource = context.Должности.ToList();
                TypeBox.DisplayMemberPath = "Название";
                TypeBox.SelectedValuePath = "Код_должности";
            }
        }
        private void TypeBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();   
            this.Close();
        }
        private void btnAddEmploye_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new АвтосалонEntities1())
                {
                    var должности = TypeBox.SelectedItem as Должности;

                    if (должности == null)
                    {
                        MessageBox.Show("Выберите должность.");
                        return;
                    }
                    var сотрудник = context.Сотрудники.FirstOrDefault(s => s.Логин == Login.Text);
                    if (сотрудник == null)
                    {
                        MessageBox.Show("Сотрудник с таким логином не найден.");
                        return;
                    }
                    сотрудник.Фамилия = txtФамилияКлиента.Text;
                    сотрудник.Имя = txtИмяКлиента.Text;
                    сотрудник.Отчество = txtОтчествоКлиента.Text;
                    сотрудник.Телефон = txtТелефонКлиента.Text;
                    сотрудник.Код_должности = должности.Код_должности;
                    сотрудник.Пароль = Password.Password;
                    var паспорт = context.Паспорт.FirstOrDefault(p => p.Код == сотрудник.Код_паспорта);
                    if (паспорт != null)
                    {
                        паспорт.Серия = txtСерияПаспорта.Text;
                        паспорт.Номер = txtНомерПаспорта.Text;
                        паспорт.Кем_выдан = txtКемВыдан.Text;
                        паспорт.Дата_выдачи = dpДатаВыдачи.SelectedDate.Value;
                        паспорт.Дата_рождения = dpДатаРождения.SelectedDate.Value;
                        паспорт.Место_прописки = txtМестоПрописки.Text;
                        паспорт.Место_рождения = txtМестоРождения.Text;
                    }
                    context.SaveChanges();
                    MessageBox.Show("Данные сотрудника обновлены.");
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
    }
}
