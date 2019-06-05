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

namespace ConcurrentOrdering.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public OrderController(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderCommand orderCommand)
        {
            return await
                // Try to generate an order
                from maybeOrder in
                    from product in _repository.FindByIdAsync(orderCommand.ProductId)
                    select OrderBehavior.TryOrder(product, orderCommand.Quantity)

                // Update the DB
                from result in maybeOrder.AwaitSideEffect(_repository.UpdateAsync)
                
                // Return results
                select result.Match<IActionResult>(
                    Ok,
                    StatusCode(500));
        }
    }
}