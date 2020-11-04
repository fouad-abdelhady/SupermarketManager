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

    class DatabaseOperations
    {
        private DatabaseManager dbManager;
        private OleDbCommand comand;

        public DatabaseOperations() {
            this.dbManager = DatabaseManager.getDatabaseMangmentObj();
            this.comand = new OleDbCommand();
        }

       
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

        public int inserUpdatetData(string query, int y)
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



    }


}
