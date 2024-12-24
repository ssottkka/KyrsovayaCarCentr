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
    public partial class AdminDeleteEmploye : Window
    {
        public AdminDeleteEmploye()
        {
            InitializeComponent();
            LoadEmployees();
        }
        private void LoadEmployees()
        {
            using (var context = new АвтосалонEntities1())
            {
                var employees = context.Сотрудники.ToList()
                    .Select(e => new
                    {
                        FullName = $"{e.Имя} {e.Фамилия}",
                        Код_сотрудника = e.Код_сотрудника
                    })
                    .ToList();

                cbEmployees.ItemsSource = employees;
                cbEmployees.DisplayMemberPath = "FullName";
                cbEmployees.SelectedValuePath = "Код_сотрудника";
            }
        }
        private void btnFire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedEmployee = cbEmployees.SelectedItem as dynamic;
                if (selectedEmployee == null)
                {
                    MessageBox.Show("Выберите сотрудника для увольнения.");
                    return;
                }

                using (var context = new АвтосалонEntities1())
                {
                    var employeeToFire = context.Сотрудники.Find(selectedEmployee.Код_сотрудника);
                    if (employeeToFire != null)
                    {
                        employeeToFire.Код_должности = 32; 
                        context.Сотрудники.Add(employeeToFire);
                        context.SaveChanges();
                        MessageBox.Show("Сотрудник уволен.");
                        LoadEmployees();
                    }
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
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        }
        private void cbEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
