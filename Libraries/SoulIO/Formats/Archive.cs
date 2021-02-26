using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using SoulIO.Interfaces;

namespace SoulIO.Formats
{
    public class Archive : IArchive
    {
        public class Child : IArchiveChild
        {
            IntPtr _parent;
            Int32 _offset;
            Int32 _length;
            Byte[] _data;

            public IArchive Parent
            {
                get
                {
                    if (_parent == IntPtr.Zero)
                        return null;

                    GCHandle _handle = (GCHandle)_parent;
                    return _handle.Target as IArchive;
                }

                set
                {
                    if (value == null)
                        _parent = IntPtr.Zero;

                    else
                    {
                        GCHandle _handle = GCHandle.Alloc(value);
                        _parent = (IntPtr)_handle;
                    }
                }
            }
            public Int32 Offset 
            { 
                get => _offset; 
                set => _offset = value; 
            }
            public Int32 Size 
            { 
                get => _length; 
                set => _length = value; 
            }
            public Stream Data
            {
                get
                {
                    var _stream = new MemoryStream(_data);
                    return _stream;
                }

                set
                {
                    var _stream = (MemoryStream)value;
                    _stream.Position = 0;

                    _data = _stream.ToArray();
                }
            }

            public Child() { }
        }

        IntPtr _parent;
        Int32 _count;
        Int32 _length;
        List<Child> _children;

        public IArchive Parent
        {
            get
            {
                if (_parent == IntPtr.Zero)
                    return null;

                GCHandle _handle = (GCHandle)_parent;
                return _handle.Target as IArchive;
            }

            set
            {
                if (value == null)
                    _parent = IntPtr.Zero;

                else
                {
                    GCHandle _handle = GCHandle.Alloc(value);
                    _parent = (IntPtr)_handle;
                }
            }
        }
        public Int32 Count 
        { 
            get => _count;
            set => _count = value;
        }
        public Int32 Size
        {
            get => _length;
            set => _length = value;
        }
        public List<IArchiveChild> Children 
        {
            get 
            {
                var _list = new List<IArchiveChild>();

                foreach (var _item in _children)
                    _list.Add(_item);

                return _list;
            }

            set
            {
                var _list = value;
                _children.Clear();

                foreach (var _item in _list)
                    _children.Add(_item as Child);
            }
        }

        public static implicit operator Archive(Child InputChild)
        {
            try
            {
                var _archive = new Archive(InputChild.Data);
                _archive.Parent = InputChild.Parent;

                return _archive;
            }

            catch (InvalidDataException)
            {
                return null;
            }
        }

        public Archive(Stream InputStream)
        {
            using (var _read = new BinaryReader(InputStream))
            {
                _count = _read.ReadInt32();
                _children = new List<Child>();

                for (Int32 i = 0; i < _count; i++)
                {
                    var _item = new Child();

                    var _currOffset = _read.ReadInt32();
                    var _nextOffset = _read.ReadInt32();

                    InputStream.Position -= 0x04;

                    _item.Offset = _currOffset;
                    _item.Size = _nextOffset - _currOffset;
                    _item.Parent = this;

                    _children.Add(_item);
                }

                _length = _read.ReadInt32();

                if (InputStream.Length != _length)
                    throw new InvalidDataException();

                _children.ForEach(
                    delegate (Child _item)
                    {
                        var _stream = new MemoryStream();

                        InputStream.Position = _item.Offset;
                        var _bytes = _read.ReadBytes(_item.Size);

                        _stream.Write(_bytes);
                        _item.Data = _stream;
                    }
                );
            }
        }
    }
}
