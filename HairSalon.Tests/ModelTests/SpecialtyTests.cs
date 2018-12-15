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
        Specialty.ClearAll();
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
      //Arrange
      string testDescription = "haircuts";

      //Act
      Specialty testSpecialty = new Specialty(testDescription);
      string result = testSpecialty.GetDescription();

      //Assert
      Assert.AreEqual(testDescription, result);
    }

    [TestMethod]
    public void GetId_ReturnsSpecialtyId_Id()
    {
      //Arrange
      int testId = 5;
      
      //Act
      Specialty testSpecialty = new Specialty("description", testId);
      int result = testSpecialty.GetId();
      
      //Assert
      Assert.AreEqual(testId, result);
    }


    [TestMethod]
      public void GetAll_ReturnsListSpecialties_List()
      {
        //Arrange
        Specialty testSpecialtyOne = new Specialty("specialty one");
        Specialty testSpecialtyTwo = new Specialty("specialty two");
        testSpecialtyOne.Save();
        testSpecialtyTwo.Save();

        //Act
        List<Specialty> testList = new List<Specialty>{testSpecialtyOne, testSpecialtyTwo};
        List<Specialty> result = Specialty.GetAll();

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }

      
    [TestMethod]
      public void FindById_ReturnSpecialtyById_Specialty()
      {
        //Arrange
        Specialty testSpecialty = new Specialty("description");
        testSpecialty.Save();

        //Act
        int resultId = testSpecialty.GetId();
        Specialty resultSpecialty= Specialty.FindById(resultId);
        Console.WriteLine(testSpecialty.GetDescription());
        Console.WriteLine(resultSpecialty.GetDescription());
        
        //Assert
        Assert.AreEqual(testSpecialty, resultSpecialty);
      }

  }
}