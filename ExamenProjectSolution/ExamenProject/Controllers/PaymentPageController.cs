using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamenProject.Models;

namespace ExamenProject.Controllers
{
    public class PaymentPageController : Controller
    {
        // GET: PaymentPage
        public ActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Payment(ExamenDataModel examen)
        {
            return View();
        }
    }
}