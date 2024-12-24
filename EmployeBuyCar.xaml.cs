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
    public partial class EmployeBuyCar : Window
    {
        private int _employeeId;

        public EmployeBuyCar(int employeeId)
        {
            InitializeComponent();
            LoadComboBoxData();
            _employeeId = employeeId;
        }
        private void LoadComboBoxData()
        {
            using (var context = new АвтосалонEntities1())
            {
                cbМарка.ItemsSource = context.Марка.ToList();
                cbЦвет.ItemsSource = context.Цвет.ToList();
                cbКомплектация.ItemsSource = context.Комплектация.ToList();
                cbСпособОплаты.ItemsSource = context.Способ_оплаты.ToList();
                cbСостояние.ItemsSource = context.Состояние.ToList();
                cbМарка.DisplayMemberPath = "Название";
                cbМарка.SelectedValuePath = "Код_марки";
                cbЦвет.DisplayMemberPath = "Название";
                cbЦвет.SelectedValuePath = "Код_цвета";
                cbКомплектация.DisplayMemberPath = "Название";
                cbКомплектация.SelectedValuePath = "Код_комплектации";
                cbСпособОплаты.DisplayMemberPath = "Название";
                cbСпособОплаты.SelectedValuePath = "Код_Способ_оплаты";
                cbСостояние.DisplayMemberPath = "Название";
                cbСостояние.SelectedValuePath = "Код_состояния";
            }
        }
        private void cbМарка_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedМарка = cbМарка.SelectedItem as Марка;
            if (selectedМарка != null)
            {
                using (var context = new АвтосалонEntities1())
                {
                    cbМодель.ItemsSource = context.Модель.Where(m => m.Код_марки == selectedМарка.Код_марки).ToList();
                    cbМодель.DisplayMemberPath = "Название";
                    cbМодель.SelectedValuePath = "Код_модели";
                }
            }
        }
        private void btnSaveCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new АвтосалонEntities1())
                {
                    var марка = cbМарка.SelectedItem as Марка;
                    var цвет = cbЦвет.SelectedItem as Цвет;
                    var комплектация = cbКомплектация.SelectedItem as Комплектация;
                    var способОплаты = cbСпособОплаты.SelectedItem as Способ_оплаты;
                    var состояние = cbСостояние.SelectedItem as Состояние;

                    if (марка == null || цвет == null || комплектация == null || способОплаты == null || состояние == null)
                    {
                        MessageBox.Show("Неверно указана марка, цвет, комплектация, способ оплаты или состояние.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtГодВыпуска.Text) ||
                        string.IsNullOrWhiteSpace(txtЦена.Text) ||
                        string.IsNullOrWhiteSpace(txtКоличествоВладельцев.Text) ||
                        string.IsNullOrWhiteSpace(txtСТС.Text) ||
                        string.IsNullOrWhiteSpace(txtПТС.Text))
                    {
                        MessageBox.Show("Заполните все обязательные поля.");
                        return;
                    }

                    var сотрудник = context.Сотрудники.Find(_employeeId);
                    if (сотрудник == null)
                    {
                        MessageBox.Show("Указанный код сотрудника не существует.");
                        return;
                    }
                    var автомобиль = new Автомобили
                    {
                        Код_марки = марка.Код_марки,
                        Код_цвета = цвет.Код_цвета,
                        Код_комплектации = комплектация.Код_комплектации,
                        Год_выпуска = int.Parse(txtГодВыпуска.Text),
                        Цена = decimal.Parse(txtЦена.Text),
                        Количество_владельцев = int.Parse(txtКоличествоВладельцев.Text),
                        Код_состояния = состояние.Код_состояния,
                        СТС = txtСТС.Text,
                        ПТС = txtПТС.Text
                    };
                    context.Автомобили.Add(автомобиль);
                    context.SaveChanges();
                    EmployeAddClient employeAddClient = new EmployeAddClient(_employeeId, decimal.Parse(txtЦена.Text), способОплаты.Код_Способ_оплаты, автомобиль.Код_авто);
                    employeAddClient.Show();
                    this.Close();

                    MessageBox.Show("Автомобиль успешно добавлен.");
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
