using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SearchEngine;

namespace WebMVC.Services
{
    public class InvertedIndexService : IInvertedIndexService
    {
        private InvertedIndex _invertedIndex;

        public InvertedIndexService()
        {
            var builder = new DbContextOptionsBuilder<InvertedIndexContext>();
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            if (connectionString != null)
                builder.UseSqlServer(connectionString);
            else
                builder.UseInMemoryDatabase("InvertedIndex");
            var context = new InvertedIndexContext(builder.Options);
            var map = new InvertedIndexMap(context);
            _invertedIndex = new InvertedIndex(map,new Tokenizer());
        }
        public List<string> Query(string query)
        {
            var userInput = new UserInput(query);
            return _invertedIndex.Query(userInput).ToList();
        }

        public void AddDocument(string name, string content)
        {
            _invertedIndex.AddDocument(name,content);
        }

        public void AddDocuments(Dictionary<string, string> fileContents)
        {
            _invertedIndex.AddDocuments(fileContents);
        }

        public void ClearIndex()
        {
            _invertedIndex.ClearIndex();
        }

        public List<string> GetHints(string hint)
        {
            return _invertedIndex.GetHints(hint);
        }
    }
}