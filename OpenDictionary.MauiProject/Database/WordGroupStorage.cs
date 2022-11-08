
using Microsoft.EntityFrameworkCore;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

internal sealed class WordGroupStorage : AppDatabaseStorage<WordGroup>
{
    public WordGroupStorage(IDatabasePath path) : base(path) { }

    protected override DbSet<WordGroup> GetContext(DatabaseContextBase context) => Cast(context).WordGroups;
}