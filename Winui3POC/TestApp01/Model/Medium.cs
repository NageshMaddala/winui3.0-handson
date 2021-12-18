using Dapper.Contrib.Extensions;
using TestApp01.Enums;
namespace TestApp01.Model;

public class Medium
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemType MediaType { get; set; }
}