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
        public ActionResult Index(string stylistName, string stylistPhone, string stylistPicture, int specialtyId)
        {
            Stylist newStylist = new Stylist(stylistName, stylistPhone, stylistPicture);
            newStylist.Save();
            newStylist.AddStylistSpecialty(specialtyId);
            List<Stylist> allStylists = Stylist.GetAll();
            return View("Index", allStylists);
        }

        [HttpGet("/stylist/new")]
        public ActionResult New()
        {
            List<Specialty> allSpecialties = Specialty.GetAll();
            return View(allSpecialties);
        }

        [HttpGet("/stylist/show/{id}")]
        public ActionResult Show(int id)
        {
            Stylist currentStylist = Stylist.FindById(id);
            List<Client> clientList = Client.GetByStylistId(id);
            Dictionary<string, object> model = new Dictionary<string, object>{};
            model.Add("stylist", currentStylist);
            model.Add("clientList", clientList);
            return View(model);
        }

        [HttpPost("/stylist/update/{id}")]
        public ActionResult Show(int id, string stylistName, string stylistPhone, string stylistPicture)
        {
            Stylist.Update(id, stylistName, stylistPhone, stylistPicture);
            Stylist currentStylist = Stylist.FindById(id);
            List<Client> clientList = Client.GetByStylistId(id);
            Dictionary<string, object> model = new Dictionary<string, object>{};
            model.Add("stylist", currentStylist);
            model.Add("clientList", clientList);
            return View("Show", model);
        }

        [HttpGet("/stylist/update/{id}")]
        public ActionResult Update(int id)
        {
            Dictionary<string, object> model = new Dictionary<string,object>{};
            Stylist selectedStylist = Stylist.FindById(id);
            List<Specialty> allSpecialties = Specialty.GetAll();
            model.Add("specialties", allSpecialties);
            model.Add("stylist", selectedStylist);
            return View(model);
        }
    }
}
