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
      List<Client> testClients = new List<Client>{clientOne, clientTwo};
      List<Client> resultClients = Client.GetAll();
      
      //Assert
      CollectionAssert.AreEqual(testClients, resultClients);
    }

    [TestMethod]
    public void GetByStylistId_ReturnClientsByStylistId_ClientList()
    {
      //Arrange
      int stylistId = 10;
      int otherStylistId = 11;
      Client clientOne = new Client("John", stylistId, "555-555-5555");
      Client clientTwo = new Client("Jane", stylistId, "555-555-5555");
      Client clientThree = new Client("Doe", otherStylistId, "555-555-5555"); //clientThree uses different Stylist so should NOT appear in result list
      clientOne.Save();
      clientTwo.Save();
      clientThree.Save();

      //Act
      List<Client> clientList = new List<Client> {clientOne, clientTwo};
      List<Client> resultList = Client.GetByStylistId(stylistId);

      //Assert
      CollectionAssert.AreEqual(clientList, resultList);
    }

    

  }
}
