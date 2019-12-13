using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearningMVC.Controllers
{
    public class FoodController : Controller
    {

        //GET: /Food/
        public IActionResult Index()
        {
            return View();
        }

        //Get: /Food/Brand
        public IActionResult Brand(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}