using System;

namespace Questioning.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetAge(this DateTime dr, DateTime current)
        {
            int age = current.Year - dr.Year;
            if (current < dr.AddYears(age)) age--;
            return age;
        }
    }
}
