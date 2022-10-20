using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkynetzMVC.Controllers.DTO;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using SkynetzMVC.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkynetzMVC.Controllers
{
    public class HomeController : Controller
    {
        public readonly HomeService homeService;
        public readonly TariffRepository tariffRepository;
        public readonly PlanRepository planRepository;

        public HomeController()
        {
            homeService = new HomeService();
            tariffRepository = new TariffRepository();
            planRepository = new PlanRepository();
        }

        public void FillSelect()
        {
            var testTariff = tariffRepository.GetAll();
            var testPlan = planRepository.GetAll();

            List<SelectListItem> selectListTariffs = new List<SelectListItem>();

            foreach (Tariff tariff in testTariff)
            {
                SelectListItem selectListItem = new SelectListItem() { Value = tariff.Id.ToString(), Text = "De " + tariff.Source + " para " + tariff.Destination };

                selectListTariffs.Add(selectListItem);
            }

            ViewBag.TariffItems = selectListTariffs;


            List<SelectListItem> selectListPlans = new List<SelectListItem>();

            foreach (Plan plan in testPlan)
            {
                SelectListItem selectListItem = new SelectListItem() { Value = plan.Name, Text = plan.Name };

                selectListPlans.Add(selectListItem);
            }

            ViewBag.PlanItems = selectListPlans;

            return;
        }
        
        public IActionResult Index()
        {
            if (ViewBag.PriceCalculation == null)
            {
                ViewBag.PriceCalculation = new ResultDTO();
            }

            FillSelect();

            return View();
        }

        public async Task<ActionResult> PriceCalculation(string idTariff, int usedMinutes, string usedPlan)
        {

            ResultDTO price = homeService.Result(idTariff, usedMinutes, usedPlan);

            FillSelect();

            return View("Index", price);

            //ViewBag.PriceCalculation = price;

            //return RedirectToAction("Index");
        }
        
    }
}
