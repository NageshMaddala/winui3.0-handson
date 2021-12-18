using Dapper.Contrib.Extensions;
using TestApp01.Enums;

namespace TestApp01.Model;

public class MediaItem
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemType MediaType { get; set; }
    public Medium MediumInfo { get; set; }
    public LocationType Location { get; set; }
    [Computed]
    public int MediumId => MediumInfo.Id;
}