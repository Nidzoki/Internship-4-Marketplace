using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Entities
{
    public abstract class User
    {
        public string Username { get; set; }
        public string Email { get; set; }

        protected User(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
