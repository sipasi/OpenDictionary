
using Microsoft.EntityFrameworkCore;

using ProjectWord.Models;

namespace ProjectWord.AppDatabase
{
    internal sealed class WordGroupStorage : DatabaseStorage<WordGroup>
    {
        protected override DbSet<WordGroup> GetContext(DatabaseContext context) => context.WordGroups;
    }
}
