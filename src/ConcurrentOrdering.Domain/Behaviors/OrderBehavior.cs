using ConcurrentOrdering.Domain.Models;
using Equilaterus.Vortex;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConcurrentOrdering.Domain.Behaviors
{
    public static class OrderBehavior
    {
        public static Maybe<Product> TryOrder(Product product, int quantity)
        {
            if (quantity > product.AvailableUnits)
            {
                return new Maybe<Product>();
            }

            var newProduct = new Product(product);
            newProduct.AvailableUnits -= quantity;
            return new Maybe<Product>(newProduct);            
        }
    }
}
