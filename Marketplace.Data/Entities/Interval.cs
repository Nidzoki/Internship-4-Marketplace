using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Entities
{
    public class Interval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Interval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

    }
}
