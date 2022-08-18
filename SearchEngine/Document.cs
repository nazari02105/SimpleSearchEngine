using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SearchEngine
{
    public class Document
    {
        [Key] public string Name { get; init; }

        public List<Word> Words { get; set; } = new();
    }
}