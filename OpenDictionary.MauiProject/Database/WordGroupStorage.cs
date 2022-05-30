
using Microsoft.EntityFrameworkCore;

using OpenDictionary.Models;

namespace OpenDictionary.AppDatabase;

internal sealed class WordGroupStorage : DatabaseStorage<WordGroup>
{
    public WordGroupStorage(IDatabasePath path)
        : base(path) { }

    protected override DbSet<WordGroup> GetContext(DatabaseContext context) => context.WordGroups;
}