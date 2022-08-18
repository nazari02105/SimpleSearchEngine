using System;
using System.Collections.Generic;
using System.Linq;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class InvertedIndex : IInvertedIndex
    {
        private readonly IDatabaseMap<string, string> _context;
        private readonly ITokenizer _tokenizer;

        public InvertedIndex(IDatabaseMap<string, string> context, ITokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        public IInvertedIndex AddDocuments(Dictionary<string, string> allDocuments)
        {
            _context.Create();
            allDocuments.ToList().ForEach(pair =>
            {
                Console.WriteLine($"adding {pair.Key}");
                BuiltinAddDocument(pair.Key, pair.Value, false);
            });

            _context.Save();
            return this;
        }

        public void AddDocument(string name, string content)
        {
            BuiltinAddDocument(name, content, true);
        }

        private void BuiltinAddDocument(string name, string content, bool saveContext)
        {
            if (name == null || content == null)
                return;
            if (saveContext)
                _context.Create();
            foreach (var word in StringUtils.ProcessRawTokens(_tokenizer.Tokenize(content)).ToList())
            {
                _context.Add(word, name);
            }

            if (saveContext)
                _context.Save();
        }

        public SortedSet<string> Query(IUserInput input)
        {
            _context.Create();
            SortedSet<string> result = null;
            input.GetAndInputs().ToList().ForEach(st => result = AndWordWithResult(st, result));
            input.GetOrInputs().ToList().ForEach(st => result = AddWordToResult(st, result));
            input.GetRemoveInputs().ToList().ForEach(st => result = RemoveWordFromResult(st, result));
            if (result != null) return result;
            else return new SortedSet<string>();
        }

        public void ClearIndex()
        {
            _context.Delete();
            _context.Create();
        }

        public List<string> GetHints(string hint)
        {
            return _context.GetHints(hint);
        }

        private SortedSet<string> RemoveWordFromResult(string word, SortedSet<string> result)
        {
            List<string> wordList = GetDocsContainWord(word);
            if (result == null) return new SortedSet<string>();
            result.ExceptWith(wordList);
            return result;
        }

        private SortedSet<string> AddWordToResult(string word, SortedSet<string> result)
        {
            List<string> wordList = GetDocsContainWord(word);
            if (result == null) return new SortedSet<string>(wordList);
            result.UnionWith(wordList);
            return result;
        }

        private SortedSet<string> AndWordWithResult(string word, SortedSet<string> result)
        {
            List<string> wordList = GetDocsContainWord(word);
            if (result == null) return new SortedSet<string>(wordList);
            result.IntersectWith(wordList);
            return result;
        }

        public List<string> GetDocsContainWord(string word)
        {
            return _context.Get(word);
        }
    }
}