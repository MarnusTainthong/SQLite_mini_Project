using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SQLite_mini_Project
{
    public class Transactions
    {
        private static string dbpath = "sqliteBookStore.db";
        private static string tbName = "Transactions";

        string bookIsbn;
        string bookTitle;
        float bookPrice;
        int qty;
        float sumPrice;

        public string BookIsbn { get => bookIsbn; set => bookIsbn = value; }
        public string BookTitle { get => bookTitle; set => bookTitle = value; }
        public float BookPrice { get => bookPrice; set => bookPrice = value; }
        public int Qty { get => qty; set => qty = value; }
        public float SumPrice { get => sumPrice; set => sumPrice = value; }

        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS Transactions (" +
                    "Transaction_Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "ISBN NVARCHAR(13) NULL," +
                    "Customer_Id NVARCHAR(5) NULL," +
                    "OrderNumber NVARCHAR(5) NULL," +
                    "Quantity INT(5) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static bool AddTransactions(string bookIsbn, string customerId, string orderNumber, int qty)
        {
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "INSERT INTO " + tbName + " (ISBN, Customer_Id, OrderNumber, Quantity) VALUES (@ISBN, @CustomerId, @OrderNumber, @Quantity)";
                    insertCommand.Parameters.AddWithValue("@ISBN", bookIsbn);
                    insertCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    insertCommand.Parameters.AddWithValue("@OrderNumber", orderNumber);
                    insertCommand.Parameters.AddWithValue("@Quantity", qty);

                    insertCommand.ExecuteReader();

                    db.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public static string generateOrderNumber()
        {
            string currentOrderNum = "";
            int temp;
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand searchCommand = new SqliteCommand();

                searchCommand.CommandText = "SELECT OrderNumber FROM " + tbName + " ORDER BY Transaction_Id DESC LIMIT 1";

                searchCommand.Connection = db;
                SqliteDataReader query = searchCommand.ExecuteReader();

                while (query.Read())
                {
                    currentOrderNum = query.GetString(0);
                }
                db.Close();
            }
            if(currentOrderNum == null || currentOrderNum == "")
            {
                currentOrderNum = "00001";
                return currentOrderNum;
            }
            else
            {
                temp = int.Parse(currentOrderNum);
                temp++;
                return temp.ToString();
            }
        }
    }
    public class PurchaseList : Transactions
    {
        public PurchaseList()
        {

        }

        public PurchaseList(string bookIsbn, string bookTitle, float bookPrice, int qty)
        {
            BookIsbn = bookIsbn;
            BookTitle = bookTitle;
            BookPrice = bookPrice;
            Qty = qty;
            SumPrice = Qty * BookPrice;
        }
    }

}
