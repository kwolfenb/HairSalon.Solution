using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private string _phone;
        private string _picture;

        public Stylist (string name, string phone, string picture = "none")
        {
            _name = name;
            _phone = phone;
            _picture = picture;
        }
//Setters
        public void SetId(int id)
        {
            _id = id;
        }
//Getters
        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists(name, phone, picture) VALUES (@stylistName, @stylistPhone, @picture);";
            cmd.Parameters.AddWithValue("@stylistName", this._name);
            cmd.Parameters.AddWithValue("@stylistPhone", this._phone);
            cmd.Parameters.AddWithValue("@picture", this._picture);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Stylist FindById (int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = @stylistId;";
            cmd.Parameters.AddWithValue("@stylistId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            string name = "";
            string phone = "";
            string picture = "";
            while(rdr.Read())
            {
                name = rdr.GetString(1);
                phone = rdr.GetString(2);
                picture = rdr.GetString(3);
            }
            Stylist foundStylist = new Stylist(name, phone, picture);
            foundStylist.SetId(id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundStylist;
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                string phone = rdr.GetString(2);
                string picture = rdr.GetString(3);
                Stylist newStylist = new Stylist(stylistName, phone, picture);
                newStylist.SetId(stylistId);
                allStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }            
        }
        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool areIdsEqual = (this.GetId() == newClient.GetId());
                bool areDescriptionsEqual = (this.GetName() == newClient.GetName());
                return (areIdsEqual && areDescriptionsEqual);
            }
        }
        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }
    }
}
