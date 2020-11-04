using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmay0._0._2.Database
{
    class DatabaseVars
    {
        
        #region company section

        public static string COMPANY_TABLE = "Companies";
        public static string COMPANY_ID = "COID";
        public static string COMPANY_NAME = "CompanyName";
        public static string COMPANY_MOBILE = "Mobile";

        #endregion

        #region customer section

        public static string CUSTOMER_TABLE = "Customers";
        public static string CUSTOMER_ID = "CID";
        public static string CUSTOMER_NAME = "CustomerName";
        public static string CUSTOMER_MOBILE = "MobileNumber";

        #endregion

        #region goods section

        public static string GOODS_TABLE = "Goods";
        public static string GOODS_ID = "GID";
        public static string GOODS_PARCODE = "GParcode";
        public static string GOODS_NAME = "GName";
        public static string GOODS_PIECE_PER_UNIT = "PicecePerUnit";
        public static string GOODS_TOTAL_AMOUNT = "TotalAmount";
        public static string GOODS_EXPIRATION = "ExpirationDate";
        public static string GOODS_BUYING_PRICE = "BuyingPrice";
        public static string GOODS_SELLING_PRICE = "SellingPrice";

        #endregion

        #region goods and orders section

        public static string GOODS_ORDER_TABEL = "GoodsAndOrders";
        public static string GOODS_ORDER_ID = "GAOID";
        public static string GOODS_ORDER_GOODS_ID= "GID";
        public static string GOODS_ORDER_ORDERS_ID = "OID";
        public static string GOODS_ORDER_AMOUNT = "Amount";
        public static string GOODS_ORDER_AMOUNT_VALUE = "AmountValue";

        #endregion

        #region old goods section

        public static string OLD_GOOD_TABEL = "OldGoods";
        public static string OLD_GOOD_ID = "OGID";
        public static string OLD_GOOD_GOODS_ID = "GID";
        public static string OLD_GOOD_AMOUNT = "Amount";
        public static string OLD_GOOD_EXPIRATION_DATE = "ExpirationDate";
        public static string OLD_GOOD_BUYING_PRICE = "BuyingPrice";
        public static string OLD_GOOD_VALUE = "OGValue";

        #endregion

        #region orders section

        public static string ORDER_TABEL = "Orders";
        public static string ORDER_ID = "OID";
        public static string ORDER_CUSTOMER_ID = "CID";
        public static string ORDER_DATE = "OrderDate";
        public static string ORDER_TOTAL_PRICE = "TotalPrice";
        public static string ORDER_PAID_AMOUNT = "PaidAmount";
        public static string ORDER_DEPTAMOUNT = "DeptAmount";

        #endregion

        #region shipments section

        public static string SHIPMENT_TABLE = "Shipments";
        public static string SHIPMENT_ID = "SID";
        public static string SHIPMENT_COMPANY_ORDER_ID = "COID";
        public static string SHIPMENT_DATE = "ShipmentDate";
        public static string SHIPMENT_TOTAL_PRICE = "TotalPrice";
        public static string SHIPMENT_PAID_AMOUNT = "PaidAmount";
        public static string SHIPMENT_DEPT_AMOUNT = "DeptAmount";

        #endregion

        #region shipments goods section 

        public static string SHIPMENT_GOODS_TABLE = "ShipmentsGoods";
        public static string SHIPMENT_GOODS_ID = "SGID";
        public static string SHIPMENT_GOODS_SHIPMENT_ID = "SID";
        public static string SHIPMENT_GOODS_GOOD_ID = "GID";
        public static string SHIPMENT_GOODS_AMOUNT = "Amount";
        public static string SHIPMENT_GOODS_TOTAL_PRICE = "TotalPrice";

        #endregion

    }
}
