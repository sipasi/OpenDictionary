
using Microsoft.EntityFrameworkCore;

using OpenDictionary.Models;

namespace OpenDictionary.AppDatabase;

internal sealed class WordMetadataStorage : DatabaseStorage<WordMetadata>
{
    public WordMetadataStorage(IDatabasePath path)
        : base(path) { }

    protected override DbSet<WordMetadata> GetContext(DatabaseContext context) => context.WordMetadatas;
}
