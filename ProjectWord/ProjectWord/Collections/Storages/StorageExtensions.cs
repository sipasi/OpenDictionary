
using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProjectWord.Models;

namespace ProjectWord.Collections.Storages.Extensions
{
    public static class QueryableExtensions
    {
        public static Task<T> GetById<T>(this IQueryable<T> query, Guid id)
            where T : IEntity
        {
            Task<T> task = query.FirstOrDefaultAsync(x => x.Id == id);

            return task;
        }
        public static IQueryable<WordGroup> OrderByDate(this IQueryable<WordGroup> query)
        {
            return query.OrderBy(x => x.Date);
        }
        public static IQueryable<WordGroup> OrderByDateDescending(this IQueryable<WordGroup> query)
        {
            return query.OrderByDescending(x => x.Date);
        }
    }
}
