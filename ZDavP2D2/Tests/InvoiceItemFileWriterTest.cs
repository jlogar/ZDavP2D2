using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace ZDavP2D2.Tests
{
    [TestFixture]
    public class InvoiceItemFileWriterTest : FileWriterTestBase<InvoiceItemFileWriter>
    {
        [SetUp]
        public void SetUp()
        {
            Writer = new InvoiceItemFileWriter();
        }

        [Test]
        public void Should_write_to_IZPIS_RAČUNI_POSTAVKE_TXT_by_default_When_no_filename_specified()
        {
            Writer.Write(new InvoiceItemRecord[0]);

            Assert.IsTrue(File.Exists("IZPIS RAČUNI POSTAVKE.TXT"));
        }

        [Test]
        public void Should_write_one_line_when_no_records_present()
        {
            Writer.Write(new InvoiceItemRecord[0]);

            using (var reader = GetReader())
            {
                var line = reader.ReadLine();
                Assert.AreNotEqual(null, line);
            }
        }

        [Test]
        public void Should_write_use_colon_for_separator()
        {
            Writer.Write(new InvoiceItemRecord[0]);

            using (var reader = GetReader())
            {
                var headerLine = reader.ReadLine();
                Assert.AreNotEqual(null, headerLine);
                var header = headerLine.Split(new[] { ";" }, StringSplitOptions.None);
                Assert.Greater(header.Length, 1);
            }
        }

        [Test]
        public void Should_write_header()
        {
            Writer.Write(new InvoiceItemRecord[0]);

            using (var reader = GetReader())
            {
                var headerLine = reader.ReadLine();
                var header = headerLine.Split(new[] { ";" }, StringSplitOptions.None);
                Assert.AreEqual("Dav št", header[0]);
                Assert.AreEqual("Rac st", header[1]);
                Assert.AreEqual("Rac dat", header[2]);
                Assert.AreEqual("PE id", header[3]);
                Assert.AreEqual("Blag id", header[4]);
                Assert.AreEqual("Post st", header[5]);
                Assert.AreEqual("Post id", header[6]);
                Assert.AreEqual("Post opis", header[7]);
                Assert.AreEqual("Post kol", header[8]);
                Assert.AreEqual("Post em", header[9]);
                Assert.AreEqual("Post znesek", header[10]);
                Assert.AreEqual("Post 8,5 % DDV", header[11]);
                Assert.AreEqual("Post 20 % DDV", header[12]);
                Assert.AreEqual("Sprem st", header[13]);
                Assert.AreEqual("Post opombe", header[14]);
            }
        }

        [Test]
        public void Should_write_header_and_one_record()
        {
            Writer.Write(new List<InvoiceItemRecord> { new InvoiceItemRecord() });

            using (var reader = GetReader())
            {
                var line = reader.ReadLine();
                Assert.AreNotEqual(null, line);
                line = reader.ReadLine();
                Assert.AreNotEqual(null, line);
                line = reader.ReadLine();
                Assert.AreEqual(null, line);
            }
        }

        [Test]
        public void Should_write_header_and_two_records()
        {
            Writer.Write(new List<InvoiceItemRecord> { new InvoiceItemRecord(), new InvoiceItemRecord() });

            using (var reader = GetReader())
            {
                var line = reader.ReadLine();
                Assert.AreNotEqual(null, line);
                line = reader.ReadLine();
                Assert.AreNotEqual(null, line);
                line = reader.ReadLine();
                Assert.AreNotEqual(null, line);
                line = reader.ReadLine();
                Assert.AreEqual(null, line);
            }
        }

        [Test]
        public void Should_write_DavSt()
        {
            var record = new InvoiceItemRecord { DavSt = "12345678" };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.DavSt, 0);
            }
        }
    }
}
