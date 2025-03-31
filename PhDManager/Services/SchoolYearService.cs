namespace PhDManager.Services
{
    public class SchoolYearService
    {
        public static string CurrentSchoolYear
        {
            get
            {
                return GetSchoolYear(DateTime.Now);
            }
        }

        public static string GetSchoolYear(DateTime dateTime)
        {
            int startYear = dateTime.Month >= 9 ? dateTime.Year : dateTime.Year - 1;
            int endYear = startYear + 1;
            return $"{startYear}/{endYear}";
        }

        public static bool IsInCurrentSchoolYear(DateTime date)
        {
            string currentYear = CurrentSchoolYear;
            int startYear = int.Parse(currentYear.Split('/')[0]);
            var schoolStart = new DateTime(startYear, 9, 1, 0, 0, 0, DateTimeKind.Utc);
            var schoolEnd = new DateTime(startYear + 1, 8, 31, 23, 59, 59, DateTimeKind.Utc);
            return date >= schoolStart && date <= schoolEnd;
        }
    }
}
