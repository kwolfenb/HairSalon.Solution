using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
 
namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTest: IDisposable
  {
    public void Dispose()
    {
        Client.ClearAll();
    }

    public SpecialtyTest()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=kenny_wolfenberger_test;";
    }

    [TestMethod]
    public void Type_ReturnsTypeOfSpecialty_Specialty()
    {
      Specialty testSpecialty = new Specialty("haircuts");
      Assert.AreEqual(testSpecialty.GetType(), typeof(Specialty));
    }


  }
}