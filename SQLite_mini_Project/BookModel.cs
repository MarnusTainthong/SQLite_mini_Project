using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SQLite_mini_Project
{
    class BookModel
    {
        private static string dbpath = "sqliteBookStore.db";
        private static string tbName = "Books";

        private string bookISBN;
        private string bookTitle;
        private string bookDesc;
        private float bookPrice;

        public string BookISBN { get => bookISBN; set => bookISBN = value; }
        public string BookTitle { get => bookTitle; set => bookTitle = value; }
        public string BookDesc { get => bookDesc; set => bookDesc = value; }
        public float BookPrice { get => bookPrice; set => bookPrice = value; }

        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteBookStore.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS Books (" +
                    "ISBN NVARCHAR(13) PRIMARY KEY," +
                    "Title NVARCHAR(30) NULL," +
                    "Description NVARXHAR(30) NULL," +
                    "Price FLOAT(5) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }

        public static bool AddBook(string BookISBN, string BookTitle, string BookDesc, float BookPrice)
        {
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "INSERT INTO " + tbName + " VALUES (@BookISBN, @BookTitle, @BookDesc, @BookPrice)";
                    insertCommand.Parameters.AddWithValue("@BookISBN", BookISBN);
                    insertCommand.Parameters.AddWithValue("@BookTitle", BookTitle);
                    insertCommand.Parameters.AddWithValue("@BookDesc", BookDesc);
                    insertCommand.Parameters.AddWithValue("@BookPrice", BookPrice);

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
    }
}
