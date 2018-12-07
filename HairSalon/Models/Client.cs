using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Client
    {
        private int _id;
        private string _name;
        private int _stylistId;
        private string _phone;
        private string _notes;

        public Client(string name, int stylistId, string phone, string notes ="")
        {
            _name = name;
            _stylistId = stylistId;
            _phone = phone;
            _notes = notes;
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

        public int GetStylistId()
        {
            return _stylistId;
        }

        public string GetNotes()
        {
            return _notes;
        }
        public string GetPhone()
        {
            return _phone;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients(name, stylist_id, phone, notes) VALUES (@clientName, @stylistId, @clientPhone, @clientNotes);";
            cmd.Parameters.AddWithValue("@clientName", this._name);
            cmd.Parameters.AddWithValue("@stylistId", this._stylistId);
            cmd.Parameters.AddWithValue("@clientPhone", this._phone);
            cmd.Parameters.AddWithValue("@clientNotes", this._notes);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Client FindById (int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = @clientId;";
            cmd.Parameters.AddWithValue("@clientId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            string name = "";
            int stylistId =0;
            string phone = "";
            string notes = "";

            while(rdr.Read())
            {
                name = rdr.GetString(1);
                stylistId = rdr.GetInt32(2);
                phone = rdr.GetString(3);
                notes = rdr.GetString(4);
            }
            Client foundClient = new Client(name, stylistId, phone, notes);
            foundClient.SetId(id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundClient;
        }

        public static List<Client> GetByStylistId(int stylistId)
        {
            List<Client> clientList = new List<Client>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylistId;";
            cmd.Parameters.AddWithValue("@stylistId", stylistId);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                int stylist_id = rdr.GetInt32(2);
                string phone = rdr.GetString(3);
                string notes = rdr.GetString(4);
                Client newClient = new Client(clientName, stylist_id, phone, notes);
                newClient.SetId(clientId);
                clientList.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return clientList;
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                int stylist_id = rdr.GetInt32(2);
                string phone = rdr.GetString(3);
                string notes = rdr.GetString(4);
                Client newClient = new Client(clientName, stylist_id, phone, notes);
                newClient.SetId(clientId);
                allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
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
