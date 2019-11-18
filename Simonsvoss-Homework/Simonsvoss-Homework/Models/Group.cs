using Newtonsoft.Json;
using System.Collections.Generic;

namespace Simonsvoss_Homework.Models
{
  public class Group : Entity
  {
    [JsonIgnore] public int NameWeight = 9;
    [JsonIgnore] public int NameTWeight = 8;
    [JsonIgnore] public int DescriptionWeight = 5;
    [JsonIgnore] public int DescriptionTWeight = 0;

    public string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public List<string> Media { get; set; }

    public Group()
    {
      Media = new List<string>();
    }

    public Group(Group group)
    {
      Id = group.Id;
      Weight = group.Weight;
      Name = group.Name;
      Description = group.Description;
    }

    public void CalculateWeight(string text, Dictionary<string, Entity> dict)
    {
      int nameWeight = SearchService.GetWeight(Name, NameWeight, text);
      int descriptionWeight = SearchService.GetWeight(Description, DescriptionWeight, text);

      Weight = nameWeight + descriptionWeight;

      // Assign transitive weights to Mediums related to this Group
      if (Weight > 0)
      {
        int tWeight = nameWeight / NameWeight * NameTWeight + descriptionWeight / DescriptionWeight * DescriptionTWeight;
        foreach (var mediumId in Media)
        {
          if (dict.ContainsKey(mediumId))
          {
            dict[mediumId].Weight = tWeight;
          }
        }
      }
    }
  }
}