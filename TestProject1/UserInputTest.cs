using System.Collections.Generic;
using SearchEngine;
using Xunit;

namespace TestProject1
{
    public class UserInputTest
    {
        [Fact]
        public void SimpleInputTest()
        {
            const string input = "hello this +fine -cats";
            var userInput = new UserInput(input);

            var expectedAndInput = new SortedSet<string> { "hello" };
            var expectedOrInput = new SortedSet<string> { "fine" };
            var expectedRemoveInput = new SortedSet<string> { "cat" };

            Assert.Equal(expectedAndInput, userInput.GetAndInputs());
            Assert.Equal(expectedOrInput, userInput.GetOrInputs());
            Assert.Equal(expectedRemoveInput, userInput.GetRemoveInputs());
        }
    }
}