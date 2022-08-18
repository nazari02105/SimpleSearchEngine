using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class SearchController : ControllerBase
    {
        private readonly IInvertedIndexService _invertedIndex;

        public SearchController(IInvertedIndexService invertedIndex)
        {
            _invertedIndex = invertedIndex;
        }

        [HttpGet]
        public List<string> Query(string query)
        {
            return _invertedIndex.Query(query);
        }

        [HttpPost]
        public IActionResult PostDocuments([FromBody] Dictionary<string, string> fileContents)
        {
            _invertedIndex.AddDocuments(fileContents);
            return Ok(new
            {
                fileContents
            });
        }

        [HttpDelete]
        public IActionResult ClearIndex()
        {
            _invertedIndex.ClearIndex();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetHints(string hint)
        {
            return Ok(_invertedIndex.GetHints(hint));
        }
    }
}