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
    /// Interaction logic for ManageBookForm.xaml
    /// </summary>
    public partial class ManageBookForm : Window
    {
        public ManageBookForm()
        {
            InitializeComponent();
            BookModel.InitializeDatabase();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!tbInputISBN.IsReadOnly)
            {
                string bookISBN = tbInputISBN.Text;
                string bookTitle = tbInputBookTitle.Text;
                string bookDesc = tbInputBookDesc.Text;
                string bookPrice = tbInputBookPrice.Text;

                if (BookModel.AddBook(bookISBN, bookTitle, bookDesc, float.Parse(bookPrice)))
                {
                    //showCustomerData();
                    MessageBox.Show("เพิ่มข้อมูลสำเร็จ!");
                }
                else
                {
                    MessageBox.Show("เพิ่มข้อมูลไม่สำเร็จ! โปรดติดต่อเจ้าหน้าที่");
                }

            }
            else
            {
                MessageBox.Show("ไม่สามารถเพิ่มข้อมูลได้ โปรดลองใหม่อีกครั้ง", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            resetFormData();
        }

        private void tbInputISBN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void resetFormData()
        {
            tbInputISBN.Clear();
            tbInputBookTitle.Clear();
            tbInputBookDesc.Clear();
            tbInputBookPrice.Clear();
        }

        private void tbInputBookPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9.]+");
        }
    }
}
