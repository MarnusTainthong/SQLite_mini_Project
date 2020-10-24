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

namespace SQLite_mini_Project
{
    /// <summary>
    /// Interaction logic for ManageCustomerForm.xaml
    /// </summary>
    public partial class ManageCustomerForm : Window
    {
        public ManageCustomerForm()
        {
            InitializeComponent();
            CustomerModel.InitializeDatabase();
            showCustomerData();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            string CustomerId = tbInputCustomerId.Text;
            string CustomerName = tbInputCustomerName.Text;
            string CustomerAddress = tbInputCustomerAddress.Text;
            string CustomerEmail = tbInputCustomerEmail.Text;

            if (CustomerModel.AddCustomer(CustomerId, CustomerName, CustomerAddress, CustomerEmail))
            {
                MessageBox.Show("เพิ่มข้อมูลสำเร็จ!");
            }
            else
            {
                MessageBox.Show("เพิ่มข้อมูลไม่สำเร็จ! โปรดติดต่อเจ้าหน้าที่");
            }      
        }
        private void showCustomerData()
        {
            List<List<string>> dataFound = new List<List<string>>();
            int i = 0;
            foreach (List<string> searchItem in CustomerModel.showCustomerData())
            {
                dataFound.Add(searchItem);
                i++;
            }

            if (dataFound.Count == 0)
            {
                MessageBox.Show("Data Not Found.");
            }
            else
            {
                List<CustomerModel> customerList = new List<CustomerModel>();
                int numberOfList = dataFound.Count();
                for (int j = 0; j < numberOfList; j++)
                {
                    customerList.Add(new CustomerModel(dataFound[j][0], dataFound[j][1], dataFound[j][2], dataFound[j][3]));
                }
                listViewCustomers.ItemsSource = customerList;
            }
            
        }

        private void tbInputSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbInputSearch.Text = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(selectSearchFilter.Text);
            string searchFilter = selectSearchFilter.Text;

            List<List<string>> dataFound = new List<List<string>>();
            int i = 0;
            foreach (List<string> searchItem in CustomerModel.filterSearchCystomerData(searchFilter, tbInputSearch.Text))
            {
                dataFound.Add(searchItem);
                i++;
            }

            if (dataFound.Count == 0)
            {
                MessageBox.Show("Data Not Found.");
                listViewCustomers.ItemsSource = "";
            }
            else
            {
                List<CustomerModel> customerList = new List<CustomerModel>();
                int numberOfList = dataFound.Count();
                for (int j = 0; j < numberOfList; j++)
                {
                    customerList.Add(new CustomerModel(dataFound[j][0], dataFound[j][1], dataFound[j][2], dataFound[j][3]));
                }
                listViewCustomers.ItemsSource = customerList;
            }          

        }
    }
}
