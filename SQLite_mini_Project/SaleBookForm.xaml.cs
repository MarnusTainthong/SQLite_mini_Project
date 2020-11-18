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
                MessageBox.Show("กรุณากรอกข้อมูลให้ถูกต้อง");
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
    }
}
