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
    /// Interaction logic for ShowOrder.xaml
    /// </summary>
    public partial class ShowOrder : Window
    {
        public ShowOrder()
        {
            
        }

        public ShowOrder(List<PurchaseList> purchaseList, string orderNo, string customerName, float totalPrice)
        {
            InitializeComponent();
            listViewCart.ItemsSource = purchaseList;
            lbOrderNo.Content = "หมายเลขคำสั่งซื้อ : " + orderNo;
            lbCustomerName.Content = "ชื่อลูกค้า : " + customerName;
            tbInputTotalPrice.Text = totalPrice.ToString();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
