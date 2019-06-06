using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConcurrentOrdering.Domain.Behaviors;
using ConcurrentOrdering.Domain.Infrastructure.Repositories;
using ConcurrentOrdering.Web.Commands;
using Microsoft.AspNetCore.Mvc;
using Equilaterus.Vortex;
using ConcurrentOrdering.Models;
using System.Diagnostics;
using ConcurrentOrdering.Domain.Models;

namespace ConcurrentOrdering.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public ProductController(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand updateProductCommand)
        {
            return await
                // Try to generate an order
                from maybeProduct in
                    from product in _repository.FindByIdAsync(updateProductCommand.Id)
                    select _mapper.MaybeMap(updateProductCommand, product)

                // Update the DB
                from result in maybeProduct.AwaitSideEffect(_repository.UpdateAsync)
                
                // Return results
                select result.Match<IActionResult>(
                    Ok, 
                    StatusCode(500, new { Error = "Error updating the product" }));
        }
    }
}