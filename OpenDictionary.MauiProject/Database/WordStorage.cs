using Microsoft.EntityFrameworkCore;

using OpenDictionary.Models;

namespace OpenDictionary.Databases;

internal sealed class WordStorage : AppDatabaseStorage<Word>
{
    public WordStorage(IDatabasePath path) : base(path) { }

    protected override DbSet<Word> GetContext(DatabaseContextBase context) => Cast(context).Words;
}