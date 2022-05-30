
using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Models;

namespace OpenDictionary.Collections.Storages.Extensions;

public static class QueryableExtensions
{
    public static Task<T> GetById<T>(this IQueryable<T> query, Guid id)
        where T : IEntity<Guid>
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

    public static IQueryable<WordGroup> IncludeAll(this IQueryable<WordGroup> query)
    {
        return query.Include(entity => entity.Words);
    }
    public static IQueryable<WordMetadata> IncludeAll(this IQueryable<WordMetadata> query)
    {
        return query.Include(entity => entity.Phonetics)
                .Include(entity => entity.Meanings)
                    .ThenInclude(entity => entity.Definitions)
                .Include(entity => entity.Meanings)
                    .ThenInclude(entity => entity.Synonyms)
                .Include(entity => entity.Meanings)
                    .ThenInclude(entity => entity.Antonyms)
                .Include(entity => entity.Meanings)
                    .ThenInclude(entity => entity.Definitions);
    }

    public static Task<WordMetadata> GetByWord(this IQueryable<WordMetadata> query, string word)
    {
        return query.FirstOrDefaultAsync(entity => EF.Functions.Like(entity.Value, word));
    }
}