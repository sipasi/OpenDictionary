
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;

namespace OpenDictionary.AppDatabase
{
    internal abstract class DatabaseStorage<T> : IStorage<T>
        where T : class, IEntity
    {
        private readonly string path;

        public DatabaseStorage(IDatabasePath path)
        {
            this.path = path.Path;
        }

        public async ValueTask<T> GetAsync(Guid id)
        {
            try
            {
                Open(out DatabaseContext database, out DbSet<T> context);

                T entity = await context.FirstOrDefaultAsync(item => item.Id == id);

                return entity;
            }
            catch (Exception e)
            {
                WritenException(e);
            }

            return null;
        }

        public async ValueTask<bool> AddAsync(T item)
        {
            try
            {
                Open(out DatabaseContext database, out DbSet<T> context);

                var entity = await context.AddAsync(item);

                await database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                WritenException(e);

                return false;
            }

            return true;
        }
        public async ValueTask<bool> AddRangeAsync(IEnumerable<T> items)
        {
            try
            {
                Open(out DatabaseContext database, out DbSet<T> context);

                await context.AddRangeAsync(items);

                await database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                WritenException(e);

                return false;
            }

            return true;
        }
        public async ValueTask<bool> DeleteAsync(T item)
        {
            try
            {
                Open(out DatabaseContext database, out DbSet<T> context);

                var entity = context.Remove(item);

                await database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                WritenException(e);

                return false;
            }

            return true;
        }

        public async ValueTask<bool> DeleteRangeAsync(IEnumerable<T> items)
        {
            try
            {
                Open(out DatabaseContext database, out DbSet<T> context);

                context.RemoveRange(items);

                await database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                WritenException(e);

                return false;
            }

            return true;
        }

        public async ValueTask<bool> UpdateAsync(T item)
        {
            try
            {
                Open(out DatabaseContext database, out DbSet<T> context);

                var entity = context.Update(item);

                await database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                WritenException(e);

                return false;
            }

            return true;
        }

        public bool Any()
        {
            Open(out DatabaseContext _, out DbSet<T> context);

            return context.Any();
        }

        public IQueryable<T> Query()
        {
            Open(out DatabaseContext _, out DbSet<T> context);

            IQueryable<T> query = context.AsQueryable();

            return query;
        }

        private void Open(out DatabaseContext database, out DbSet<T> context)
        {
            database = Open();
            context = GetContext(database);
        }

        protected abstract DbSet<T> GetContext(DatabaseContext context);

        private DatabaseContext Open()
        {
            DatabaseContext context = new DatabaseContext(path);

            return context;
        }

        private static void WritenException(Exception exception)
        {
            string message = $"Database exception.\nException: {exception.Message}.\nInner: {exception.InnerException?.Message ?? "Empty"}";

            Debug.WriteLine(message);
        }
    }
}