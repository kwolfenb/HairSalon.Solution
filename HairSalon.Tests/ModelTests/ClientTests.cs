using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
 
namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest: IDisposable
  {
    public void Dispose()
    {
        Client.ClearAll();
    }

    public ClientTest()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=kenny_wolfenberger_test;";
    }
   
    [TestMethod]
    public void Type_ReturnsTypeOfClient_Client()
    {
      Client testClient = new Client("test name", 1, "555-555-5555", "notes");
      Assert.AreEqual(testClient.GetType(), typeof(Client));
    }
  }
}
