using Pharmay0._0._2.Classes;
using Pharmay0._0._2.UI.Suppliers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pharmay0._0._2.Database.ClassesDatabaseOperations
{
    
    class CustomersOperations
    {
        public static ArrayList CUSTOMERS_LIST = new ArrayList();
        private DatabaseManager dbManager;
        private OleDbCommand comand;
        private DatabaseOperations inserUpdatetObj;
        private OrdersOperations Oo;
        private CustomersOrders cOrders;

        public CustomersOrders COrders
        {
            get
            {
                return cOrders;
            }

            set
            {
                cOrders = value;
            }
        }

        public CustomersOperations() {
            this.dbManager = DatabaseManager.getDatabaseMangmentObj();
            this.comand = new OleDbCommand();
            inserUpdatetObj = new DatabaseOperations();
            Oo = new OrdersOperations();
            
        }
        public void resetCustomerList() {
            CUSTOMERS_LIST = new ArrayList();
        }
        public bool addNewCustomer(Customers customer)
        {
            string query = "INSERT INTO DebtCustomers ({0},{1},{2}) VALUES(\"{3}\",{4},\"{5}\")";
            query = string.Format(query,
                DatabaseVars.CUSTOMER_NAME, 
                DatabaseVars.DEBT_VALUE, 
                DatabaseVars.MOBILE_NUMBER,
                customer.CustomerName, 
                customer.DebtValue, 
                customer.MobileNumber);

            int customerID = inserUpdatetObj.inserUpdatetData(query,0);

            if (customerID != -1) {
                customer.CustomerID = customerID;
                CUSTOMERS_LIST.Add(customer);
                COrders.addAndRefreshCustomersComboBox(customer);
                return true;
            }               
            return false;
        }

        public void getAllCustomers()
        {
            string query = "SELECT * FROM CustomersDepts";
            getAllCustomers(query);
        }

        private void getAllCustomers(string query)
        {

            Customers customer = new Customers();

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            while (result.Read())
            {

                customer.CustomerID = result.GetInt32(0);
                customer.CustomerName = result.GetString(1);
                //try
                //{
                    customer.DebtValue = result.GetDouble(2);
                //}
                //catch (Exception e) {
                //    customer.DebtValue = 0;
                //}
                
                customer.MobileNumber = result.GetString(3);
                CUSTOMERS_LIST.Add(customer);
                customer = new Customers();
            }

            result.Close();
            dbManager.closeDb();

        }
        public bool updateCustomer(Customers cutomer) {
            string query = "UPDATE Meds SET CustomerName = '{0}', DebtValue = {1}, MobileNum = '{2}' WHERE CustomerID = {3}";
            query = string.Format(query, cutomer.CustomerName, cutomer.DebtValue, cutomer.CustomerID);
            return inserUpdatetObj.inserUpdatetData(query);
        }
        public void getAllOrdersForCustomer(Customers customer) {
            Oo.getCustomerUnPaidOrders(customer);
        }

        public double getCustomerDebtsTotalValue()
        {
            double totalLateFees = 0;
            string query = "SELECT TotalDepts FROM TotalDepts;";
            Customers customer = new Customers();

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            if (result.Read())
            {
                try
                {
                    totalLateFees = (double)result.GetDouble(0);
                }
                catch (Exception e) {
                }              
            }
           // totalLateFees = Regex.Replace(totalLateFees, "[^0-9.]", "");

            result.Close();
            dbManager.closeDb();
            return totalLateFees;
        }
        public double getSuplierDebts()
        {
            string totalLateFees = "";
            string query = "SELECT FORMAT (Sum(DebtValue) , 'Currency' ) As Total FROM QCompanies;";
            Customers customer = new Customers();

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            if (result.Read())
            {
                totalLateFees = result.GetString(0);
            }
            totalLateFees = Regex.Replace(totalLateFees, "[^0-9.]", "");

            result.Close();
            dbManager.closeDb();
            try
            {
                return double.Parse(totalLateFees);
            }
            catch (Exception e) {
                return 0;
            }
        }
    }

    


}
