using System;

namespace NorthwindInventory.Models
{
	public class OrderDetailModel
	{
		#region Properties and fields

		public int OrderID { get; set; }

		public string ProductName { get; set; }

		public string CategoryName { get; set; }

		public decimal UnitPrice { get; set; }

		public float Discount { get; set; }

		public int Quantity { get; set; }

		public decimal TotalPrice => Quantity * (UnitPrice - (decimal)Discount * UnitPrice);

		#endregion

		#region Constructor

		public OrderDetailModel()
		{
		
		}

		#endregion
	}
}
