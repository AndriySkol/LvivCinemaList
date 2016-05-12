using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieServices.Services
{
    public class DummyTimeHelper : ITimeHelper
    {
        public DateTime GetStartOfWeek()
        {
            return DateTime.Parse("21.03.08");
        }

        public DateTime GetEndOfWeek()
        {
            return DateTime.Parse("27.03.08");
        }
    }
}