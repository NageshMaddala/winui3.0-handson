using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.UI.Xaml.Input;
using TestApp01.Enums;
using TestApp01.Interfaces;
using TestApp01.Model;

namespace TestApp01.ViewModels;

public class MainViewModel : BindableBase
{
    private string selectedMedium;
    private ObservableCollection<MediaItem> items = new ObservableCollection<MediaItem>();
    private ObservableCollection<MediaItem> allItems;
    private IList<string> mediums;
    private MediaItem selectedMediaItem;
    private int additionalItemCount = 1;
    private const string AllMediums = "All";

    public MainViewModel(INavigationService navigationService, ISqliteDataService dataService)
    {
        _navigationService = navigationService;
        _sqliteDataService = dataService;

        //PopulateData();

        //DeleteCommand = new RelayCommand(DeleteItem, CanDeleteItem);
        DeleteCommand = new RelayCommand(async () => await DeleteItemAsync(), CanDeleteItem);
        
        // No CanExecute param is needed for this command
        // because you can always add or edit items.
        AddEditCommand = new RelayCommand(AddOrEditItem);
        PopulateDataAsync();
    }

    public async Task PopulateDataAsync()
    {
        items.Clear();

        foreach (var item in await _sqliteDataService.GetItemsAsync())
        {
            items.Add(item);
        }

        allItems = new ObservableCollection<MediaItem>(Items);

        mediums = new ObservableCollection<string>
        {
            AllMediums
        };

        foreach (var itemType in _sqliteDataService.GetItemTypes())
        {
            mediums.Add(itemType.ToString());
        }

        selectedMedium = Mediums[0];
    }


    public void PopulateData()
    {
        //var cd = new MediaItem
        //{
        //    Id = 1,
        //    Name = "Classical Favorites",
        //    MediaType = ItemType.Music,
        //    MediumInfo = new Medium { Id = 1, MediaType = ItemType.Music, Name = "CD" }
        //};

        //var book = new MediaItem
        //{
        //    Id = 2,
        //    Name = "Classic Fairy Tales",
        //    MediaType = ItemType.Book,
        //    MediumInfo = new Medium { Id = 2, MediaType = ItemType.Book, Name = "Book" }
        //};

        //var bluRay = new MediaItem
        //{
        //    Id = 3,
        //    Name = "The Mummy",
        //    MediaType = ItemType.Video,
        //    MediumInfo = new Medium { Id = 3, MediaType = ItemType.Video, Name = "Blu Ray" }
        //};

        //items = new ObservableCollection<MediaItem>
        //    {
        //        cd,
        //        book,
        //        bluRay
        //    };

        //allItems = new ObservableCollection<MediaItem>(Items);

        //mediums = new List<string>
        //    {
        //        "All",
        //        nameof(ItemType.Book),
        //        nameof(ItemType.Music),
        //        nameof(ItemType.Video)
        //    };

        //selectedMedium = Mediums[0];

        items.Clear();

        foreach (var item in _dataService.GetItems())
        {
            items.Add(item);
        }

        allItems = new ObservableCollection<MediaItem>(Items);

        mediums = new ObservableCollection<string>
        {
            AllMediums
        };

        foreach (var itemType in _dataService.GetItemTypes())
        {
            mediums.Add(itemType.ToString());
        }

        selectedMedium = Mediums[0];
    }

    public ObservableCollection<MediaItem> Items
    {
        get
        {
            return items;
        }
        set
        {
            SetProperty(ref items, value);
        }
    }

    public IList<string> Mediums
    {
        get
        {
            return mediums;
        }
        set
        {
            SetProperty(ref mediums, value);
        }
    }

    public string SelectedMedium
    {
        get
        {
            return selectedMedium;
        }
        set
        {
            SetProperty(ref selectedMedium, value);

            Items.Clear();

            foreach (var item in allItems)
            {
                if (string.IsNullOrWhiteSpace(selectedMedium) ||
                    selectedMedium == "All" ||
                    selectedMedium == item.MediaType.ToString())
                {
                    Items.Add(item);
                }
            }
        }
    }

    public MediaItem SelectedMediaItem
    {
        get => selectedMediaItem;
        set
        {
            SetProperty(ref selectedMediaItem, value);
            ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
        }
    }

    public ICommand AddEditCommand { get; set; }

    public void AddOrEditItem()
    {
        // Note this is temporary until
        // we use a real data source for items.
        //const int startingItemCount = 3;

        //var newItem = new MediaItem
        //{
        //    Id = startingItemCount + additionalItemCount,
        //    Location = LocationType.InCollection,
        //    MediaType = ItemType.Music,
        //    MediumInfo = new Medium { Id = 1, MediaType = ItemType.Music, Name = "CD" },
        //    Name = $"CD {additionalItemCount}"
        //};

        //allItems.Add(newItem);
        //Items.Add(newItem);
        //additionalItemCount++;
        var selectedItemId = -1;

        if (SelectedMediaItem != null)
        {
            selectedItemId = SelectedMediaItem.Id;
        }

        _navigationService.NavigateTo("ItemDetailsPage", selectedItemId);
    }

    public ICommand DeleteCommand { get; set; }

    public void DeleteItem()
    {
        allItems.Remove(SelectedMediaItem);
        Items.Remove(SelectedMediaItem);
    }

    private async Task DeleteItemAsync()
    {
        await _sqliteDataService.DeleteItemAsync(SelectedMediaItem);
        Items.Remove(SelectedMediaItem);
    }

    private bool CanDeleteItem() => selectedMediaItem != null;

    public void ListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        AddOrEditItem();
    }
}