using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredicateWaterConsumption
{
    public static class DateTimeHelper
    {
        public static int CountWorkingDays(DateTime startDate, DateTime endDate)
        {
            int workingDaysCount = 0;

            // Iterowanie przez każdy dzień w przedziale czasowym
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Sprawdzenie, czy dzień nie jest sobotą ani niedzielą
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDaysCount++;
                }
            }

            return workingDaysCount;
        }
    }
}
