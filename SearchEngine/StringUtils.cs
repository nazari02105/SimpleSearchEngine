using System.Collections.Generic;
using System.Linq;
using Porter2Stemmer;

namespace SearchEngine
{
    public static class StringUtils
    {
        private const string StopWordList =
            "a-about-actually-almost-also-although-always-am-an-and-any-are-as-at-be-became-become-but-" +
            "by-can-could-did-do-does-each-either-else-for-from-had-has-have-hence-how-i-if-in-is-it-its-" +
            "just-may-maybe-me-might-mine-must-my-neither-nor-not-of-oh-ok-" +
            "when-where-whereas-wherever-whenever-whether-which-while-who-whom-whoever-whose-why-" +
            "will-with-within-without-would-yes-yet-you-your-this-that-to";

        public static void RemoveStopWords(SortedSet<string> words)
        {
            var mustRemove = StopWordList.Split('-').ToList();
            mustRemove.ForEach(word => words.Remove(word));
        }

        public static SortedSet<string> Stem(SortedSet<string> tokens)
        {
            var stemTokens = new SortedSet<string>();
            var stemmer = new EnglishPorter2Stemmer();
            tokens.ToList().ForEach(token => stemTokens.Add(stemmer.Stem(token).Value));
            return stemTokens;
        }

        public static SortedSet<string> ProcessRawTokens(SortedSet<string> rawTokens)
        {
            RemoveStopWords(rawTokens);
            rawTokens = Stem(rawTokens);
            return rawTokens;
        }
    }
}