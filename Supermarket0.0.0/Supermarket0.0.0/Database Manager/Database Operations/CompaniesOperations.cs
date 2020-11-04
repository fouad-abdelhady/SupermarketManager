
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
    
    public class CompaniesOperations
    {
        public static int ALL_COMPANIES = 0;
        public static int DEBT_COMPANIES = 1;

        private DatabaseManager dbManager;
        private OleDbCommand comand;
        private DatabaseOperations inserUpdatetObj;

        public CompaniesOperations() {
            this.dbManager = DatabaseManager.getDatabaseMangmentObj();
            this.comand = new OleDbCommand();
            inserUpdatetObj = new DatabaseOperations();
        }





        public bool addCompany(Company company)
        {
            string query = "INSERT INTO Companies ({0},{1},{2}) VALUES(\"{3}\",{4},\"{5}\")";
            query = string.Format(query, 
                DatabaseVars.COMPANY_NAME, DatabaseVars.DEBT_VALUE, DatabaseVars.MOBILE_NUMBER,
                company.CompanyName, company.DebtValue, company.MobileNum);
            DatabaseManager dm = DatabaseManager.getDatabaseMangmentObj();

            return inserUpdatetObj.inserUpdatetData(query);
        }

        public bool updateCompany(Company company) {
            string query = "UPDATE Companies SET {0} = '{1}', {2} = {3}, {4} = '{5}' WHERE {6} = {7}";

            query = string.Format(query, 
               DatabaseVars.COMPANY_NAME,  company.CompanyName,
               DatabaseVars.DEBT_VALUE,    company.DebtValue, 
               DatabaseVars.MOBILE_NUMBER, company.MobileNum,
               DatabaseVars.COMPANY_ID,    company.CompanyID);

            return inserUpdatetObj.inserUpdatetData(query);
        }
        public ArrayList getAllCompanies(int whichCompanies)
        {
            string query;
            if(whichCompanies == DEBT_COMPANIES)
                query = "SELECT * FROM QCompanies";
            else
                query = "SELECT * FROM Companies";
            return getCompany(query);
        }

        private ArrayList getCompany(string query)
        {
            ArrayList companiesData = new ArrayList();
            Company company = new Company();

            OleDbConnection dbConnection = dbManager.openDb();
            this.comand.Connection = dbConnection;
            comand.CommandText = query;
            OleDbDataReader result = comand.ExecuteReader();

            while (result.Read())
            {
                company.CompanyID = result.GetInt32(0);
                company.CompanyName = result.GetString(1);
                try
                {
                    company.DebtValue = (double)result.GetDecimal(2);
                }
                catch (Exception e) {
                    company.DebtValue = 0;
                }
                company.MobileNum = result.GetString(3);
                companiesData.Add(company);
                company = new Company();
            }

            result.Close();
            dbManager.closeDb();
            return companiesData;
        }

        public Company getCompany(int id) {
            string query = "SELECT * FROM Companies WHERE {0} = {1};";
            query = string.Format(query,
               DatabaseVars.COMPANY_ID, id);
            return (Company)getCompany(query)[0];
        }


    }
}
