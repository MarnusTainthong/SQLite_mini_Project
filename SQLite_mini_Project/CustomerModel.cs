using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_mini_Project
{
    class CustomerModel
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteBookStore.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS Customers (" +
                    "Customer_Id INTEGER(5) PRIMARY KEY," +
                    "Customer_name NVARCHAR(30) NULL," +
                    "Customer_address NVARXHAR(100) NULL," +
                    "Customer_email NVARXHAR(100) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
    }
}
