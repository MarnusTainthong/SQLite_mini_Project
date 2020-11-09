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
            showBookData();
        }

        private void showBookData()
        {
            List<List<string>> dataFound = new List<List<string>>();
            int i = 0;
            foreach (List<string> bookItem in BookModel.showBookData())
            {
                dataFound.Add(bookItem);
                i++;
            }

            if (dataFound.Count == 0)
            {
                MessageBox.Show("Data Not Found.");
            }
            else
            {
                List<BookModel> bookList = new List<BookModel>();
                int numberOfList = dataFound.Count();
                for (int j = 0; j < numberOfList; j++)
                {
                    bookList.Add(new BookModel(dataFound[j][0], dataFound[j][1], dataFound[j][2], float.Parse(dataFound[j][3])));
                }
                listViewBook.ItemsSource = bookList;
            }

        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
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
                    showBookData();
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
            tbInputISBN.IsReadOnly = false;
            tbInputISBN.Clear();
            tbInputBookTitle.Clear();
            tbInputBookDesc.Clear();
            tbInputBookPrice.Clear();
            showBookData();
        }

        private void tbInputBookPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9.]+");
        }

        private void tbInputSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbInputSearch.Text = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchFilter = selectSearchFilter.Text;

            List<List<string>> dataFound = new List<List<string>>();
            int i = 0;
            foreach (List<string> searchItem in BookModel.filterSearchBookData(searchFilter, tbInputSearch.Text))
            {
                dataFound.Add(searchItem);
                i++;
            }

            if (dataFound.Count == 0)
            {
                MessageBox.Show("Data Not Found.");
                listViewBook.ItemsSource = "";
            }
            else
            {
                List<BookModel> bookList = new List<BookModel>();
                int numberOfList = dataFound.Count();
                for (int j = 0; j < numberOfList; j++)
                {
                    bookList.Add(new BookModel(dataFound[j][0], dataFound[j][1], dataFound[j][2], float.Parse(dataFound[j][3])));
                }
                listViewBook.ItemsSource = bookList;
            }
        }

        private void listViewBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookModel selectBook = (BookModel)listViewBook.SelectedItem;
            if(selectBook != null)
            {
                tbInputISBN.IsReadOnly = true;
                tbInputISBN.Text = selectBook.BookISBN;
                tbInputBookTitle.Text = selectBook.BookTitle;
                tbInputBookDesc.Text = selectBook.BookDesc;
                tbInputBookPrice.Text = selectBook.BookPrice.ToString();
            }
        }

        private void btnEditBook_Click(object sender, RoutedEventArgs e)
        {
            string message = "คุณ 'ยืนยัน' ที่จะแก้ไขข้อมูล ใช่หรือไม่ ?";
            string title = "ยืนยันการแก้ไข";
            MessageBoxButton msgButton = MessageBoxButton.YesNo;

            if (MessageBox.Show(message, title, msgButton, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (BookModel.updateBook(tbInputISBN.Text, tbInputBookTitle.Text, tbInputBookDesc.Text, tbInputBookPrice.Text))
                {

                    MessageBox.Show("แก้ไขข้อมูลสำเร็จ!");
                }
                else
                {
                    MessageBox.Show("แก้ไขข้อมูลไม่สำเร็จ! โปรดติดต่อเจ้าหน้าที่");
                }
            }
            resetFormData();
        }
    }
}
