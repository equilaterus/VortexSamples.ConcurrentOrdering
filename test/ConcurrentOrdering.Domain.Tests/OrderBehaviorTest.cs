using ConcurrentOrdering.Domain.Behaviors;
using ConcurrentOrdering.Domain.Models;
using System;
using Xunit;

namespace ConcurrentOrdering.Domain.Tests
{
    public class OrderBehaviorTest
    {
        [Fact]
        public void TryOrder_Unavailable_Returns_Empty()
        {
            // Execute
            var maybeProduct = OrderBehavior.TryOrder(new Product { AvailableUnits = 0 }, 1);

            // Check
            var result = maybeProduct.Match(p => p, null);
            Assert.Null(result);
        }

        [Fact]
        public void TryOrder_Unavailable_Returns_Product()
        {
            // Prepare
            var sourceProduct = new Product { AvailableUnits = 1 };

            // Execute
            var maybeProduct = OrderBehavior.TryOrder(sourceProduct, 1);

            // Check
            var result = maybeProduct.Match(p => p, null);
            Assert.NotNull(result);
            Assert.NotEqual(sourceProduct, result);
            Assert.Equal(0, result.AvailableUnits);
        }
    }
}
