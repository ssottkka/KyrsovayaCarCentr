using System.Windows.Controls;
using System.Windows;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
using System;
namespace CARCENTRdb
{
    public partial class EmployeSellCar : Window
    {
        private АвтосалонEntities1 _context;
        private decimal _serviceCost;
        private int _employeeId;
        public EmployeSellCar(int employeeId)
        {
            InitializeComponent();
            _context = new АвтосалонEntities1();
            LoadData();
            LoadComboBoxData();
            _employeeId = employeeId;   
        }
        private void LoadComboBoxData()
        {
            using (var context = new АвтосалонEntities1())
            {
                cbУслуги.ItemsSource = context.Услуги.ToList();
                cbУслуги.DisplayMemberPath = "Название";
                cbУслуги.SelectedValuePath = "Код_услуги";
               
            }
        }
        private void LoadData()
        {
            using (var context = new АвтосалонEntities1())
            {
                dgАвтомобили.ItemsSource = context.Автомобили
                    .Include(a => a.Марка)
                    .Include(a => a.Цвет)
                    
                    .Where(a => a.Код_состояния != 5)
                    .ToList();
            }       
        }
        private void dgАвтомобили_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCar = dgАвтомобили.SelectedItem as Автомобили;
            if (selectedCar != null)
            {
                using (var context = new АвтосалонEntities1())
                {
                    var carToUpdate = context.Автомобили.Find(selectedCar.Код_авто);
                    if (carToUpdate != null)
                    {
                        carToUpdate.Код_состояния = 5;
                        context.SaveChanges();
                        MessageBox.Show("Состояние автомобиля изменено на 'Продано'.");

                        // Обновление DataGrid
                        dgАвтомобили.ItemsSource = context.Автомобили
                            .Include(a => a.Марка)
                            .Include(a => a.Цвет)
                            .Where(a => a.Код_состояния != 5)
                            .ToList();

                        EmployeSellCarAddClient employeSellCarAddClient = new EmployeSellCarAddClient(_employeeId, selectedCar.Код_авто, selectedCar.Цена, _paymentMethodId, selectedCar);
                        employeSellCarAddClient.Show();
                        
                        this.Hide();
                    }
                }
            }
        }
        private int _carId;
        private decimal _price;
        private int _paymentMethodId;
        private void btnSaleCar_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void cbУслуги_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}