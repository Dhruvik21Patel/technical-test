using ProductManagement.Common.Constants;

namespace ProductManagement.Common.Extensions
{
    public static class DateExtensions
    {
        public static DateTime ToDateTimeWithAESTZone(this string dateString)
        {
            if (!DateTime.TryParseExact(dateString, SystemConstants.DefaultDateFormat, null, System.Globalization.DateTimeStyles.None,
                out DateTime parsedDateTime))
            {
                throw new Exception($"Invalid date format. Expected format: {SystemConstants.DefaultDateFormat}");
            }

            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(SystemConstants.DefaultTimezoneID);

            DateTime unspecifiedKindDateTime = DateTime.SpecifyKind(parsedDateTime, DateTimeKind.Unspecified);

            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(unspecifiedKindDateTime, timeZone);

            return utcDateTime;
        }

        public static DateTime ConvertToDate(this string date)
        {
            return DateTime.Parse(date);
        }

        public static DateTime ToDateTime(this string date, string time)
        {
            if (string.IsNullOrEmpty(time))
            {
                throw new Exception("Please add time");
            }

            TimeSpan timeSpan = new TimeSpan(Convert.ToInt32(time.Split(":")[0]), Convert.ToInt32(time.Split(":")[1]), 0);
            DateTime dateTime = date.ConvertToDate();

            return dateTime.Add(timeSpan);
        }
    }
}