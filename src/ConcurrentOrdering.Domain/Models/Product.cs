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

        public string ImageUrl { get; set; }

        public int AvailableUnits { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }


        public Product() { }

        public Product(Product previous)
        {
            Id = previous.Id;
            Name = previous.Name;
            ImageUrl = previous.ImageUrl;
            AvailableUnits = previous.AvailableUnits;
            RowVersion = previous.RowVersion;
        }
    }
}
