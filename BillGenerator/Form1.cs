using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace BillGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
            string query = "select o.OrderID,c.CustomerID,c.ContactName,c.Address,c.PostalCode,c.city,c.Phone,o.OrderDate from Orders o inner join Customers c on o.CustomerID = c.CustomerID "+ $"where o.OrderDate between '{ dtFromDate.Value}' and '{dtToDate.Value}'";
                ordersBindingSource.DataSource = db.Query<Orders>(query, commandType: CommandType.Text);

            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Orders obj = ordersBindingSource.Current as Orders;
            if (obj != null)
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string query = "Select d.OrderID,p.ProductName,d.Quantity,d.Discount,d.UnitPrice from [Order Details] d inner join Products p on d.ProductID = p.ProductID " +
                       $" where d.OrderID = '{obj.OrderID}' ";
                    List<OrderDetail> list = db.Query<OrderDetail>(query, commandType: CommandType.Text).ToList();

                    using (frmPrint frm = new frmPrint(obj,list))
                    {
                        frm.ShowDialog();
                    }
                }
            }
        }
    }
}
