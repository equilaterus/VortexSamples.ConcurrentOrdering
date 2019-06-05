using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrentOrdering.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AvailableUnits { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
