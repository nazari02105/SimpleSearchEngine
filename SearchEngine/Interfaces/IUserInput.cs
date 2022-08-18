using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IUserInput
    {
        SortedSet<string> GetAndInputs();
        SortedSet<string> GetOrInputs();
        SortedSet<string> GetRemoveInputs();
    }
}