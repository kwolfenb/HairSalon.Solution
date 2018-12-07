using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_ReturnsCorrect_View()
        {
            HomeController testController = new HomeController();

            ActionResult indexView = testController.Index();

            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }

    }
}
