namespace NorthwindInventory.Models
{
	public class CustomerModel
	{
		#region Properties and fields

		public string CustomerID { get; set; }

		public string CompanyName { get; set; }

		public string ContactName { get; set; }

		public string ContactTitle { get; set; }

		public string Address { get; set; }

		public string Phone { get; set; }

		#endregion

		#region Constructor

		public CustomerModel()
		{

		}

		#endregion
	}
}
