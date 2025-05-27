using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIPC.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string Condition { get; set; }

        public bool IsDriver { get; set; }
    }
}
