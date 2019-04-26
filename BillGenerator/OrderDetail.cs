using System;

namespace BillGenerator
{
    public class OrderDetail
    {
        public int OrderID { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Total
        {
            get
            {
                return Quantity * UnitPrice - Quantity * UnitPrice * Discount;
            }
        }
    }
}
