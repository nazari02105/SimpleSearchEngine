using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class InvertedIndexMap : IDatabaseMap<string, string>

    {
        public readonly InvertedIndexContext Context;

        public InvertedIndexMap(InvertedIndexContext context)
        {
            Context = context;
        }


        public List<string> Get(string key)
        {
            var word = Context.Words.Where(w => w.String == key).Include(w => w.Documents).FirstOrDefault();
            if (word == null)
            {
                return new List<string>();
            }

            return word.Documents.Select(document => document.Name).ToList();
        }

        public void Add(string key, string value)
        {
            var word = Context.Words.Find(key);
            if (word == null)
            {
                word = new Word() { String = key };
                Context.Words.Add(word);
            }

            var document = Context.Documents.Find(value);
            if (document == null)
            {
                document = new Document() { Name = value };
                Context.Documents.Add(document);
            }

            if (word.Documents.Contains(document))
            {
                return;
            }

            word.Documents.Add(document);
        }

        public bool Delete()
        {
            return Context.Database.EnsureDeleted();
        }

        public bool Create()
        {
            return Context.Database.EnsureCreated();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public List<string> GetHints(string hint)
        {
            return Context.Words.Where(word => word.String.StartsWith(hint)).Select(word => word.String).ToList();
        }
    }
}