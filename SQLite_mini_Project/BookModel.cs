using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_mini_Project
{
    class BookModel
    {
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
    }
}
