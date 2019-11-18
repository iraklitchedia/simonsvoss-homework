using Microsoft.AspNetCore.Mvc;

namespace Simonsvoss_Homework.Controllers
{
  [Route("api/[controller]")]
  public class SearchController : Controller
  {
    private ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
      _searchService = searchService;
    }

    [HttpGet("[action]")]
    public IActionResult SearchText(string text)
    {
      var result = _searchService.Search(text);
      return Ok(result);
    }
  }
}
