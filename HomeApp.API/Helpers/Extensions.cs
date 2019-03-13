using System;

namespace HomeApp.API.Helpers
{
    public static class Extensions
    {
        public static int CalculateDaysOnMarket(this DateTime dateAdded)
        {
            var daysOnMarket = DateTime.Now.Subtract(dateAdded).Days;
            return daysOnMarket == 0 ? 1 : daysOnMarket;
        }
    }
}