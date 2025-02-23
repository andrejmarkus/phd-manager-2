namespace PhDManager.Services
{
    public class SchoolYearService
    {
        public string CurrentSchoolYear { 
            get
            {
                var today = DateTime.Now;
                int startYear = today.Month >= 9 ? today.Year : today.Year - 1;
                int endYear = startYear + 1;
                return $"{startYear}/{endYear}";
            }
        }

        public bool IsInCurrentSchoolYear(DateTime date)
        {
            string currentYear = CurrentSchoolYear;
            int startYear = int.Parse(currentYear.Split('/')[0]);
            var schoolStart = new DateTime(startYear, 9, 1);
            var schoolEnd = new DateTime(startYear + 1, 8, 31);
            return date >= schoolStart && date <= schoolEnd;
        }
    }
}
