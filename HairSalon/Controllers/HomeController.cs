using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/home/contact")]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet("/home/admin")]
        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet("/home/delete/stylists")]
        public ActionResult ShowStylists()
        {
            List<Stylist>allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpGet("/home/delete/stylists/confirm")]
        public ActionResult DeleteStylists()
        {
            Stylist.DeleteAll();
            return RedirectToAction("ShowStylists");
        }

        [HttpGet("/home/delete/clients")]
        public ActionResult ShowClients()
        {
            List<Client>allClients = Client.GetAll();
            return View(allClients);
        }

        [HttpGet("/home/delete/clients/confirm")]
        public ActionResult DeleteClients()
        {
            Client.DeleteAll();
            return RedirectToAction("ShowClients");
        }

    }
}
