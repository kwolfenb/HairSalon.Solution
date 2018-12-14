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
    
    [TestMethod]
    public void GetDescription_ReturnsDescriptionFromSpecialty_Description()
    {
      string testDescription = "haircuts";
      Specialty testSpecialty = new Specialty(testDescription);
      string result = testSpecialty.GetDescription();
      Assert.AreEqual(testDescription, result);
    }

    [TestMethod]
    public void GetId_ReturnsSpecialtyId_Id()
    {
      int testId = 5;
      Specialty testSpecialty = new Specialty("description", testId);
      int result = testSpecialty.GetId();
      Assert.AreEqual(testId, result);
    }


  }
}