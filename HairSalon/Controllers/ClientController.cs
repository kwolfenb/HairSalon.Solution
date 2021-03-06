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

        [HttpGet("/client/new")]
        public ActionResult New()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpGet("/client/show/{id}")]
        public ActionResult Show(int id)
        {
            Client selectedClient = Client.FindById(id);
            int stylistId = selectedClient.GetStylistId();
            Stylist selectedStylist = Stylist.FindById(stylistId);
            Dictionary<string, object> model = new Dictionary<string, object>{};
            model.Add("client", selectedClient);
            model.Add("stylist", selectedStylist);

            return View(model);
        }

        [HttpPost("/client/show")]
        public ActionResult Show(string clientName, string clientPhone, int stylistName, string clientNotes)
        {
            Client newClient = new Client(clientName, stylistName, clientPhone, clientNotes);
            newClient.Save();
            int stylistId = newClient.GetStylistId();
            Stylist selectedStylist = Stylist.FindById(stylistId);
            Dictionary<string, object> model = new Dictionary<string, object>{};
            model.Add("client", newClient);
            model.Add("stylist", selectedStylist);
            return View("Show", model);
        }

        [HttpPost("/client/update/{id}")]
        public ActionResult Update(int id, string clientName, string phone, string clientNotes)
        {
            Client.Update(id, clientName, phone, clientNotes);
            return RedirectToAction("Show", id);

        }

        [HttpGet("/client/update/{id}")]
        public ActionResult Update (int id)
        {
            Dictionary<string, object> model = new Dictionary<string,object>{};
            Client selectedClient = Client.FindById(id);
            int stylistId = selectedClient.GetStylistId();
            Stylist selectedStylist = Stylist.FindById(stylistId);
            List<Stylist> allStylists = Stylist.GetAll();
            model.Add("client", selectedClient);
            model.Add("stylist", selectedStylist);
            model.Add("allStylists", allStylists);
            return View(model);
        }


        [HttpGet("/client/delete/{id}")]
        public ActionResult Delete(int id)
        {
            Client.DeleteById(id);
            return View();
        }
    }
}
