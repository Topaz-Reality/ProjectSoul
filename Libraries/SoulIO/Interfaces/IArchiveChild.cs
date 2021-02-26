using System;
using System.IO;

namespace SoulIO.Interfaces
{
    public interface IArchiveChild
    {
        IArchive Parent { get; set; }
        Int32 Offset { get; set; }
        Int32 Size { get; set; }
        Stream Data { get; set; }
    }
}
