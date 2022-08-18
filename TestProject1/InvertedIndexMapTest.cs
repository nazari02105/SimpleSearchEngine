using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SearchEngine;
using Xunit;

namespace TestProject1
{
    public class InvertedIndexMaptTest
    {
        private InvertedIndexMap GetContext()
        {
            var builder = new DbContextOptionsBuilder<InvertedIndexContext>();
            builder.UseInMemoryDatabase("InvertedIndexContext");
            var map = new InvertedIndexMap(new InvertedIndexContext(builder.Options));
            map.Context.Database.EnsureDeleted();
            return map;
        }

        [Fact]
        public void TestCreateDatabase()
        {
            var context = GetContext();
            Assert.True(context.Create());
        }

        [Fact]
        public void TestDeleteDatabase()
        {
            var context = GetContext();
            context.Create();
            Assert.True(context.Delete());
        }

        [Fact]
        public void TestDatabaseNotExist()
        {
            var context = GetContext();
            Assert.False(context.Delete());
        }

        [Fact]
        public void TestAddWordDocument()
        {
            var context = GetContext();
            context.Create();
            Word myWord = new Word() { String = "word" };
            var word = myWord.String;
            Document myDocument = new Document() { Name = "document" };
            var document = myDocument.Name;
            //
            myDocument.Words = new List<Word>();
            var words = myDocument.Words;
            words.Add(myWord);
            //
            Assert.Empty(context.Get(word));
            context.Add(word, document);
            context.Save();
            var expected = new List<string> { document };
            Assert.Equal(expected, context.Get(word));
        }

        [Fact]
        public void TestDuplicateAdd()
        {
            var context = GetContext();
            context.Create();
            var word = "word";
            var document = "document";
            Assert.Empty(context.Get(word));
            context.Add(word, document);
            context.Add(word, document);
            context.Save();
            var expected = new List<string> { document };
            Assert.Equal(expected, context.Get(word));
        }

        [Fact]
        public void TestAddManyDocument()
        {
            var context = GetContext();
            context.Create();
            var word = "word";
            var document1 = "document1";
            var document2 = "document2";
            Assert.Empty(context.Get(word));
            context.Add(word, document1);
            context.Add(word, document2);
            context.Save();
            var expected = new List<string> { document1, document2 };
            Assert.Equal(expected, context.Get(word));
        }

        [Fact]
        public void TestHints()
        {
            var context = GetContext();
            context.Create();
            context.Add("hello","1");
            context.Add("hell","2");
            context.Save();
            var expected = new List<string>
            {
                "hello", "hell"
            };
            Assert.Equal(expected,context.GetHints("he"));
        }
    }
}