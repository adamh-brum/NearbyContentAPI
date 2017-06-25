namespace API.Helpers
{
    using System;

    public static class DateTimeExtensions
    {
        public static DateTime GetStartOfWeek(this DateTime dateTime)
        {
            DateTime startOfWeek = dateTime;
            while(startOfWeek.DayOfWeek != DayOfWeek.Monday){
                startOfWeek = startOfWeek.AddDays(-1);
            }

            return startOfWeek.Date;
        }
    }
}