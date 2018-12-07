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

    [TestMethod]
    public void Save_SaveToClientTable_NameMatch()
    {
      //Arrange
      string clientName = "Cathy Client";
      Client testClient = new Client(clientName, 1, "555-555-5555", "notes");

      //Act
      testClient.Save();
      List<Client> clients = Client.GetAll();
      Client resultClient = clients[0];
      string resultName = resultClient.GetName();
      
      //Assert
      Assert.AreEqual(clientName, resultName);
    }

    [TestMethod]
    public void GetAll_RetrieveAllClientsFromDB_Client()
    {
      //Arrange
      Client clientOne = new Client("Cathy", 1, "555-555-5555", "notes");
      Client clientTwo = new Client("John", 1, "555-555-5555", "notes");
      clientOne.Save();
      clientTwo.Save();

      //Act
      List<Client> clients = Client.GetAll();
      Client resultClientOne = clients[0];
      Client resultClientTwo = clients[1];
      
      //Assert
      Assert.AreEqual(clientOne, resultClientOne);
      Assert.AreEqual(clientTwo, resultClientTwo);
    }
  }
}
