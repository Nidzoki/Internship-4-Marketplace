using Marketplace.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Marketplace.Domain.Validation
{
    public static class Validation
    {
        public static bool ValidateUsername(string username)
            => Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$")
            && username.Length >= 3
            && username.Length <= 20;

        public static bool IsUsernameAvailable(string username, Data.Program.Context marketplace)
            => !marketplace.Users.Select(x => x.Username).Contains(username);

        public static bool ValidateEmail(string email)
            => Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

        public static bool IsEmailAvailable(string email, Data.Program.Context marketplace)
            => !marketplace.Users.Select(x => x.Email).Contains(email);

        public static bool ValidatePromoCode(PromoCode promoCode) => promoCode.ExpiryDate <= DateTime.Now;

        public static bool CheckIfInInterval(Interval interval, DateTime moment) => moment < interval.End && moment > interval.Start;
    }
}
