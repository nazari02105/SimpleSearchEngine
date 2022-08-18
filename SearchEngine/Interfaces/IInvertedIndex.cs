using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IInvertedIndex
    {
        IInvertedIndex AddDocuments(Dictionary<string, string> allDocuments);
        void AddDocument(string word, string content);
        SortedSet<string> Query(IUserInput input);
        void ClearIndex();
        List<string> GetHints(string hint);
    }
}