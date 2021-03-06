﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmay0._0._2.Database
{
    class DatabaseManager
    {
        // @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Supermarket.accdb;"
        private static DatabaseManager databaseManagment = null;

        private string connectionString;
        private OleDbConnection db;
        private OleDbCommand comand;

        private DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
            comand = new OleDbCommand();
            db = null;
        }

        public static DatabaseManager getDatabaseMangmentObj(string connectionString)
        {
            if (databaseManagment == null)
                databaseManagment = new DatabaseManager(connectionString);
            return databaseManagment;
        }

        public OleDbConnection openDb()
        {
            try
            {
                db = new OleDbConnection();
                db.ConnectionString = this.connectionString;
                db.Open();
                
            }
            catch (Exception e)
            {

            }
            return db;
        }

        public void closeDb()
        {
            try
            {
                if (this.db.State.Equals(System.Data.ConnectionState.Open))
                {
                    this.db.Close();
                }
            }
            catch (Exception e)
            {

            }
        }

       

    }
}





/*
SELECT Orders.OrderID, Customers.CustomerName, Orders.OrderDate, Employees.FirstName, Employees.LastName
FROM ((Orders
INNER JOIN Customers ON Orders.CustomerID=Customers.CustomerID)
INNER JOIN Employees ON Orders.EmployeeID=Employees.EmployeeID)
WHERE Customers.CustomerID=44;
     */
