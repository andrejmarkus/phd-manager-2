namespace PhDManager.Services
{
    public class SchoolYearService
    {
        private static SchoolYearService? _instance;

        public static SchoolYearService Instance => _instance ??= new SchoolYearService();
        public string CurrentSchoolYear => GetSchoolYear(DateTime.Now);

        private SchoolYearService() {}

        public string GetSchoolYear(DateTime dateTime)
        {
            var startYear = dateTime.Month >= 9 ? dateTime.Year : dateTime.Year - 1;
            var endYear = startYear + 1;
            return $"{startYear}/{endYear}";
        }

        public bool IsInCurrentSchoolYear(DateTime date)
        {
            var currentYear = CurrentSchoolYear;
            var startYear = int.Parse(currentYear.Split('/')[0]);
            var schoolStart = new DateTime(startYear, 9, 1, 0, 0, 0, DateTimeKind.Utc);
            var schoolEnd = new DateTime(startYear + 1, 8, 31, 23, 59, 59, DateTimeKind.Utc);
            return date >= schoolStart && date <= schoolEnd;
        }
    }
}
