using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Simonsvoss_Homework.Models;

namespace Simonsvoss_Homework
{
  public class SearchService : ISearchService
  {
    private readonly string _filepath = @".\Data\sv_lsm_data.json";
    private Data _data;
    // Dictionary will help to access data using Ids
    private readonly Dictionary<string, Entity> _dict;

    public SearchService()
    {
      _dict = new Dictionary<string, Entity>();

      // We do it here to avoid reading file every time search is requested
      GetDataFromJsonFile();

      // Setup dictionary
      InitDictionary();
    }

    /// <summary>
    /// Search text between the entities
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Add entities to dictionary
    /// </summary>
    private void InitDictionary()
    {
      foreach (var buildingItem in _data.buildings)
      {
        _dict.Add(buildingItem.Id, buildingItem);
      }

      foreach (var lockItem in _data.locks)
      {
        _dict.Add(lockItem.Id, lockItem);

        // Match Locks to Buildings
        if (!string.IsNullOrEmpty(lockItem.BuildingId) && _dict.ContainsKey(lockItem.BuildingId))
        {
          ((Building) _dict[lockItem.BuildingId]).Locks.Add(lockItem.Id);
        }
      }

      foreach (var groupItem in _data.groups)
      {
        _dict.Add(groupItem.Id, groupItem);
      }

      foreach (var mediumItem in _data.media)
      {
        _dict.Add(mediumItem.Id, mediumItem);

        // Match Mediums to Groups
        if (!string.IsNullOrEmpty(mediumItem.GroupId) && _dict.ContainsKey(mediumItem.GroupId))
        {
          ((Group) _dict[mediumItem.GroupId]).Media.Add(mediumItem.Id);
        }
      }
    }
  }
}
