using Microsoft.AspNetCore.Mvc;

namespace Simonsvoss_Homework.Controllers
{
  [Route("api/[controller]")]
  public class SearchController : Controller
  {
    private ISearchService _searchService;

    SearchController(ISearchService searchService)
    {
      _searchService = searchService;
    }

    [HttpGet("[action]")]
    public string SearchText()
    {
      return "Hello world";
    }
  }
}
