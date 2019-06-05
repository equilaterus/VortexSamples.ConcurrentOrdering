using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrentOrdering.Web.Commands
{
    public class OrderCommand
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
