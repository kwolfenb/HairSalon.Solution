using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class SpecialtyController : Controller
    {
        [HttpGet("/specialty")]
        public ActionResult Index()
        {
          List<Specialty> allSpecialties = Specialty.GetAll();
          return View(allSpecialties);
        }

        [HttpGet("/specialty/new")]
        public ActionResult New()
        {
          return View();
        }

        [HttpPost("/specialty")]
        public ActionResult Create(string specialtyDescription)
        {
          Specialty specialty = new Specialty(specialtyDescription);
          specialty.Save();
          return RedirectToAction("Index");
        }

        [HttpGet("/specialty/show/{id}")]
        public ActionResult Show(int id)
        {
          Dictionary<string, object> model = new Dictionary<string,object>{};
          Specialty newSpecialty = Specialty.FindById(id);
          List<Stylist> stylists = Stylist.ReturnStylistsBySpecialty(id);
          model.Add("specialty",newSpecialty);
          model.Add("stylists",stylists);
          return View(model);
        }
    }
}
