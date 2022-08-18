using System.Collections.Generic;

namespace WebMVC.Services
{
    public interface IInvertedIndexService
    {
        List<string> Query(string query);
        void AddDocument(string name, string content);
        void AddDocuments(Dictionary<string, string> fileContents);
        void ClearIndex();
        List<string> GetHints(string word);
    }
}