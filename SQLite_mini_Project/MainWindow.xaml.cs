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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLite_mini_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomerForm manageCustomerForm = new ManageCustomerForm();
            manageCustomerForm.Show();
        }

        private void btnManageBook_Click(object sender, RoutedEventArgs e)
        {
            ManageBookForm manageBookForm = new ManageBookForm();
            manageBookForm.Show();
        }

        private void btnPurchaseBook_Click(object sender, RoutedEventArgs e)
        {
            SaleBookForm saleBookForm = new SaleBookForm();
            saleBookForm.Show();
        }
    }
}
