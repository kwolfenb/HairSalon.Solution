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

        public static List<Client> GetByStylistId(int stylistId)
        {
            
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
