namespace LazySloth.Utilities
{
    using System;

    public static class DateTimeExtensions
    {
        public static DateTime Replace(this DateTime dateTime, 
            int? year = null,
            int? month = null,
            int? day = null,
            int? hour = null,
            int? minute = null,
            int? second = null,
            int? millisecond = null
        )
        {
            return new DateTime(
                year ?? dateTime.Year,
                month ?? dateTime.Month,
                day ?? dateTime.Day,
                hour ?? dateTime.Hour,
                minute ?? dateTime.Minute,
                second ?? dateTime.Second,
                millisecond ?? dateTime.Millisecond
            );
        }
    }
}