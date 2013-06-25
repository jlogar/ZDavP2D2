using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace ZDavP2D2.Tests
{
    [TestFixture]
    public class InvoiceAggregateFileWriterTest
    {
        private InvoiceAggregateFileWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _writer = new InvoiceAggregateFileWriter();
        }

        [Test]
        public void Should_write_to_IZPIS_RAČUNI_GLAVE_TXT_by_default_When_no_filename_specified()
        {
            _writer.Write(new List<InvoiceAggregateRecord>());

            Assert.IsTrue(File.Exists(_writer.Path));
        }

        [Test]
        public void Should_write_one_record()
        {
            _writer.Write(new List<InvoiceAggregateRecord> { new InvoiceAggregateRecord { } });

            using (var reader = File.OpenText(_writer.Path))
            {
                var line = reader.ReadLine();
                Assert.AreNotEqual(null, line);
                line = reader.ReadLine();
                Assert.AreEqual(null, line);
            }
        }
    }
}
