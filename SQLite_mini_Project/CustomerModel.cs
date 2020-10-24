using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SQLite_mini_Project
{
    class CustomerModel
    {
        private static string dbpath = "sqliteBookStore.db";
        private string customer_Id;
        private string customer_name;
        private string customer_address;
        private string customer_email;
        private static string tbName = "Customers";

        public string Customer_Id { get => customer_Id; set => customer_Id = value; }
        public string Customer_name { get => customer_name; set => customer_name = value; }
        public string Customer_address { get => customer_address; set => customer_address = value; }
        public string Customer_email { get => customer_email; set => customer_email = value; }

        public CustomerModel(string customerId, string customerName, string address, string email)
        {
            Customer_Id = customerId;
            Customer_name = customerName;
            Customer_address = address;
            Customer_email = email;
        }

        public static void InitializeDatabase()
        {
            using(SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS Customers (" +
                    "Customer_Id NVARCHAR(5) PRIMARY KEY," +
                    "Customer_name NVARCHAR(30) NOT NULL," +
                    "Customer_address NVARXHAR(100) NOT NULL," +
                    "Customer_email NVARXHAR(100) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static Boolean AddCustomer(string CustomerId, string CustomerName, string CustomerAddress, string CustomerEmail)
        {
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "INSERT INTO Customers VALUES (@customerId, @customerName, @customerAddress, @customerEmail)";
                    insertCommand.Parameters.AddWithValue("@customerId", CustomerId);
                    insertCommand.Parameters.AddWithValue("@customerName", CustomerName);
                    insertCommand.Parameters.AddWithValue("@customerAddress", CustomerAddress);
                    insertCommand.Parameters.AddWithValue("@customerEmail", CustomerEmail);

                    insertCommand.ExecuteReader();

                    db.Close();
                    return true;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            } 
                
        }
        public static List<List<String>> showCustomerData()
        {
            List<List<String>> customerData = new List<List<String>>();
            using(SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.CommandText = "SELECT * FROM " + tbName;
                selectCommand.Connection = db;
                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    List<string> dataColumn = new List<string>();
                    for(int i = 0; i < query.FieldCount; i++)
                    {
                        dataColumn.Add(query.GetString(i));
                    }
                    customerData.Add(dataColumn);
                }

                db.Close();
            }
            return customerData;
        }

        public static List<List<string>> filterSearchCystomerData(string searchFilter,string searchKeyword)
        {
            MessageBox.Show(tbName);
            List<List<String>> customerData = new List<List<String>>();
            using(SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand searchCommand = new SqliteCommand();
                if(searchFilter == "ทั้งหมด")
                {
                    searchCommand.CommandText = "SELECT * FROM " + tbName + " " +
                                                "WHERE Customer_Id LIKE '%"+ @searchKeyword + "%' OR " +
                                                "Customer_name LIKE '%" + @searchKeyword + "%' OR " +
                                                "Customer_address LIKE '%" + @searchKeyword + "%' OR " +
                                                "Customer_email LIKE '%" + @searchKeyword + "%'";
                    /*
                    searchCommand.CommandText = "SELECT * FROM " + tbName + " " +
                                                "WHERE Customer_Id LIKE '%" + @searchKeyword + "%'";
                    */

                    searchCommand.Parameters.AddWithValue(@searchKeyword,searchKeyword);
                    MessageBox.Show(searchCommand.CommandText);
                    searchCommand.Connection = db;
                    SqliteDataReader query = searchCommand.ExecuteReader();

                    while (query.Read())
                    {
                        List<string> dataColumn = new List<string>();
                        for (int i = 0; i < query.FieldCount; i++)
                        {
                            dataColumn.Add(query.GetString(i));
                        }
                        customerData.Add(dataColumn);
                    }

                    db.Close();
                }
                
                db.Close();
            }
            return customerData;
        }

    }
}
