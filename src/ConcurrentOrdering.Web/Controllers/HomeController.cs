using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConcurrentOrdering.Models;
using ConcurrentOrdering.Domain.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace ConcurrentOrdering.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _repository;

        public HomeController(IMapper mapper, IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.FindAllAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Get the details of the exception that occurred
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature is null)
                return StatusCode(500, "Unknown error");

            if (!exceptionFeature.Path.Contains("/api/", StringComparison.InvariantCultureIgnoreCase))
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            
            if (exceptionFeature.Error is DbUpdateConcurrencyException)
                return Json(new { Error = "Concurrency exception", AllowRetry = true });
            else
                return Json(new { exceptionFeature.Error });
        }
    }
}
