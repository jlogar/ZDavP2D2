using System.IO;
using System.Text;
using ZDavP2D2.FileWriters;

namespace ZDavP2D2.Test
{
    public class FileWriterTestBase<TWriter> where TWriter : IHavePath
    {
        protected TWriter Writer;

        protected StreamReader GetReader()
        {
            return new StreamReader(Writer.Path, Encoding.GetEncoding("windows-1250"));
        }
    }
}