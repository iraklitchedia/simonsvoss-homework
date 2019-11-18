using Newtonsoft.Json;

namespace Simonsvoss_Homework.Models
{
  public class Medium : Entity
  {
    [JsonIgnore] public int TypeWeight = 3;
    [JsonIgnore] public int OwnerWeight = 10;
    [JsonIgnore] public int SerialNumberWeight = 8;
    [JsonIgnore] public int DescriptionWeight = 6;

    public string GroupId { get; set; }
    public string Type { get; set; }
    public string Owner { get; set; }
    public string SerialNumber { get; set; }
    public string Description { get; set; }
  }
}