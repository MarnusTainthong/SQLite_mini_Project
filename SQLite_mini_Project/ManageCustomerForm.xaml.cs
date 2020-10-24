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
                resetFormData();
                showCustomerData();
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

        public void resetFormData()
        {
            tbInputCustomerId.Clear();
            tbInputCustomerName.Clear();
            tbInputCustomerAddress.Clear();
            tbInputCustomerEmail.Clear();
        }

        private void listViewCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerModel selectCustomer = (CustomerModel)listViewCustomers.SelectedItem;
            if(selectCustomer != null)
            {
                tbInputCustomerId.Text = selectCustomer.Customer_Id;
                tbInputCustomerId.IsReadOnly = true;
                tbInputCustomerName.Text = selectCustomer.Customer_name;
                tbInputCustomerAddress.Text = selectCustomer.Customer_address;
                tbInputCustomerEmail.Text = selectCustomer.Customer_email;
            }
            
        }

        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            string message = "คุณ 'ยืนยัน' ที่จะแก้ไขข้อมูล ใช่หรือไม่ ?";
            string title = "ยืนยันการลบ";
            MessageBoxButton msgButton = MessageBoxButton.YesNo;

            if (MessageBox.Show(message, title, msgButton, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (CustomerModel.updateCustomer(tbInputCustomerId.Text, tbInputCustomerName.Text, tbInputCustomerAddress.Text, tbInputCustomerEmail.Text))
                {
                    resetFormData();
                    showCustomerData();
                    MessageBox.Show("แก้ไขข้อมูลสำเร็จ!");
                }
                else
                {
                    MessageBox.Show("แก้ไขข้อมูลไม่สำเร็จ! โปรดติดต่อเจ้าหน้าที่");
                }
            }
        }
    }
}
