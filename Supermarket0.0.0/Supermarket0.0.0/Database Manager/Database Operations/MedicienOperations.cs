using Pharmay0._0._2.Classes;
using System;
using System.Collections;
using System.Data.OleDb;

namespace Pharmay0._0._2.Database.ClassesDatabaseOperations
{
    class MedicienOperations
    {
        private DatabaseManager dbManager;
        private OleDbCommand comand;
        private DatabaseOperations inserUpdatetObj;
        ArrayList listOfMeds;

        public MedicienOperations() {
            this.dbManager = DatabaseManager.getDatabaseMangmentObj();
            this.comand = new OleDbCommand();
            listOfMeds = new ArrayList();
            inserUpdatetObj = new DatabaseOperations();
        }

        public Medicine getMedicen(string parcode)
        {
            string query = "SELECT * FROM Meds WHERE {0} = '{1}'";
            query = string.Format(query, DatabaseVars.PARCODE, parcode);
            return getMed(query);
        }

        private Medicine getMed(string query)
        {
            bool isFound = false;
            Medicine med = new Medicine();
            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            if (result.Read())
            {
                isFound = true;
                med.MedID = result.GetInt32(0);
                med.MedName = result.GetString(1);
                med.TabPrice = (double)result.GetDecimal(2);               
                med.TabPerPackage = result.GetInt32(3);
                med.NumOfTabes = result.GetInt32(4);
                med.Parecode = result.GetString(5);
                med.ExpiryDate = result.GetDateTime(6);
                med.TabBuyingPrice = (double)result.GetDecimal(7);
            }

            result.Close();
            dbManager.closeDb();
            if (isFound) {
                return getFullMedData(med);
            }
                
            return null;
        }

        public ArrayList getAllMeds()
        {
            this.listOfMeds = new ArrayList();
            string query = "SELECT * FROM Meds;";          
            Medicine med = new Medicine();
            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            while (result.Read())
            {
                
                med.MedID = result.GetInt32(0);
                med.MedName = result.GetString(1);
                med.TabPrice = (double)result.GetDecimal(2);
                med.TabPerPackage = result.GetInt32(3);
                med.NumOfTabes = result.GetInt32(4);
                med.Parecode = result.GetString(5);
                med.ExpiryDate = result.GetDateTime(6);

                listOfMeds.Add(med);
                med = new Medicine();
            }

            result.Close();
            dbManager.closeDb();
            

            return listOfMeds;
        }

        private Medicine getFullMedData(Medicine med)
        {
            string query = "SELECT * FROM OldMeds WHERE {0} = '{1}'";
            query = string.Format(query, DatabaseVars.PARCODE, med.Parecode);

            OldMed oldMedInfo = new OldMed();

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            if (result.Read())
            {
                oldMedInfo.Id1 = result.GetInt32(0);
                oldMedInfo.PareCode = result.GetString(1);
                oldMedInfo.RemainingAmount = result.GetInt32(2);
                oldMedInfo.ExpiryDate = result.GetDateTime(3);
                med.Oldmed = oldMedInfo;
            }

            result.Close();
            dbManager.closeDb();
            return med;
        }

        public void getOldMeds(Medicine med)
        {
            string query = "SELECT * FROM OldMeds WHERE {0} = '{1}'";
            query = string.Format(query, DatabaseVars.PARCODE, med.Parecode);

            OldMed oldMedInfo = new OldMed();

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            while (result.Read())
            {
                oldMedInfo = new OldMed();
                oldMedInfo.Id1 = result.GetInt32(0);
                oldMedInfo.PareCode = result.GetString(1);
                oldMedInfo.RemainingAmount = result.GetInt32(2);
                oldMedInfo.ExpiryDate = result.GetDateTime(3);
                med.addOldMed(oldMedInfo);
            }

            result.Close();
            dbManager.closeDb();
        }


