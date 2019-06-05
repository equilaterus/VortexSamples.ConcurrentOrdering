using AutoMapper;
using ConcurrentOrdering.Domain.Models;
using ConcurrentOrdering.Web.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrentOrdering.Web.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}
