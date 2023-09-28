using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGUI
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Birth { get; set; }
        public string Address { get; set; }

        public Users(int id, string name, DateOnly birth, string address)
        {
            Id = id;
            Name = name;
            Birth = birth;
            Address = address;
        }

        public Users()
        {
        }
   
    }
}
