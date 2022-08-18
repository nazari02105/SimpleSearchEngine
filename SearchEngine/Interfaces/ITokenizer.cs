using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface ITokenizer
    {
        SortedSet<string> Tokenize(string wholeText);
    }
}