using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenDictionary.Collections.Storages;

public interface IStorage<T>
{
    ValueTask<bool> AddAsync(T item);
    ValueTask<bool> AddRangeAsync(IEnumerable<T> items);
    ValueTask<bool> DeleteAsync(T item);
    ValueTask<bool> DeleteRangeAsync(IEnumerable<T> items);

    IQueryable<T> Query();

    bool Any();

    ValueTask<bool> UpdateAsync(T item);
}