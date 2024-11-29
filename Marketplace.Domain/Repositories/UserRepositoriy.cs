using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Data;
using Marketplace.Data.Entities;

namespace Marketplace.Domain.Repositories
    {
        public class UserRepository
        {
            private readonly Data.Program.Context marketplace;

            public UserRepository(Data.Program.Context marketplaceContext)
            {
                marketplace = marketplaceContext;
            }

            public void AddUser(User user) => marketplace.Users.Add(user);

            public User GetUser(Guid id) => marketplace.Users.Find(x => x.Id == id);

            public User GetUser(string username, string email) => marketplace.Users.Find(x => x.Username == username && x.Email == email);

            public List<User> GetUserList() => marketplace.Users;
        }
    }


