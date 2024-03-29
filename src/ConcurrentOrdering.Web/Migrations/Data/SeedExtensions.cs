﻿using ConcurrentOrdering.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConcurrentOrdering.Domain.Infrastructure.Data
{
    public static class SeedExtensions
    {
        public static readonly Product[] ProductsSeed = new Product[] {
            new Product
            {
                Id = 1,
                Name = "Thimbleweed Park Diskette",
                AvailableUnits = 3,
                ImageUrl = "https://i.ytimg.com/vi/zRmh1aFHHQQ/hqdefault.jpg"
            },
            new Product
            {
                Id = 2,
                Name = "Return of the Obra Dinn",
                AvailableUnits = 3,
                ImageUrl = "https://i.ytimg.com/vi/GWMtT0EFX_0/hqdefault.jpg"
            }
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
               ProductsSeed
            );
        }
    }
}
