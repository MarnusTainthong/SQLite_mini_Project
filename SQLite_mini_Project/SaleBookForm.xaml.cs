using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SaleBookForm.xaml
    /// </summary>
    
    public partial class SaleBookForm : Window
    {
        List<PurchaseList> bookList = new List<PurchaseList>();
        bool btnCustomerStatus = false;
        int btnConfirmState = 0;
        float totalPrice = 0;
        public SaleBookForm()
        {
            InitializeComponent();
        }

        private void tbInputISBN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+$");
        }

        private void btnSearchISBN_Click(object sender, RoutedEventArgs e)
        {
            if (checkEmpty(tbInputISBN.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูล");
            }
            else
            {
                List<List<string>> dataFound = new List<List<string>>();
                foreach (List<string> bookData in BookModel.getBookDataByISBN(tbInputISBN.Text))
                {
                    dataFound.Add(bookData);
                }
                if (!dataFound.Any())
                {
                    MessageBox.Show("Data Not Found.");
                }
                else
                {
                    tbInputBookTitle.Text = dataFound[0][1];
                    tbInputBookDesc.Text = dataFound[0][2];
                    tbInputBookPrice.Text = dataFound[0][3];
                    tbInputQtyBuy.Text = "1";
                    tbInputQtyBuy.IsEnabled = true;
                }
            }
        }

        private bool checkEmpty(string str)
        {
            if(str == null || str == "" || str == " ")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void tbInputQtyBuy_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+$");
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (!checkEmpty(tbInputISBN.Text))
            {
                bookList.Add(new PurchaseList(tbInputISBN.Text, tbInputBookTitle.Text, float.Parse(tbInputBookPrice.Text), int.Parse(tbInputQtyBuy.Text)));
                listViewCart.ItemsSource = bookList;
                listViewCart.Items.Refresh();
                btnConfirmState++;

                totalPrice += (float.Parse(tbInputBookPrice.Text) * int.Parse(tbInputQtyBuy.Text));
                tbInputTotalPrice.Text = totalPrice.ToString() + ".-";

                resetForm();
            }
            else
            {
                MessageBox.Show("กรุณากรอกข้อมูล");
            }
            checkBtnConfirm();

        }

        private void resetForm()
        {
            tbInputBookTitle.Text = null;
            tbInputBookDesc.Text = null;
            tbInputBookPrice.Text = null;
            tbInputQtyBuy.Text = null;
            tbInputISBN.Text = null;

            tbInputQtyBuy.IsEnabled = false;
            tbInputBookTitle.IsEnabled = false;
            tbInputBookDesc.IsEnabled = false;
            tbInputBookPrice.IsEnabled = false;
            tbInputQtyBuy.IsEnabled = false;
        }

        private void btnSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (btnCustomerStatus)
            {
                tbInputCustomerId.Text = null;
                btnSearchCustomer.Content = "ค้นหา";
                tbInputCustomerId.IsEnabled = true;
                tbInputCustomerName.Text = null;
                btnCustomerStatus = false;
                btnConfirmState--;
            }
            else
            {
                if (checkEmpty(tbInputCustomerId.Text))
                {
                    MessageBox.Show("กรุณากรอกข้อมูล");
                }
                else
                {
                    List<List<string>> dataFound = new List<List<string>>();
                    foreach (List<string> bookData in CustomerModel.getCustomerById(tbInputCustomerId.Text))
                    {
                        dataFound.Add(bookData);
                    }
                    if (!dataFound.Any())
                    {
                        MessageBox.Show("Data Not Found.");
                    }
                    else
                    {
                        tbInputCustomerName.Text = dataFound[0][1];
                        tbInputCustomerId.IsEnabled = false;
                        btnSearchCustomer.Content = "ยกเลิก";
                        btnConfirmState++;
                        btnCustomerStatus = true;
                    }
                }
            }

            checkBtnConfirm();    

        }

        private void tbInputCustomerId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+$");
        }

        private void checkBtnConfirm()
        {
            if (btnConfirmState >= 2)
            {
                btnConfirmBuy.IsEnabled = true;
            }
            else
            {
                btnConfirmBuy.IsEnabled = false;
            }
            MessageBox.Show(btnConfirmState.ToString());
        }

        private void btnConfirmBuy_Click(object sender, RoutedEventArgs e)
        {
            string message = "คุณต้องการ 'ยืนยันคำสั่งซื้อ' นี้ ใช่หรือไม่ ?";
            string title = "ยืนยันคำสั่งซื้อ";
            MessageBoxButton msgButton = MessageBoxButton.OKCancel;
            if (MessageBox.Show(message, title, msgButton, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                Transactions.InitializeDatabase();
                string orderNumber = Transactions.generateOrderNumber();
                //MessageBox.Show(orderNumber.ToString());
                foreach(PurchaseList purchaseItem in listViewCart.Items)
                {
                    //MessageBox.Show(purchaseItem.BookIsbn);
                    Transactions.AddTransactions(purchaseItem.BookIsbn, tbInputCustomerId.Text, orderNumber.ToString(), purchaseItem.Qty);
                }

                ShowOrder showOrder = new ShowOrder(bookList, orderNumber, tbInputCustomerName.Text, totalPrice);
                showOrder.Show();
                this.Close();
                
            }
            resetForm();
        }
    }
}
