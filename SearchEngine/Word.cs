using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SearchEngine
{
    public class Word
    {
        [Key] public string String { init; get; }

        public List<Document> Documents { get; } = new();
    }
}