using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrentOrdering.Web.Commands
{
    public class UpdateProductCommand
    {
        public int Id { get; set; }

        public int AvailableUnits { get; set; }
    }
}
