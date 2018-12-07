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

        public Stylist (string name, string phone, string picture)
        {
            _name = name;
            _phone = phone;
            _picture = picture;
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
