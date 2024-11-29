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
        public Guid Id { get; set; }

        protected User(string username, string email)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
        }
    }
}
