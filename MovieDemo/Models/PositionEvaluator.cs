using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MovieDemo.Models
{
    public class PositionEvaluator
    {
        public static string GetPercent(DateTime time)
        {
            double offcet = time.TimeOfDay.TotalMinutes - 600.0;
            offcet = offcet >= 0 ? offcet : time.TimeOfDay.TotalMinutes + 14 * 60;
            double percent = offcet / (16 * 60) * 100;
            return percent.ToString(CultureInfo.InvariantCulture);
        }

        public static string GetPercent(int i)
        {
            return GetPercent(DateTime.Today + TimeSpan.FromHours(i));
        }
    }
}