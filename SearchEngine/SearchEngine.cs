using System.Collections.Generic;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class SearchEngine : ISearchEngine
    {
        private readonly IInputReader _reader;
        private readonly IOutputWriter _writer;
        private readonly IInvertedIndex _invertedIndex;

        public SearchEngine(IInputReader reader, IOutputWriter writer, IInvertedIndex invertedIndex)
        {
            _reader = reader;
            _writer = writer;
            _invertedIndex = invertedIndex;
        }

        public void Run()
        {
            while (true)
            {
                string input = _reader.Read();
                if (IsFinished(input))
                {
                    break;
                }

                UserInput userInput = new UserInput(input);
                SortedSet<string> containingDocs = _invertedIndex.Query(userInput);
                if (containingDocs.Count == 0)
                {
                    _writer.Write("no doc found");
                }

                foreach (var docName in containingDocs)
                {
                    _writer.Write(docName);
                }
            }
        }

        private static bool IsFinished(string input)
        {
            return input is null or "exit";
        }
    }
}