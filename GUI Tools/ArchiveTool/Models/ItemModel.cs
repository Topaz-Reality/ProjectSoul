using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using SoulIO.Formats;
using SoulIO.Interfaces;

namespace ArchiveTool.Models
{
    public class ItemModel : ReactiveObject
    {
        Int32 _index;
        Byte _type;
        Object _item;
        ObservableCollection<ItemModel> _children;

        public String Tag
        {
            get => String.Format("File{0}", Index);
        }

        public Int32 Index
        {
            get => _index;
        }

        public Byte Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }

        public ObservableCollection<ItemModel> Children
        {
            get => _children;
        }

        public ItemModel(IArchiveChild Input)
        {
            _item = Input;
            _children = new ObservableCollection<ItemModel>();

            var _parent = Input.Parent as IArchive;

            _index = _parent.Children.FindIndex(x => x == _item);
            _type = 0;

            Archive _archive = Input as Archive.Child;

            if (_archive != null)
                foreach (var _child in _archive.Children)
                    _children.Add(new ItemModel(_child));
        }
    }
}
