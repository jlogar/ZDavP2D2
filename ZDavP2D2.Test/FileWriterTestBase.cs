﻿using System.IO;
using System.Text;

namespace ZDavP2D2.Tests
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