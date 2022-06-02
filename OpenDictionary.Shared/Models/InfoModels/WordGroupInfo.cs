#nullable enable

using System;

namespace OpenDictionary.Models
{
    public class WordGroupInfo
    {
        public Guid? Id { get; set; }

        public DateTime? Date { get; set; }

        public string? Name { get; set; }

        public int? Count { get; set; }
    }
}
