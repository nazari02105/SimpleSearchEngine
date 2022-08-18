using System.Collections.Generic;
using System.Text;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class Tokenizer : ITokenizer
    {
        public SortedSet<string> Tokenize(string wholeText)
        {
            SortedSet<string> tokens = new SortedSet<string>();
            for (int i = 0; i < wholeText.Length; ++i)
            {
                if (!char.IsLetter(wholeText[i]))
                {
                    continue;
                }

                StringBuilder token = new StringBuilder();
                while (i < wholeText.Length && char.IsLetterOrDigit(wholeText[i]))
                {
                    token.Append(char.ToLower(wholeText[i]));
                    i++;
                }

                tokens.Add(token.ToString());
            }

            return tokens;
        }
    }
}