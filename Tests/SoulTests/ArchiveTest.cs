using System.IO;

using SoulIO.Formats;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SoulTests
{
    [TestClass]
    public class ArchiveTest
    {
        string _testName = "../../../Assets/ArchiveTest/testpackage.pkg";

        [TestMethod]
        public void ArchiveCheck()
        {
            var _testStream = new FileStream(_testName, FileMode.Open);
            var _testArchive = new Archive(_testStream);

            // Header and Continuity.
            Assert.AreEqual(0x000007, _testArchive.Count);
            Assert.AreEqual(0x01C890, _testArchive.Size);

            // Children Checks
            Assert.AreEqual(0x000030, _testArchive.Children[0].Offset);
            Assert.AreEqual(0x0050F0, _testArchive.Children[1].Offset);
            Assert.AreEqual(0x008520, _testArchive.Children[2].Offset);
            Assert.AreEqual(0x008E30, _testArchive.Children[3].Offset);
            Assert.AreEqual(0x009060, _testArchive.Children[4].Offset);
            Assert.AreEqual(0x016960, _testArchive.Children[5].Offset);
            Assert.AreEqual(0x01C430, _testArchive.Children[6].Offset);
        }
        
        [TestMethod]
        public void SubArchiveCheck()
        {
            var _testStream = new FileStream(_testName, FileMode.Open);
            var _testArchive = new Archive(_testStream);

            Archive _subArchive01 = _testArchive.Children[5] as Archive.Child;
            Archive _subArchive02 = _testArchive.Children[6] as Archive.Child;

            // Sub-Archive Checks
            Assert.AreEqual(0x00000A, _subArchive01.Count);
            Assert.AreEqual(0x000008, _subArchive02.Count);
            Assert.AreEqual(0x005AD0, _subArchive01.Size);
            Assert.AreEqual(0x000460, _subArchive02.Size);

            // Sub-Archive Parental Checks
            Assert.AreEqual(_testArchive, _subArchive01.Parent);
            Assert.AreEqual(_testArchive, _subArchive02.Parent);
        }
    }
}
