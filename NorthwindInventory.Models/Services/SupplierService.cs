using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NorthwindInventory.Models.Services
{
	public class SupplierService
	{
		/// <summary>
		/// Return a new list of suppliers as they are described in <see cref="Supplier"/>.
		/// </summary>
		/// <returns></returns>
		public static List<Supplier> GetSuppliers()
		{
			using(var context = new NorthwindEntities())
			{
				var suppliers = from supplier in context.Suppliers
								select supplier;
				return suppliers.ToList();
			}
		}

		/// <summary>
		/// Removes a supplier from the database.
		/// </summary>
		/// <param name="supplier">The supplier that needs to be removed.</param>
		/// <returns>Whether the removal was allowed or not.</returns>
		public static string RemoveSupplier(Supplier supplier)
		{
			try
			{
				using (var context = new NorthwindEntities())
				{
					var supplierToDelete = from s in context.Suppliers
										   where s.SupplierID == supplier.SupplierID
										   select s;
					context.Suppliers.Remove(supplierToDelete.First());
					context.SaveChanges();
					return "The supplier " + supplier.CompanyName + " was succesfully removed!";
				}
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		/// <summary>
		/// Adds a new supplier.
		/// </summary>
		/// <param name="supplier">The supplier to be added.</param>
		public static void AddSupplier(Supplier supplier)
		{
			using(var context = new NorthwindEntities())
			{
				var getMaxSupplierId = context.Suppliers.Max(x => x.SupplierID);
				supplier.SupplierID = getMaxSupplierId + 1;

				context.Suppliers.Add(supplier);
				context.SaveChanges();
			}
		}

		/// <summary>
		/// Returns a list of products that come from a certain supplier.
		/// </summary>
		/// <param name="selectedSupplier">The supplier'products required.</param>
		/// <returns></returns>
		public static List<Product> GetProductsPerSupplier(Supplier selectedSupplier)
		{
			using(var context = new NorthwindEntities())
			{
				List<Product> products = (from s in context.Suppliers
										  join p in context.Products
										  on s.SupplierID equals p.SupplierID
										  where s.SupplierID == selectedSupplier.SupplierID
										  select p).ToList();
				foreach(var product in products)
				{
					product.Category.CategoryName = (from p in context.Products
													 where p.ProductID == product.ProductID
													select p.Category.CategoryName).First();
				}

				return products;
			}
		}

		/// <summary>
		/// Save the modifies to the database.
		/// </summary>
		/// <param name="suppliers">The modified suppliers.</param>
		public static void Save(List<Supplier> suppliers)
		{
			//To do...
		}
	}
}
