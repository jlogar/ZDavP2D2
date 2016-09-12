using System.IO;
using NUnit.Framework;

namespace ZDavP2D2.Test
{
    public static class ReaderAssertExtensions
    {
        public static void AssertFieldValue(this StreamReader reader, string value, int index)
        {
            //header
            reader.ReadLine();
            var line = reader.ReadLine();
            Assert.AreNotEqual(null, line);
            var fields = line.Split(new[] { ';' });
            Assert.AreEqual(value, fields[index]);
        }
    }
}