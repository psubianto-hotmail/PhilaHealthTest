using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhilaHealthTest.Models;

namespace PhilaHealthTest.Controllers
{
    public class RealEstateTaxController : Controller
    {
        public IActionResult Index()
        {
			var model = new Brt();
			return View(model);
        }

		public IActionResult Result()
		{
			var model = new List<TaxDetail>();

			return View("Result", model);
		}

		[HttpPost]
		public IActionResult Index(Brt brt)
		{


			return RedirectToAction("Result");
		}
    }
}