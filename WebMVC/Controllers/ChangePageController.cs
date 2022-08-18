using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Route("[controller]/[action]")]
    public class ChangePageController : Controller
    {
        private readonly IInvertedIndexService _invertedIndex;

        public ChangePageController(IInvertedIndexService invertedIndex)
        {
            _invertedIndex = invertedIndex;
        }
        
        [HttpGet]
        public IActionResult Query(string query)
        {
            ViewBag.Query = _invertedIndex.Query(query);
            return View();
        }
    }
}