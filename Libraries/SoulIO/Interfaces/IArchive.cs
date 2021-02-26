using System;
using System.Collections.Generic;
using System.Text;

namespace SoulIO.Interfaces
{
    public interface IArchive
    {
        IArchive Parent { get; set; }
        Int32 Count { get; set; }
        List<IArchiveChild> Children { get; set; }
    }
}
