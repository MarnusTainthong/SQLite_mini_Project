using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_mini_Project
{
    class Transactions
    {
        string bookIsbn;
        string bookTitle;
        float bookPrice;
        int qty;
        float sumPrice;

        private static string dbpath = "sqliteBookStore.db";

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
                    "Transaction_Id INTEGER(5) PRIMARY KEY" +
                    "ISBN NVARCHAR(13) NULL," +
                    "Customer_Id INTEGER(5) NULL," +
                    "Quantity INT(5) NULL," +
                    "Total_price FLOAT(8) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
    }
    class PurchaseList : Transactions
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
