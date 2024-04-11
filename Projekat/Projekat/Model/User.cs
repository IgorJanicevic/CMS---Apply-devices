using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Projekat.Model
{
    [Serializable]
    public enum Role { Admin, Guest };
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public User(string name, string password, Role role)
        {
            this.Name = name;
            this.Password = password;
            this.Role = role;
        }

        public User()
        {
        }
    }
}
