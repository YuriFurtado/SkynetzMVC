using Microsoft.AspNetCore.Mvc;
using SkynetzMVC.Services;
using System;

namespace SkynetzMVC.Controllers
{
    public class HomeController : Controller
    {
        public readonly HomeService homeService;

        public HomeController()
        {
            homeService = new HomeService();
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PriceCalculation(string source, string destination, int usedMinutes, string usedPlan)
        {

            var price = homeService.Results(source, destination, usedMinutes, usedPlan);

            return View(price);
        }
        
    }
}
