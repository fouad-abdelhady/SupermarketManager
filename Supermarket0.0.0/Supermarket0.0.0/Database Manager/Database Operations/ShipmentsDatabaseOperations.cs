
using Pharmay0._0._2.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmay0._0._2.Database.ClassesDatabaseOperations
{
    class ShipmentsDatabaseOperations
    {
        private DatabaseManager dbManager;
        private OleDbCommand comand;
        private DatabaseOperations inserUpdatetObj;
        private MedicienOperations mOperations;
        private CompaniesOperations cOperations;

        public ShipmentsDatabaseOperations()
        {
            this.dbManager = DatabaseManager.getDatabaseMangmentObj();
            this.comand = new OleDbCommand();
            inserUpdatetObj = new DatabaseOperations();
            mOperations = new MedicienOperations();
            cOperations = new CompaniesOperations();
        }

        #region get data

        public ArrayList getOrders()
        {
            string query = "SELECT * FROM CompanyOrders";
            return getShipment(query);
        }

        public ArrayList getOrders(Company com)
        {
            string query = "SELECT * FROM CompanyOrders WHERE {0} = {1}";
            query = string.Format(query, DatabaseVars.COMPANY_ID, com.CompanyID);

            return getShipment(query);
        }

        internal ArrayList getAllShipmentsOrdered(DateTime from, DateTime to, string rest)
        {
            string query = "SELECT * FROM ShipmentsQ WHERE OrderDate BETWEEN #{0}# AND #{1}# {2};";
            query = string.Format(query, from, to, rest);
            return getAllShipments(query);
        }

        internal bool deleteCompanyOrder(DateTime from, DateTime to, string rest)
        {
            string query = "Delete FROM CompanyOrders WHERE OrderDate BETWEEN #{0}# AND #{1}# {2};";
            query = string.Format(query, from, to, rest);
            return inserUpdatetObj.inserUpdatetData(query);
        }
        private ArrayList getShipment(string query)
        {
            ArrayList shipmentsData = new ArrayList();
            Shipments shipment = new Shipments();

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            while (result.Read())
            {
                shipment.CompanyOrderId = result.GetInt32(0);
                shipment.CompanyId = result.GetInt32(1);
                shipment.TotalPrice = (double)result.GetDecimal(2);
                shipment.PaidAmount = (double)result.GetDecimal(3);
                shipment.OrderDate = result.GetDateTime(4);
                shipment.DebtValue = (double)result.GetDecimal(5);

                shipmentsData.Add(shipment);
                shipment = new Shipments();
            }

            result.Close();
            dbManager.closeDb();
            return shipmentsData;
        }

        private ArrayList getAllShipments(string query)
        {
            ArrayList shipmentsData = new ArrayList();
            Shipments shipment = new Shipments();

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            while (result.Read())
            {
                shipment.CompanyOrderId = result.GetInt32(0);
                shipment.CompanyId = result.GetInt32(1);
                shipment.CompanyName = result.GetString(2);
                shipment.TotalPrice = (double)result.GetDecimal(3);
                shipment.PaidAmount = (double)result.GetDecimal(4);
                shipment.OrderDate = result.GetDateTime(5);
                shipment.DebtValue = (double)result.GetDecimal(6);

                shipmentsData.Add(shipment);
                shipment = new Shipments();
            }

            result.Close();
            dbManager.closeDb();
            return shipmentsData;
        }

        #endregion

        public bool insertShipment(Shipments shipment)
        {
            insertMed(shipment.OrderMeds);
            insertCmedsOrder(shipment, insertOrder(shipment));
            updateCompanyDepts(shipment.Company);
            return true;
        }

        private void updateCompanyDepts(Company company)
        {
            cOperations.updateCompany(company);
        }

        private void insertCmedsOrder(Shipments shipment, int orderId)
        {
            

            foreach (MedDetailsInShipment mds in shipment.OrderMeds)
            {
                string query = "INSERT INTO CMedsOrders ({0},{1},{2},{3},{4}) VALUES('{5}',{6},{7},{8},{9});";
                query = string.Format(query,
                DatabaseVars.PARCODE,
                DatabaseVars.COMPANY_ORDER_ID,
                DatabaseVars.NUMBER_OF_PACKAGES,
                DatabaseVars.TOTAL_PRICE,
                DatabaseVars.PRICE_PER_UNIT,
                mds.cmo.Parcode,
                orderId,
                mds.cmo.NumOfPackages,
                mds.cmo.TotalPrice,
                mds.cmo.PricePerUnit);

                inserUpdatetObj.inserUpdatetData(query);
            }
                
            
        }

        private int insertOrder(Shipments shipment)
        {
            string query = "INSERT INTO CompanyOrders ({0},{1},{2},{3}) VALUES({4},{5},{6},'{7}');";
            query = string.Format(query,
            DatabaseVars.COMPANY_ID,
            DatabaseVars.TOTAL_PRICE,
            DatabaseVars.PAID_AMOUNT,
            DatabaseVars.ORDER_DATE,
            shipment.Company.CompanyID,
            shipment.TotalPrice,
            shipment.PaidAmount,
            shipment.OrderDate);

            return inserUpdatetObj.inserUpdatetData(query, 0);
        }

        #region meds operations

        private void insertMed(ArrayList orderMeds)
        {
            foreach (MedDetailsInShipment mds in orderMeds)
            {
                bool result = moveMedToOldList(mds.medicen);
                if (result)
                    mOperations.addMedicen(mds.medicen);
                else
                    mOperations.addNewMed(mds.medicen);
            }
        }

        internal bool setCompanyOrder(Shipments shpment)
        {
            string query = "UPDATE CompanyOrders set PaidAmount = {0} WHERE CompanyOrderID = {1};";
            query = string.Format(query, shpment.TotalPrice, shpment.CompanyOrderId);
            return inserUpdatetObj.inserUpdatetData(query);
        }

        private bool moveMedToOldList(Medicine med)
        { // TotalAmountPerOldMed
            bool isFounded = false;
            string query = "INSERT INTO OldMeds ({0},{1},{2},{3}) VALUES(\"{4}\",{5},\"{6}\",{7})";

            Medicine tempMed = mOperations.getMedicen(med.Parecode);
            if (tempMed != null && tempMed.NumOfTabes <= 0) {
                return true;
            }
            if (tempMed != null)
            {
                isFounded = true;
                query = string.Format(query,
                DatabaseVars.PARCODE,
                DatabaseVars.REMAINING_AMOUNT,
                DatabaseVars.EXPIRY_DATE,
                DatabaseVars.TAB_BUYING_PRICE,
                tempMed.Parecode,
                tempMed.NumOfTabes - getTheAmountInOldMeds(tempMed.Parecode),
                tempMed.ExpiryDate,
                tempMed.TabBuyingPrice);

                inserUpdatetObj.inserUpdatetData(query);

            }
            return isFounded;

        }

        private double getTheAmountInOldMeds(string pareCd)
        {
            string query = "SELECT * FROM OldInventory WHERE {0} = '{1}'";
            query = string.Format(query, DatabaseVars.PARCODE, pareCd);
            double amount = 0;
            string temp;
            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();
            if (result.Read()) {
                temp = result.GetString(0);
                amount = (double)result.GetDouble(2);
            }
                
            result.Close();
            dbManager.closeDb();
            return amount;
        }
        #endregion
    }


}
