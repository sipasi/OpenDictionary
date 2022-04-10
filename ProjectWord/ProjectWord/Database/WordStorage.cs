
using Microsoft.EntityFrameworkCore;

using ProjectWord.Models;

namespace ProjectWord.AppDatabase
{
    internal sealed class WordStorage : DatabaseStorage<Word>
    {
        protected override DbSet<Word> GetContext(DatabaseContext context) => context.Words;
    }
}
