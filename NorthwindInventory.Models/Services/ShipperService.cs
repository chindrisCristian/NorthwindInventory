using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindInventory.Models.Services
{
	public static class ShipperService
	{
		/// <summary>
		/// Return a new list of Shippers as they are described in <see cref="Shipper"/>.
		/// </summary>
		/// <returns></returns>
		public static List<Shipper> GetShippers()
		{
			using (var context = new NorthwindEntities())
			{
				var Shippers = from Shipper in context.Shippers
							   select Shipper;
				return Shippers.ToList();
			}
		}

	}
}
