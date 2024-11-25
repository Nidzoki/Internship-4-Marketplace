using Marketplace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Marketplace.Presentation
{
    public static class ValidateInput
    {
        public static bool ValidateUsername(string username) 
            => Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$") 
            && username.Length >= 3 
            && username.Length <= 20;

        public static bool IsUsernameAvailable(string username, Market marketplace) 
            => !marketplace.Users.Select(x => x.Username).Contains(username);

        public static bool ValidateEmail(string email) 
            => Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

        public static bool IsEmailAvailable(string email, Market marketplace) 
            => !marketplace.Users.Select(x => x.Email).Contains(email);
    }
}
