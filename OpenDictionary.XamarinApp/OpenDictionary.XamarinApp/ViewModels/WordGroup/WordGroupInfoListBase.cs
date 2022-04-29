using System.Linq;
using System.Threading.Tasks;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;

namespace OpenDictionary.ViewModels
{
    public abstract class WordGroupInfoListBase
    {
        public SelectableCollection<WordGroup, WordGroupInfo> Groups { get; }

        public WordGroupInfoListBase(IStorage<WordGroup> wordGroupStorage)
        {
            Groups = new SelectableCollection<WordGroup, WordGroupInfo>(wordGroupStorage, Query, OnTapped);
        }

        protected virtual IQueryable<WordGroupInfo> Query(IQueryable<WordGroup> query)
        {
            return query.OrderByDateDescending().Select(group => new WordGroupInfo
            {
                Id = group.Id,
                Name = group.Name,
                Count = group.Words.Count(),
                Date = group.Date
            });
        }

        protected abstract Task OnTapped(WordGroupInfo item);
    }
}