
using Microsoft.EntityFrameworkCore;

using OpenDictionary.Models;

namespace OpenDictionary.AppDatabase;

internal sealed class WordStorage : DatabaseStorage<Word>
{
    public WordStorage(IDatabasePath path)
        : base(path) { }

    protected override DbSet<Word> GetContext(DatabaseContext context) => context.Words;
}
