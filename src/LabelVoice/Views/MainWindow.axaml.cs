using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using LabelVoice.ViewModels;
using ReactiveUI;

namespace LabelVoice.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            SetupNativeMenu();
            
            btnGetProjectRoot.Click += async (sender, e) => await GetProjectRoot();
        }

        private void SetupNativeMenu()
        {
            var item1 = new NativeMenuItem("Item1");
            var item2 = new NativeMenuItem("Item2");
            var menu1 = new NativeMenu();
            var menu2 = new NativeMenu();
            item2.Menu = menu1;
            menu1.Add(item1);
            menu2.Add(item2);
            NativeMenu.SetMenu(this, menu2);
        }

        private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            var lang = item.Content.ToString();
            App.SetCulture(lang);
        }

        private async Task GetProjectRoot()
        {
            var dlg = new OpenFolderDialog();

            var window = this.GetVisualRoot() as Window;
            if (window is null)
            {
                return;
            }

            var result = await dlg.ShowAsync(window);
            if (result != null)
            {
                string strFolder = result;

                ((MainWindowViewModel)DataContext!).OpenProjectRoot(strFolder);
            }
        }
    }
}
