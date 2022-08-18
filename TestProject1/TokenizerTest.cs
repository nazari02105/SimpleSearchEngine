using System.Collections.Generic;
using SearchEngine;
using Xunit;

namespace TestProject1
{
    public class TokenizerTest
    {
        [Fact]
        public void EmptyTest()
        {
            Tokenizer tokenizer = new Tokenizer();
            Assert.Empty(tokenizer.Tokenize("123"));
        }

        [Fact]
        public void SimpleWordsTest()
        {
            Tokenizer tokenizer = new Tokenizer();
            string wholeText = "test this text";
            var expected = new SortedSet<string> { "test", "this", "text" };
            Assert.Equal(expected, tokenizer.Tokenize(wholeText));
        }
    }
}