using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BillGenerator
{
    public partial class frmPrint : Form
    {
        List<OrderDetail> _list;
        Orders _orders;

        public frmPrint(Orders orders, List<OrderDetail> list)
        {
            InitializeComponent();
            _orders = orders;
            _list = list;
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {
            rptOrders1.SetDataSource(_list);
            rptOrders1.SetParameterValue("pOrderID", _orders.OrderID);
            rptOrders1.SetParameterValue("pDate", _orders.OrderDate.ToString());
            rptOrders1.SetParameterValue("pContactName", _orders.ContactName);
            rptOrders1.SetParameterValue("pPostalCode", _orders.PostalCode);
            rptOrders1.SetParameterValue("pAddress", _orders.Address);
            rptOrders1.SetParameterValue("pCity", _orders.City);
            rptOrders1.SetParameterValue("pPhone", _orders.Phone);
            crystalReportViewer.ReportSource = rptOrders1;
            crystalReportViewer.Refresh();
        }
    }
}
