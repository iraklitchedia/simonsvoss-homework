using Newtonsoft.Json;

namespace Simonsvoss_Homework.Models
{
  public class Lock : Entity
  {
    [JsonIgnore] public int TypeWeight = 3;
    [JsonIgnore] public int NameWeight = 10;
    [JsonIgnore] public int SerialNumberWeight = 8;
    [JsonIgnore] public int FloorWeight = 6;
    [JsonIgnore] public int RoomNumberWeight = 6;
    [JsonIgnore] public int DescriptionWeight = 6;

    public string BuildingId { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Floor { get; set; }
    public string RoomNumber { get; set; }
    public string Description { get; set; }

    public void CalculateWeight(string text)
    {
      Weight += SearchService.GetWeight(Type, TypeWeight, text);
      Weight += SearchService.GetWeight(Name, NameWeight, text);
      Weight += SearchService.GetWeight(SerialNumber, SerialNumberWeight, text);
      Weight += SearchService.GetWeight(Floor, FloorWeight, text);
      Weight += SearchService.GetWeight(RoomNumber, RoomNumberWeight, text);
      Weight += SearchService.GetWeight(Description, DescriptionWeight, text);
    }
  }
}