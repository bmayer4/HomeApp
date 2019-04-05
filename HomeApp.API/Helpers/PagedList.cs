using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HomeApp.API.Helpers
{
    public class PagedList<T>: List<T> 
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public PagedList(List<T> items, int currentPage, int pageSize, int totalItems)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            TotalItems = totalItems;
            AddRange(items);
        }

        public static async Task<PagedList<T>> CreatePagedListAsync(IQueryable<T> source, int currentPage, int pageSize)
        {
            var totalItems = await source.CountAsync();
            var items = await source.Skip(pageSize * (currentPage - 1)).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, currentPage, pageSize, totalItems);
        }
    }

}