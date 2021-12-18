using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using TestApp01.Model;
using TestApp01.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestApp01.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestPage1 : Page
    {
        public IList<MediaItem> _items { get; set; }
        private IList<MediaItem> _allItems { get; set; }
        private IList<string> _mediums { get; set; }
        private bool _isLoaded;

        public TestPage1()
        {
            InitializeComponent();
        }

        // use DI here to resolve now
        //public MainViewModel ViewModel => new MainViewModel();
        public MainViewModel ViewModel { get; } = (MainViewModel)(Application.Current as App).Container.GetService(typeof(MainViewModel));
    }
}
