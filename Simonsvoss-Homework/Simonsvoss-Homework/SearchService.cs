using System.IO;
using Newtonsoft.Json;
using Simonsvoss_Homework.Models;

namespace Simonsvoss_Homework
{
  public class SearchService : ISearchService
  {
    private readonly string _filepath = @".\Data\sv_lsm_data.json";
    private Data _data;

    public SearchService()
    {
      // We do it here to avoid reading file every time search is requested
      GetDataFromJsonFile();
    }

    public string Search(string text)
    {
      return JsonConvert.SerializeObject(_data);
    }

    /// <summary>
    /// Gets data from "sv_lsm_data.json"
    /// </summary>
    /// <returns></returns>
    private void GetDataFromJsonFile()
    {
      // Read file and deserialize from json
      var content = File.ReadAllText(_filepath);
      _data = JsonConvert.DeserializeObject<Data>(content);
    }
  }
}
