using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
      var result = new List<Entity>();

      foreach (var buildingItem in _data.buildings)
      {
        buildingItem.CalculateWeight(text, _dict);

        if (buildingItem.Weight > 0)
        {
          var newBuildingItem = new Building(buildingItem);
          result.Add(newBuildingItem);
          buildingItem.Weight = 0;
        }
      }

      foreach (var lockItem in _data.locks)
      {
        lockItem.CalculateWeight(text);

        if (lockItem.Weight > 0)
        {
          var newLockItem = new Lock(lockItem);
          result.Add(newLockItem);
          lockItem.Weight = 0;
        }
      }

      foreach (var groupItem in _data.groups)
      {
        groupItem.CalculateWeight(text, _dict);

        if (groupItem.Weight > 0)
        {
          var newGroupItem = new Group(groupItem);
          result.Add(newGroupItem);
          groupItem.Weight = 0;
        }
      }

      foreach (var mediumItem in _data.media)
      {
        mediumItem.CalculateWeight(text);

        if (mediumItem.Weight > 0)
        {
          var newMediumItem = new Medium(mediumItem);
          result.Add(newMediumItem);
          mediumItem.Weight = 0;
        }
      }

      // Sort by weight descending
      result = result.OrderByDescending(r => r.Weight).ToList();

      return JsonConvert.SerializeObject(result);
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

    /// <summary>
    /// Calculates Weight of the property based on Searched text
    /// Weight of the "Full match" is 10x more than weighht of the "Partial match"
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyWeight"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public static int GetWeight(string property, int propertyWeight, string text)
    {
      // If propert is null or empty return 0
      if (string.IsNullOrEmpty(property))
      {
        return 0;
      }

      if (property.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
      {
        // Check if it is a full match or not
        int multiplier = property.Length == text.Length ? 10 : 1;
        return multiplier * propertyWeight;
      }

      return 0;
    }
  }
}
