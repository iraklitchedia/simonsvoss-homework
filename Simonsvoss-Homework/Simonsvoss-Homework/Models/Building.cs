using Newtonsoft.Json;
using System.Collections.Generic;

namespace Simonsvoss_Homework.Models
{
  public class Building : Entity
  {
    [JsonIgnore] public int ShortCutWeight = 7;
    [JsonIgnore] public int ShortCutTWeight = 8;
    [JsonIgnore] public int NameWeight = 10;
    [JsonIgnore] public int NameTWeight = 5;
    [JsonIgnore] public int DescriptionWeight = 5;
    [JsonIgnore] public int DescriptionTWeight = 0;

    public string ShortCut { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public List<string> Locks { get; set; }

    public Building()
    {
      Locks = new List<string>();
    }

    public Building(Building building)
    {
      Id = building.Id;
      Weight = building.Weight;
      ShortCut = building.ShortCut;
      Name = building.Name;
      Description = building.Description;
      Locks = building.Locks;
    }

    public void CalculateWeight(string text, Dictionary<string, Entity> dict)
    {
      int shortCutWeight = SearchService.GetWeight(ShortCut, ShortCutWeight, text);
      int nameWeight = SearchService.GetWeight(Name, NameWeight, text);
      int descriptionWeight = SearchService.GetWeight(Description, DescriptionWeight, text);

      Weight = shortCutWeight + nameWeight + descriptionWeight;

      // Assign transitive weights to Locks related to this Building
      if (Weight > 0)
      {
        int tWeight = shortCutWeight / ShortCutWeight * ShortCutTWeight + nameWeight / NameWeight * NameTWeight + descriptionWeight / DescriptionWeight * DescriptionTWeight;
        foreach (var lockId in Locks)
        {
          if (dict.ContainsKey(lockId))
          {
            dict[lockId].Weight = tWeight;
          }
        }
      }
    }
  }
}