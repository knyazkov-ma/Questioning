namespace Questioning.Helpers
{
    public static class IntExtensions
    {
        public static string GetYearUnitName(this int year)
        {
            int mod = year % 10;
            if (mod == 0 || mod >= 5 && mod <= 9 || year >= 11 && year <= 20)
                return "лет";
            if (mod >= 2 && mod <= 4)
                return "года";
            return "год";
        }
    }
}
