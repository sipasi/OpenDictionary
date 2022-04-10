using System;
using System.Collections.Generic;

namespace ProjectWord.Models
{
    public class WordGroup : IEntity
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string Name { get; set; }

        public ICollection<Word> Words { get; set; }
    }
}
