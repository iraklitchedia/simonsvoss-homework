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
  }
}