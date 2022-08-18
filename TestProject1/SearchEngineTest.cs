using System;
using System.Collections.Generic;
using NSubstitute;
using SearchEngine.Interfaces;
using Xunit;

namespace TestProject1
{
    public class SearchEngineTest
    {
        [Fact]
        public void TestSearchEngine()
        {
            var reader = new CustomInput("hello\nthis\nexit");
            var writer = new CustomOutput();
            var invertedIndex = GetInvertedIndex();
            var searchEngine =
                new SearchEngine.SearchEngine(reader, writer, invertedIndex);
            searchEngine.Run();
            const string expectedOutput = "1\n2\nno doc found\n";
            Assert.Equal(expectedOutput, writer.AllOutput);
        }

        [Fact]
        public void TestNoDocFound()
        {
            var reader = new CustomInput("cat\nnothing\nexit");
            var writer = new CustomOutput();
            var invertedIndex = GetInvertedIndex();
            var searchEngine =
                new SearchEngine.SearchEngine(reader, writer, invertedIndex);
            searchEngine.Run();
            const string expectedOutput = "no doc found\nno doc found\n";
            Assert.Equal(expectedOutput, writer.AllOutput);
        }

        [Fact]
        public void TestJustExit()
        {
            var reader = new CustomInput("exit");
            var writer = new CustomOutput();
            var invertedIndex = GetInvertedIndex();
            var searchEngine =
                new SearchEngine.SearchEngine(reader, writer, invertedIndex);
            searchEngine.Run();
            const string expectedOutput = "";
            Assert.Equal(expectedOutput, writer.AllOutput);
        }

        [Fact]
        public void TestWithNoInput()
        {
            var reader = new CustomInput("");
            var writer = new CustomOutput();
            var invertedIndex = GetInvertedIndex();
            var searchEngine =
                new SearchEngine.SearchEngine(reader, writer, invertedIndex);
            searchEngine.Run();
            const string expectedOutput = "";
            Assert.Equal(expectedOutput, writer.AllOutput);
        }

        private static IInvertedIndex GetInvertedIndex()
        {
            var invertedIndex = Substitute.For<IInvertedIndex>();
            invertedIndex.AddDocuments(Arg.Any<Dictionary<string, string>>()).Returns(invertedIndex);
            invertedIndex.Query(Arg.Any<IUserInput>()).Returns(x =>
            {
                var list = new SortedSet<string>();
                if (((IUserInput)x[0]).GetAndInputs().Contains("hello"))
                {
                    list.Add("1");
                    list.Add("2");
                }

                return list;
            });
            return invertedIndex;
        }
    }

    internal class CustomInput : IInputReader
    {
        private readonly string[] _inputs;
        private int _counter;

        public CustomInput(string input)
        {
            _inputs = string.IsNullOrEmpty(input) ? Array.Empty<string>() : input.Split('\n');
            _counter = 0;
        }

        public string Read()
        {
            if (_counter < _inputs.Length)
                return _inputs[_counter++];
            return null;
        }
    }

    internal class CustomOutput : IOutputWriter
    {
        public string AllOutput { private set; get; } = "";

        public void Write(string output)
        {
            AllOutput += output + "\n";
        }
    }
}