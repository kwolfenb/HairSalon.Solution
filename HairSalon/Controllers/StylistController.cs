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
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpPost("/stylist/index")]
        public ActionResult Index(string stylistName, string stylistPhone, string stylistPicture)
        {
            Stylist newStylist = new Stylist(stylistName, stylistPhone, stylistPicture);
            newStylist.Save();
            List<Stylist> allStylists = Stylist.GetAll();
            
            return View("Index", allStylists);
        }

        [HttpGet("/stylist/new")]
        public ActionResult New()
        {
            return View();
        }
    }
}
