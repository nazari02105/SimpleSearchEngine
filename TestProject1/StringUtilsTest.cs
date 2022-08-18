using System.Collections.Generic;
using SearchEngine;
using Xunit;

namespace TestProject1
{
    public class StringUtilsTest
    {
        [Fact]
        public void TestRemoveStopWords()
        {
            var words = new SortedSet<string> { "hello", "this", "my", "rain" };
            StringUtils.RemoveStopWords(words);
            var expected = new SortedSet<string> { "hello", "rain" };
            Assert.Equal(expected, words);
        }

        [Fact]
        public void TestStem()
        {
            var words = new SortedSet<string> { "cats", "helpful", "hello", "nested", "running" };
            var result = StringUtils.Stem(words);
            var expected = new SortedSet<string> { "cat", "help", "hello", "nest", "run" };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestProcessWords()
        {
            var toBeProcess = new SortedSet<string> { "cats", "this", "hello", "helpful" };
            var processedWords = StringUtils.ProcessRawTokens(toBeProcess);
            var expected = new SortedSet<string> { "cat", "hello", "help" };
            Assert.Equal(expected, processedWords);
        }
    }
}