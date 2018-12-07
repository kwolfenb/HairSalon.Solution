using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
 
namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest: IDisposable
  {
    public void Dispose()
    {
        Stylist.ClearAll();
    }

    public StylistTest()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=kenny_wolfenberger_test;";
    }

    [TestMethod]
    public void Type_ReturnsTypeOfStylist_Stylist()
    {
      Stylist testStylist = new Stylist("style name", "555-555-5555");
      Assert.AreEqual(testStylist.GetType(), typeof(Stylist));
    }

    [TestMethod]
    public void Save_SaveToStylistTable_NameMatch()
    {
      //Arrange
      string stylistName = "Stylish";
      Stylist testStylist = new Stylist(stylistName, "555-555-5555");

      //Act
      testStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      Stylist resultStylist = allStylists[0];
      string resultName = resultStylist.GetName();
      
      //Assert
      Assert.AreEqual(stylistName, resultName);
    }

  }
}
