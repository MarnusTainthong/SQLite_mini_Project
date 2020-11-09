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

        public BookModel(string bookISBN, string bookTitle, string bookDesc, float bookPrice)
        {
            BookISBN = bookISBN;
            BookTitle = bookTitle;
            BookDesc = bookDesc;
            BookPrice = bookPrice;

        }

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

        public static List<List<String>> showBookData()
        {
            List<List<String>> booksData = new List<List<String>>();
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.CommandText = "SELECT * FROM " + tbName;
                selectCommand.Connection = db;
                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    List<string> dataColumn = new List<string>();
                    for (int i = 0; i < query.FieldCount; i++)
                    {
                        dataColumn.Add(query.GetString(i));
                    }
                    booksData.Add(dataColumn);
                }

                db.Close();
            }
            return booksData;
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

        public static List<List<string>> filterSearchBookData(string searchFilter, string searchKeyword)
        {
            List<List<String>> bookData = new List<List<String>>();
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand searchCommand = new SqliteCommand();

                if (searchFilter == "ทั้งหมด")
                {

                    searchCommand.CommandText = "SELECT * FROM " + tbName + " " +
                                                "WHERE ISBN LIKE '%" + @searchKeyword + "%' OR " +
                                                "Title LIKE '%" + @searchKeyword + "%' OR " +
                                                "Description LIKE '%" + @searchKeyword + "%' OR " +
                                                "Price LIKE '%" + @searchKeyword + "%'";
                }
                else if (searchFilter == "ISBN")
                {
                    searchCommand.CommandText = "SELECT * FROM " + tbName + " " +
                                                "WHERE ISBN LIKE '%" + @searchKeyword + "%'";
                }
                else if (searchFilter == "ชื่อหนังสือ")
                {
                    searchCommand.CommandText = "SELECT * FROM " + tbName + " " +
                                                "WHERE Title LIKE '%" + @searchKeyword + "%'";
                }
                else if (searchFilter == "รายละเอียด")
                {
                    searchCommand.CommandText = "SELECT * FROM " + tbName + " " +
                                                "WHERE Description LIKE '%" + @searchKeyword + "%'";
                }
                else if (searchFilter == "ราคา")
                {
                    searchCommand.CommandText = "SELECT * FROM " + tbName + " " +
                                                "WHERE Price LIKE '%" + @searchKeyword + "%'";
                }

                searchCommand.Parameters.AddWithValue("@searchKeyword", searchKeyword);
                searchCommand.Connection = db;
                SqliteDataReader query = searchCommand.ExecuteReader();

                while (query.Read())
                {
                    List<string> dataColumn = new List<string>();
                    for (int i = 0; i < query.FieldCount; i++)
                    {
                        dataColumn.Add(query.GetString(i));
                    }
                    bookData.Add(dataColumn);
                }

                db.Close();
            }
            return bookData;
        }

        public static Boolean updateBook(string ISBN, string bookTitle, string bookDesc, string bookPrice)
        {
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    SqliteCommand updateCommand = new SqliteCommand();
                    updateCommand.CommandText = "UPDATE " + tbName + " SET Title = @Book_title, Description = @Book_desc, Price = @Book_price " +
                                                "WHERE ISBN = @Book_ISBN";

                    updateCommand.Parameters.AddWithValue("@Book_ISBN", ISBN);
                    updateCommand.Parameters.AddWithValue("@Book_title", bookTitle);
                    updateCommand.Parameters.AddWithValue("@Book_desc", bookDesc);
                    updateCommand.Parameters.AddWithValue("@Book_price", bookPrice);

                    updateCommand.Connection = db;
                    updateCommand.ExecuteReader();

                    db.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
