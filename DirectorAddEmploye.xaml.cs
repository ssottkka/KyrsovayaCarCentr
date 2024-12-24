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
    public partial class DirectorAddEmploye : Window
    {
        public DirectorAddEmploye()
        {
            InitializeComponent();
            LoadComboBoxData();
        }
        private void LoadComboBoxData()
        {
            using (var context = new АвтосалонEntities1())
            {
                var positions = context.Должности.ToList();
                TypeBox.ItemsSource = positions;
            }
        }
        private void TypeBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selectedДолжность = TypeBox.SelectedItem as Должности;
            if (TypeBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите должность.");
                return;
            }
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
                    int typeId = (int)TypeBox.SelectedValue;
                    var сотрудник = new Сотрудники
                    {
                        Имя = txtИмяКлиента.Text,
                        Фамилия = txtФамилияКлиента.Text,
                        Телефон = txtТелефонКлиента.Text,
                        Код_паспорта = паспорт.Код,
                        Отчество = txtОтчествоКлиента.Text,
                        Код_должности = typeId,
                        Логин = Login.Text,
                        Пароль = Password.Password
                    };
                    context.Сотрудники.Add(сотрудник);
                    context.SaveChanges();
                    MessageBox.Show("Сотрудник добавлен");
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
            Director director = new Director();
            director.Show();    
            this.Close();   
        }    
    }
}
