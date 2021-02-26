using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ReactiveUI;
using Avalonia.Controls;

using ArchiveTool.Views;
using ArchiveTool.Models;
using SoulIO.Interfaces;
using SoulIO.Formats;

namespace ArchiveTool.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        MainWindow Instance;
        IArchive _currArchive;

        public ObservableCollection<ItemModel> Items { get; private set; }
        public IReactiveCommand OpenCommand => ReactiveCommand.Create(OpenEvent);

        public MainWindowViewModel()
        {
            Instance = MainWindow.Instance;
            Items = new ObservableCollection<ItemModel>();

            this.RaisePropertyChanged();
        }

        async void OpenEvent()
        {
            var _dialog = new OpenFileDialog()
            {
                Title = "Open an Archive...",
                Filters = new List<FileDialogFilter>
                {
                     new FileDialogFilter() { Name = "Namco Archive", Extensions = new List<string>() { "pkg" } },
                     new FileDialogFilter() { Name = "All Files", Extensions = new List<string>() { "*" } },
                }
            };

            var _files = await _dialog.ShowAsync(Instance);

            if (_files != null && _files.Length == 1)
            {
                Items.Clear();

                using (FileStream _stream = new FileStream(_files[0], FileMode.Open))
                {
                    _currArchive = new Archive(_stream);

                    foreach (var _item in _currArchive.Children)
                    {
                        var _treeItem = new ItemModel(_item);
                        Items.Add(_treeItem);
                    }

                    this.RaisePropertyChanged(nameof(Items));
                }
            }
        }
    }
}