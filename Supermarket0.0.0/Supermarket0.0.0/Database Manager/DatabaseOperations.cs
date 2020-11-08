using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections;

namespace Pharmay0._0._2.Database
{

    abstract class DatabaseOperations
    {
        private DatabaseManager dbManager;
        private OleDbCommand comand;
        

        public DatabaseOperations(string connectionString) {
            this.dbManager = DatabaseManager.getDatabaseMangmentObj(connectionString);
            this.comand = new OleDbCommand();
        }

        /// <summary>
       /// for insert and update data row 
       /// </summary>
       /// <param name="query"> update or insertion query </param>
       /// <returns>whether updated or deleted </returns>
        public bool inserUpdatetData(string query)
        {
            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            this.comand.CommandText = query;
            int done = comand.ExecuteNonQuery();
            this.dbManager.closeDb();
            if (done == 0)
            {
                dbConnection.Close();
                return false;
            }
               
            //MessageBox.Show("Done");
            dbConnection.Close();
            return true;
        }

        /// <summary>
        /// get the id of the last inserted row 
        /// </summary>
        /// <param name="query"> the SQL query </param>
        /// <returns>the last inserted or updated row ID</returns>
        public int inserUpdatetReturnID(string query)
        {
            int orderID = -1;
            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            this.comand.CommandText = query;
            if (comand.ExecuteNonQuery() == 1)
            {
                this.comand.CommandText = "Select @@Identity";
                orderID = Convert.ToInt32(comand.ExecuteScalar());
            }
            
            this.dbManager.closeDb();
            return orderID;
            //MessageBox.Show("Done");
        }

        /// <summary>
        /// select all rows from tabel
        /// </summary>
        /// <param name="table">The reqired tabel</param>
        public void getData(string table) {
            int operationNum = 1;
            string query = "SELECT * FROM {0};";
            query = string.Format(query, table);

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            while (result.Read())
            {
                this.queryResult(result);
            }

            result.Close();
            dbManager.closeDb();
        }

        /// <summary>
        /// get the data of a table based on givin condtion 
        /// </summary>
        /// <param name="table"> The of the tabel that you want to get its data </param> 
        /// <param name="condition">The selection condtion ex. field name1 = v or field name2 = v1 AND field name3  < v2 </param>
        public void getData(string table, string condition)
        {
            int operationNum = 2;
            string query = "SELECT * FROM {0} WHERE {1};";
            query = string.Format(query, table, condition);

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            while (result.Read())
            {
                this.queryResult(result);
            }

            result.Close();
            dbManager.closeDb();
        }

        /// <summary>
        /// get the row from tabel with the givien ID
        /// </summary>
        /// <param name="table"> Tabel name</param>
        /// <param name="IDFieldName"> ID field name </param>
        /// <param name="id">ID value </param>
        public void getDataByID(string table, string IDFieldName, int id )
        {
            int operationNum = 3;
            string query = "SELECT * FROM {0} WHERE {1} = {2};";
            query = string.Format(query, table, IDFieldName, id);

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            while (result.Read())
            {
                this.queryResult(result);
            }

            result.Close();
            dbManager.closeDb();

        }

        /// <summary>
        /// for returning the result of a query 
        /// </summary>
        /// <param name="output"> its OleDbDataReader object that contains result data row</param>
        public abstract void queryResult(OleDbDataReader output);
        
    }


}
