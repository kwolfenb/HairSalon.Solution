using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private string _description;

    public Specialty(string description, int id=0)
    {
      _id = id;
      _description = description;
    }

    public string GetDescription()
    {
      return _description;
    }

    public int GetId()
    {
      return _id;
    }
    
    public void Save()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO specialties(description) VALUES (@description);";
        cmd.Parameters.AddWithValue("@description", this._description);

        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }
    
    public static Specialty FindById(int specialtyId)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialties WHERE id = @specialtyId;";
        cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        string description = "";
        int id = 0;
        while(rdr.Read())
        {
            id = rdr.GetInt32(0);
            description = rdr.GetString(1);
        }
        Specialty foundSpecialty = new Specialty(description, id);
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return foundSpecialty;
    }

    public static List<Specialty> GetAll()
    {
        List<Specialty> allSpecialties = new List<Specialty>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialties;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int specialtyId = rdr.GetInt32(0);
            string specialtyDescription = rdr.GetString(1);
            Specialty newSpecialty = new Specialty(specialtyDescription, specialtyId);
            allSpecialties.Add(newSpecialty);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allSpecialties;
    }

    public static void ClearAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM specialties;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if(conn != null)
        {
            conn.Dispose();
        }            
    }
    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
          return false;
      }
      else
      {
          Specialty newSpecialty = (Specialty) otherSpecialty;
          bool areIdsEqual = (this.GetId() == newSpecialty.GetId());
          bool areDescriptionsEqual = (this.GetDescription() == newSpecialty.GetDescription());
          return (areIdsEqual && areDescriptionsEqual);
      }
    }

    public override int GetHashCode()
    {
        return this.GetDescription().GetHashCode();
    }
  }
}