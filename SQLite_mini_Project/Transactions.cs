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
        private static string dbpath = "sqliteBookStore.db";
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
}
