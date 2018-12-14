using System;
using System.Linq;
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
            int stylistId = newStylist.GetId();
            newStylist.AddStylistSpecialty(specialtyId, stylistId);
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
            List<Specialty> stylistSpecialties = Stylist.ReturnSpecialtiesByStylist(id);
            Dictionary<string, object> model = new Dictionary<string, object>{};
            model.Add("stylist", currentStylist);
            model.Add("clientList", clientList);
            model.Add("specialties", stylistSpecialties);
            return View(model);
        }

        [HttpPost("/stylist/update/{id}")]
        public ActionResult Show(int id, string stylistName, string stylistPhone, string stylistPicture, List<int> specialty)
        {
            Stylist.Update(id, stylistName, stylistPhone, stylistPicture);
            Stylist currentStylist = Stylist.FindById(id);
            List<Specialty> currentSpecialties = Stylist.ReturnSpecialtiesByStylist(id);
            List<Specialty> allSpecialties = Specialty.GetAll();
            List<int> currentSpecialtiesInt = new List<int>{};
            List<int> allSpecialtiesInt = new List<int>{};
//Generates Int list of specialties to compare existing specialties with the new specialties to ensure no duplicates. 
            foreach(var x in currentSpecialties)
            {
                currentSpecialtiesInt.Add(x.GetId());
            }
            foreach(var y in allSpecialties)
            {
                allSpecialtiesInt.Add(y.GetId());
            }
            foreach(int selection in specialty)
            {
                if(currentSpecialtiesInt.Contains(selection)==false)
                {
                currentStylist.AddStylistSpecialty(selection, id);
                }
            }
            var nonSpecialties = allSpecialtiesInt.Except(specialty);
            foreach(int z in nonSpecialties)
            {
                Stylist.DeleteStylistSpecialty(z, id);
            }
            return RedirectToAction("Show", id);
        }

        [HttpGet("/stylist/update/{id}")]
        public ActionResult Update(int id)
        {
            Dictionary<string, object> model = new Dictionary<string,object>{};
            Stylist selectedStylist = Stylist.FindById(id);
            int stylistId = selectedStylist.GetId();
            List<Specialty> allSpecialties = Specialty.GetAll();
            List<Specialty> stylistSpecialties = Stylist.ReturnSpecialtiesByStylist(stylistId);
            model.Add("specialties", allSpecialties);
            model.Add("stylist", selectedStylist);
            model.Add("stylistSpecialties", stylistSpecialties);
            return View(model);
        }

        [HttpGet("/stylist/delete/{id}")]
        public ActionResult Delete(int id)
        {
            Stylist stylist = Stylist.FindById(id);
            stylist.Delete();
            return View(stylist);
        }
    }
}
