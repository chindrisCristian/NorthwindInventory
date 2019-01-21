using NorthwindInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindInventory.Services
{
   public static class ProductsService
    {


        
            /// <summary>
            /// Return a new list of Shippers as they are described in <see cref="Shipper"/>.
            /// </summary>
            /// <returns></returns>
            public static List<Product> GetProducts()
            {
                using (var context = new NorthwindEntities())
                {
                    var Products = from Product in context.Products
                                   select Product;
                    return Products.ToList();
                }
            }

   }
    
}
