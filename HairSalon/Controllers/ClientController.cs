using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet("/client/index")]
        public ActionResult Index()
        {
            List<Client> clientList = Client.GetAll();
            return View(clientList);
        }

        [HttpPost("/client/index")]
        public ActionResult Index(string clientName, string clientPhone, int stylistName, string clientNotes)
        {
            Client newClient = new Client(clientName, stylistName, clientPhone, clientNotes);
            newClient.Save();
            List<Client> clientList = Client.GetAll();
            return View("Index", clientList);
        }

        [HttpGet("/client/new")]
        public ActionResult New()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }
    }
}
