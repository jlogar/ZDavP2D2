using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
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

        private StreamReader GetReader()
        {
            return new StreamReader(_writer.Path, Encoding.GetEncoding("windows-1250"));
        }

        [Test]
        public void Should_write_one_line_when_no_records_present()
        {
            _writer.Write(new InvoiceAggregateRecord[0]);

            using (var reader = GetReader())
            {
                var line = reader.ReadLine();
                Assert.AreNotEqual(null, line);
            }
        }

        [Test]
        public void Should_write_use_colon_for_separator()
        {
            _writer.Write(new InvoiceAggregateRecord[0]);

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
            _writer.Write(new InvoiceAggregateRecord[0]);

            using (var reader = GetReader())
            {
                var headerLine = reader.ReadLine();
                var header = headerLine.Split(new[] { ";" }, StringSplitOptions.None);
                Assert.AreEqual("Dav št", header[0]);
                Assert.AreEqual("Rac st", header[1]);
                Assert.AreEqual("Rac dat", header[2]);
                Assert.AreEqual("Rac ura", header[3]);
                Assert.AreEqual("PE id", header[4]);
                Assert.AreEqual("Blag id", header[5]);
                Assert.AreEqual("Kupec", header[6]);
                Assert.AreEqual("IŠ za DDV", header[7]);
                Assert.AreEqual("Rac znesek", header[8]);
                Assert.AreEqual("Rac 8,5 % DDV", header[9]);
                Assert.AreEqual("Rac 20 % DDV", header[10]);
                Assert.AreEqual("Plac got", header[11]);
                Assert.AreEqual("Plac kart", header[12]);
                Assert.AreEqual("Plac ostalo", header[13]);
                Assert.AreEqual("Sprem dat", header[14]);
                Assert.AreEqual("Sprem ura", header[15]);
                Assert.AreEqual("Sprem st", header[16]);
                Assert.AreEqual("Sprem id", header[17]);
                Assert.AreEqual("Sprem razlog", header[18]);
                Assert.AreEqual("Sprem upor", header[19]);
                Assert.AreEqual("Sprem oseba", header[20]);
                Assert.AreEqual("Rac opombe", header[21]);
            }
        }

        [Test]
        public void Should_write_header_and_one_record()
        {
            _writer.Write(new List<InvoiceAggregateRecord> { new InvoiceAggregateRecord { } });

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
            _writer.Write(new List<InvoiceAggregateRecord> { new InvoiceAggregateRecord(), new InvoiceAggregateRecord() });

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
            var record = new InvoiceAggregateRecord { DavSt = "12345678" };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.DavSt, 0);
            }
        }

        [Test]
        public void Should_write_RacSt()
        {
            var record = new InvoiceAggregateRecord { RacSt = "2013/23" };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.RacSt, 1);
            }
        }

        [Test]
        public void Should_write_RacDat()
        {
            var record = new InvoiceAggregateRecord { RacDat = new DateTime(2013, 6, 25, 18, 07, 12) };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("25062013", 2);
            }
        }

        [Test]
        public void Should_write_RacUr()
        {
            var record = new InvoiceAggregateRecord { RacDat = new DateTime(2013, 6, 25, 18, 07, 12) };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("18:07", 3);
            }
        }

        [Test]
        public void Should_write_PeId()
        {
            var record = new InvoiceAggregateRecord { PeId = "pe" };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.PeId, 4);
            }
        }

        [Test]
        public void Should_write_BlagId()
        {
            var record = new InvoiceAggregateRecord { BlagId = "blag" };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.BlagId, 5);
            }
        }

        [Test]
        public void Should_write_Kupec()
        {
            var record = new InvoiceAggregateRecord { Kupec = "John Doe" };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.Kupec, 6);
            }
        }

        [Test]
        public void Should_write_IsZaDdv()
        {
            var record = new InvoiceAggregateRecord { IsZaDdv = "12345678" };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.IsZaDdv, 7);
            }
        }

        [Test]
        public void Should_write_RacZnesek()
        {
            var record = new InvoiceAggregateRecord { RacZnesek = 987654.67m };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("987654,67", 8);
            }
        }

        [Test]
        public void Should_write_negative_RacZnesek()
        {
            var record = new InvoiceAggregateRecord { RacZnesek = -987654.67m };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("-987654,67", 8);
            }
        }

        [Test]
        public void Should_write_Rac85Ddv()
        {
            var record = new InvoiceAggregateRecord { Rac85Ddv = 987654.67m };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("987654,67", 9);
            }
        }

        [Test]
        public void Should_write_Rac20Ddv()
        {
            var record = new InvoiceAggregateRecord { Rac20Ddv = 987654.67m };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("987654,67", 10);
            }
        }

        [Test]
        public void Should_write_PlacGot()
        {
            var record = new InvoiceAggregateRecord { PlacGot = 987654.67m };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("987654,67", 11);
            }
        }

        [Test]
        public void Should_write_negative_PlacGot()
        {
            var record = new InvoiceAggregateRecord { PlacGot = -987654.67m };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("-987654,67", 11);
            }
        }

        [Test]
        public void Should_write_PlacKart()
        {
            var record = new InvoiceAggregateRecord { PlacKart = 987654.67m };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("987654,67", 12);
            }
        }

        [Test]
        public void Should_write_negative_PlacKart()
        {
            var record = new InvoiceAggregateRecord { PlacKart = -987654.67m };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("-987654,67", 12);
            }
        }

        [Test]
        public void Should_write_PlacOstalo()
        {
            var record = new InvoiceAggregateRecord { PlacOstalo = 987654.67m };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("987654,67", 13);
            }
        }

        [Test]
        public void Should_write_negative_PlacOstalo()
        {
            var record = new InvoiceAggregateRecord { PlacOstalo = -987654.67m };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("-987654,67", 13);
            }
        }

        [Test]
        public void Should_write_SpremDat()
        {
            var record = new InvoiceAggregateRecord { SpremDat = new DateTime(2013, 6, 25, 18, 07, 12) };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("25062013", 14);
            }
        }

        [Test]
        public void Should_write_SpremUra()
        {
            var record = new InvoiceAggregateRecord { SpremDat = new DateTime(2013, 6, 25, 18, 07, 12) };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("18:07", 15);
            }
        }

        [Test]
        public void Should_write_SpremSt()
        {
            var record = new InvoiceAggregateRecord { SpremSt = 123 };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("123", 16);
            }
        }

        [Test]
        public void Should_write_SpremId_Storno()
        {
            var record = new InvoiceAggregateRecord { SpremId = InvoiceChangeType.Storno };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("S", 17);
            }
        }

        [Test]
        public void Should_write_SpremId_Dobropis()
        {
            var record = new InvoiceAggregateRecord { SpremId = InvoiceChangeType.Dobropis };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("D", 17);
            }
        }

        [Test]
        public void Should_write_SpremId_Ostalo()
        {
            var record = new InvoiceAggregateRecord { SpremId = InvoiceChangeType.Ostalo };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("O", 17);
            }
        }

        [Test]
        public void Should_write_SpremRazlog()
        {
            var record = new InvoiceAggregateRecord { SpremRazlog = "some reason" };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.SpremRazlog, 18);
            }
        }

        [Test]
        public void Should_write_SpremUpor()
        {
            var record = new InvoiceAggregateRecord { SpremUpor = "some user" };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.SpremUpor, 19);
            }
        }

        [Test]
        public void Should_write_SpremOseba()
        {
            var record = new InvoiceAggregateRecord { SpremOseba = "John 1 Doe" };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.SpremOseba, 20);
            }
        }

        [Test]
        public void Should_write_RacOpombe()
        {
            var record = new InvoiceAggregateRecord { RacOpombe = "some notes" };
            _writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.RacOpombe, 21);
            }
        }
    }

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
