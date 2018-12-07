using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistController : Controller
    {
        [HttpGet("/stylist/index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
