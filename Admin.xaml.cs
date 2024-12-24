using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class Admin : Window
    {
        private ObservableCollection<История> historyCollection;
        private CollectionViewSource historyViewSource;
        public Admin()
        {
            InitializeComponent();
        }
        private void HistoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void btnAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            AdminAddEmploye adminAddEmploye = new AdminAddEmploye();    
            adminAddEmploye.Show(); 
            this.Close();
        }
        private void showHistory_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new АвтосалонEntities1())
            {
                var historyData = context.История.ToList();
                historyCollection = new ObservableCollection<История>(historyData);
               
                historyViewSource = new CollectionViewSource { Source = historyCollection };
                historyViewSource.SortDescriptions.Add(new SortDescription("date", ListSortDirection.Ascending));
                historyDataGrid.ItemsSource = historyViewSource.View;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();    
            this.Close();   
        }
        private void btnFireEmployee_Click(object sender, RoutedEventArgs e)
        {
            AdminDeleteEmploye adminDeleteEmploye = new AdminDeleteEmploye();   
            adminDeleteEmploye.Show();
            this.Close();
        }
        private void btnChangeEmploye_Click(object sender, RoutedEventArgs e)
        {
            AdminChangeEmploye adminChangeEmploye = new AdminChangeEmploye();   
            adminChangeEmploye.Show();
            this.Close();
        }
    }
}
