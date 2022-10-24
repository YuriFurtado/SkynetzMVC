﻿using Microsoft.AspNetCore.Mvc;
using SkynetzMVC.Controllers.DTO;
using SkynetzMVC.Repositories;
using SkynetzMVC.Services;
using System.Threading.Tasks;

namespace SkynetzMVC.Controllers
{
    public class AjaxHomeController : Controller
    {

        public readonly HomeService homeService;
        public readonly TariffRepository tariffRepository;
        public readonly PlanRepository planRepository;
        public readonly HomeController homeController;

        public AjaxHomeController()
        {
            homeService = new HomeService();
            tariffRepository = new TariffRepository();
            planRepository = new PlanRepository();
            homeController = new HomeController();
        }

        public IActionResult AjaxIndex()
        {
            return View();
        }

        public async Task<ActionResult> LoadSelectTariff()
        {
            var listTariff = tariffRepository.GetAll();

            return Json(listTariff);
        }
        public async Task<ActionResult> LoadSelectPlan()
        {
            var listPlan = planRepository.GetAll();

            return Json(listPlan);
        }

        public ActionResult PriceCalculation(string idTariff, int usedMinutes, string usedPlan)
        {

            ResultDTO price = homeService.Result(idTariff, usedMinutes, usedPlan);

            return Json(price);
        }
    }
}
