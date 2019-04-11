using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HomeApp.API.Helpers
{
    public static class Extensions
    {
        public static int CalculateDaysOnMarket(this DateTime dateAdded)
        {
            var daysOnMarket = DateTime.Now.Subtract(dateAdded).Days;
            return daysOnMarket == 0 ? 1 : daysOnMarket;
        }

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int pageSize, int totalPages, int totalItems)
        {
            var paginationHeader = new PaginationHeader(currentPage, pageSize, totalPages, totalItems);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();  // we dont want title case
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}