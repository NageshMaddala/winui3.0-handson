using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp01.Enums;
using TestApp01.Model;

namespace TestApp01.Interfaces;

public interface IDataService
{
    IList<MediaItem> GetItems();
    MediaItem GetItem(int id);
    int AddItem(MediaItem item);
    void UpdateItem(MediaItem item);
    IList<ItemType> GetItemTypes();
    Medium GetMedium(string name);
    IList<Medium> GetMediums();
    IList<Medium> GetMediums(ItemType itemType);
    IList<LocationType> GetLocationTypes();
    Task InitializeDataAsync();
}

public interface ISqliteDataService
{
    Task InitializeDataAsync();
    Task<IList<MediaItem>> GetItemsAsync();
    Task<MediaItem> GetItemAsync(int id);
    Task<int> AddItemAsync(MediaItem item);
    Task UpdateItemAsync(MediaItem item);
    Task DeleteItemAsync(MediaItem item);
    IList<ItemType> GetItemTypes();
    Medium GetMedium(string name);
    Medium GetMedium(int id);
    IList<Medium> GetMediums();
    IList<Medium> GetMediums(ItemType itemType);
    IList<LocationType> GetLocationTypes();
}