        public void addMedicen(Medicine med) {
            string query =
            "UPDATE Meds SET {0} = '{1}', {2} = {3}, {4} = {5} , {6} = {7}, {8} = '{9}', {10} = {11} WHERE {12} = '{13}'";

            query = string.Format(query,
               DatabaseVars.MEDECIEN_NAME, med.MedName,
               DatabaseVars.TABE_PRICE, med.TabPrice,
               DatabaseVars.TABE_PER_PACKAGE, med.TabPerPackage,
               DatabaseVars.NUMBER_OF_TABES, med.NumOfTabes,
               DatabaseVars.EXPIRY_DATE, med.ExpiryDate,
               DatabaseVars.TAB_BUYING_PRICE, med.TabBuyingPrice,
               DatabaseVars.PARCODE, med.Parecode);
            inserUpdatetObj.inserUpdatetData(query);
        }

        internal void addNewMed(Medicine medicen)
        {
            string query = "INSERT INTO Meds ({0},{1},{2},{3},{4},{5},{6}) VALUES('{7}',{8},{9},{10},'{11}','{12}',{13});";
            query = string.Format(query,
                DatabaseVars.MEDECIEN_NAME, 
                DatabaseVars.TABE_PRICE, 
                DatabaseVars.TABE_PER_PACKAGE,
                DatabaseVars.NUMBER_OF_TABES,
                DatabaseVars.PARCODE,
                DatabaseVars.EXPIRY_DATE,
                DatabaseVars.TAB_BUYING_PRICE,
                medicen.MedName,
                medicen.TabPrice,
                medicen.TabPerPackage,
                medicen.NumOfTabes,
                medicen.Parecode,
                medicen.ExpiryDate,
                medicen.TabBuyingPrice);
            inserUpdatetObj.inserUpdatetData(query);
        }

        public void deleteOldMed(OldMed om) {
            string query = "DELETE FROM OldMeds WHERE {0} = {1};";

            query = string.Format(query,
               DatabaseVars.ID, om.Id1);
            inserUpdatetObj.inserUpdatetData(query);
        }

        public void getSoldMedsReport(DateTime from, DateTime to) {
            SoldMeds sMed = new SoldMeds();
            sMed.resetSoldMdsList();

            string query = "SELECT MedsDailySales.MedName, MedsDailySales.Parcode, Sum(MedsDailySales.NumeOTabesPDay) AS TotalTabes FROM MedsDailySales " +
                            " WHERE( ( (MedsDailySales.[OrderDate]) Between #{0}# And #{1}#)) GROUP BY MedsDailySales.MedName, MedsDailySales.Parcode ORDER BY Sum(MedsDailySales.NumeOTabesPDay) DESC;";           
            query = string.Format(query, from, to);

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();
            
            while (result.Read())
            {
                sMed.MedName = result.GetString(0);
                sMed.PareCode = result.GetString(1);
                sMed.SoldSlips = result.GetDouble(2);

                SoldMeds.SOLD_MEDS_LIST.Add(sMed);
                sMed = new SoldMeds();
            }

            result.Close();
            dbManager.closeDb();
        }


        public void getInventory() {
            InventoryC.INVNTRY = new System.Collections.Generic.List<InventoryC>();
            InventoryC.TOTAL_INVENT_VALUE = 0;
            
            string query = "SELECT * FROM Inventory;";

            InventoryC inventory = new InventoryC();

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            while (result.Read())
            {
                inventory.PareCode = result.GetString(0);
                inventory.MedName = result.GetString(1);
                inventory.OldAmount = result.GetDouble(2);
                inventory.OldValue = (double)result.GetDecimal(3);
                inventory.NewAmount = result.GetDouble(4);
                inventory.NewValue = result.GetDouble(5);
                inventory.TotalMedValue = inventory.OldValue + inventory.NewValue;

                InventoryC.INVNTRY.Add(inventory);
                inventory = new InventoryC();               
            }

            result.Close();
            dbManager.closeDb();
        }

    }
}
