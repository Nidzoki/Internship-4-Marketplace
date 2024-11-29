using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.ReviewGenerator
{
    public static class ReviewGenerator
    {
        public static List<int> GetReviews()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            var numberOfReviews = rnd.Next(1, 50);
            var reviews = new List<int>();
            
            for (var i = 0; i < numberOfReviews; i++)
                reviews.Add(rnd.Next(1, 11));
                Thread.Sleep(1);
            
            return reviews;
        }
    }
}
