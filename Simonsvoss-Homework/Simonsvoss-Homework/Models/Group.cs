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
  }
}