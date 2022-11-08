using Microsoft.EntityFrameworkCore;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

internal sealed class WordMetadataStorage : AppDatabaseStorage<WordMetadata>
{
    public WordMetadataStorage(IDatabasePath path) : base(path) { }

    protected override DbSet<WordMetadata> GetContext(DatabaseContextBase context) => Cast(context).WordMetadatas;
}
