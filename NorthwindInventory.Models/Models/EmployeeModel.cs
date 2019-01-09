using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindInventory.Models
{
	public class EmployeeModel
	{
		public int EmployeeID { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string Title { get; set; }

		public string TitleOfCourtesy { get; set; }

		public string FullName => string.Format("{0} {1} {2} {3}", TitleOfCourtesy, Title, FirstName, LastName);

		public DateTime? BirthDate { get; set; }

		public DateTime? HireDate { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string PostalCode { get; set; }

		public string Country { get; set; }

		public string FullAddress => string.Format("{0}, {1}, {2}, {3}", Address, City, PostalCode, Country);

		public string HomePhone { get; set; }

		public byte[] Photo { get; set; }

		public string Notes { get; set; }

		public int? ReportsTo { get; set; }

		public string PhotoPath { get; set; }

		public string Password { get; set; }

		public string UserName => string.Format("{0}.{1}", FirstName.ToLower(), LastName.ToLower());
	}
}
