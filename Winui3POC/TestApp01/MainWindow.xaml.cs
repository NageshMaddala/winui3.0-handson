using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestApp01
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            Title = "TestApp01";
            mainWindow.IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
            mainWindow.IsSettingsVisible = false;
            mainWindow.SelectionChanged += MainWindow_SelectionChanged;
        }

        private void MainWindow_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = args.SelectedItemContainer as NavigationViewItem;

            if (item == null || item.Tag == null)
                return;

            contentFrame.Navigate(Type.GetType(item.Tag.ToString()), item.Content);
            mainWindow.SelectedItem = item;
        }
    }
}
