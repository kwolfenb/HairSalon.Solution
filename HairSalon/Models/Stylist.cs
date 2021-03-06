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

        public string GetPhone()
        {
            return _phone;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetPicture()
        {
            return _picture;
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

        public void AddStylistSpecialty(int specialtyId, int stylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties(stylist_id, specialty_id) VALUES (@stylistId, @specialtyId);";
            cmd.Parameters.AddWithValue("@stylistId", stylistId);
            cmd.Parameters.AddWithValue("@specialtyId", specialtyId);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteStylistSpecialty(int specialtyId, int stylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists_specialties WHERE stylist_id=@stylistId AND specialty_id=@specialtyId;";
            cmd.Parameters.AddWithValue("@stylistId", stylistId);
            cmd.Parameters.AddWithValue("@specialtyId", specialtyId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Stylist> ReturnStylistsBySpecialty(int specialtyId)
        {
            List<Stylist> allStylists = new List<Stylist>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM specialties
                JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
                JOIN stylists ON (stylists.id = stylists_specialties.stylist_id)
                WHERE specialties.id=@specialtyId;";
            cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
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

        public static List<Specialty> ReturnSpecialtiesByStylist(int stylistId)
        {
            List<Specialty> allSpecialties = new List<Specialty>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialties.* FROM stylists
                JOIN stylists_specialties ON (stylists.id = stylists_specialties.stylist_id)
                JOIN specialties ON (specialties.id = stylists_specialties.specialty_id)
                WHERE stylists.id=@stylistId;";
            cmd.Parameters.AddWithValue("@stylistId", stylistId);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string description = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(description, id);
                allSpecialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }

        public static Stylist Update(int id, string stylistName, string stylistPhone, string stylistPicture)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name=@stylistName, phone=@stylistPhone, picture=@stylistPicture WHERE id = @stylistId;";
            cmd.Parameters.AddWithValue("@stylistId", id);
            cmd.Parameters.AddWithValue("@stylistName", stylistName);
            cmd.Parameters.AddWithValue("@stylistPhone", stylistPhone);
            cmd.Parameters.AddWithValue("@stylistPhone", stylistPhone);
            cmd.Parameters.AddWithValue("@stylistPicture", stylistPicture);
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

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", this._id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }            
        }

        public static void DeleteAll()
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


        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;
                DELETE FROM stylists_specialties;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }            
        }


        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                bool areIdsEqual = (this.GetId() == newStylist.GetId());
                bool areDescriptionsEqual = (this.GetName() == newStylist.GetName());
                return (areIdsEqual && areDescriptionsEqual);
            }
        }
        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }
    }
}
