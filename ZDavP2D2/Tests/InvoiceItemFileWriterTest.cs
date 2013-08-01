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

        [Test]
        public void Should_write_RacSt()
        {
            var record = new InvoiceItemRecord { RacSt = "2013/98" };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.RacSt, 1);
            }
        }

        [Test]
        public void Should_write_RacDat()
        {
            var record = new InvoiceItemRecord { RacDat = new DateTime(2013, 06, 26, 13, 05, 32) };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.RacDat.ToString("ddMMyyyy"), 2);
            }
        }

        [Test]
        public void Should_write_PeId()
        {
            var record = new InvoiceItemRecord { PeId = "some pe" };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.PeId, 3);
            }
        }

        [Test]
        public void Should_write_BlagId()
        {
            var record = new InvoiceItemRecord { BlagId = "some blag" };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.BlagId, 4);
            }
        }

        [Test]
        public void Should_write_PostSt()
        {
            var record = new InvoiceItemRecord { PostSt = 321 };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("321", 5);
            }
        }

        [Test]
        public void Should_write_PostId()
        {
            var record = new InvoiceItemRecord { PostId = "123" };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.PostId, 6);
            }
        }

        [Test]
        public void Should_write_PostOpis()
        {
            var record = new InvoiceItemRecord { PostOpis = "some service" };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.PostOpis, 7);
            }
        }

        [Test]
        public void Should_write_PostKol()
        {
            var record = new InvoiceItemRecord { PostKol = 548795.78m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("548795,78", 8);
            }
        }

        [Test]
        public void Should_write_PostKol_with_correct_form_when_no_decimals_give()
        {
            var record = new InvoiceItemRecord { PostKol = 125m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("125,00", 8);
            }
        }

        [Test]
        public void Should_write_negative_PostKol()
        {
            var record = new InvoiceItemRecord { PostKol = -548795.78m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("-548795,78", 8);
            }
        }

        [Test]
        public void Should_write_PostEm()
        {
            var record = new InvoiceItemRecord { PostEm = "KOS" };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.PostEm, 9);
            }
        }

        [Test]
        public void Should_write_PostZnesek()
        {
            var record = new InvoiceItemRecord { PostZnesek = 695847.87m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("695847,87", 10);
            }
        }

        [Test]
        public void Should_write_PostZnesek_with_correct_form_when_no_decimals_give()
        {
            var record = new InvoiceItemRecord { PostZnesek = 125m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("125,00", 10);
            }
        }

        [Test]
        public void Should_write_negative_PostZnesek()
        {
            var record = new InvoiceItemRecord { PostZnesek = -695847.87m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("-695847,87", 10);
            }
        }

        [Test]
        public void Should_write_Post85Ddv()
        {
            var record = new InvoiceItemRecord { Post85Ddv = 695847.87m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("695847,87", 11);
            }
        }

        [Test]
        public void Should_write_Post85Ddv_with_correct_form_when_no_decimals_given()
        {
            var record = new InvoiceItemRecord { Post85Ddv = 125m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("125,00", 11);
            }
        }

        [Test]
        public void Should_write_negative_Post85Ddv()
        {
            var record = new InvoiceItemRecord { Post85Ddv = -695847.87m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("-695847,87", 11);
            }
        }

        [Test]
        public void Should_write_Post20Ddv()
        {
            var record = new InvoiceItemRecord { Post20Ddv = 695847.87m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("695847,87", 12);
            }
        }
        
        [Test]
        public void Should_write_Post20Ddv_with_correct_form_when_no_decimals_given()
        {
            var record = new InvoiceItemRecord { Post20Ddv = 125m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("125,00", 12);
            }
        }

        [Test]
        public void Should_write_negative_Post20Ddv()
        {
            var record = new InvoiceItemRecord { Post20Ddv = -695847.87m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("-695847,87", 12);
            }
        }

        [Test]
        public void Should_write_SpremSt()
        {
            var record = new InvoiceItemRecord { SpremSt = 12 };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("12", 13);
            }
        }

        [Test]
        public void Should_write_PostOpombe()
        {
            var record = new InvoiceItemRecord { PostOpombe = "soem notes" };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.PostOpombe, 14);
            }
        }

        [Test]
        public void Should_write_empty_string_when_SpremSt_is_null()
        {
            var record = new InvoiceItemRecord { SpremSt = null };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(string.Empty, 13);
            }
        }

        [Test]
        public void Should_write_separator_after_last_header_field()
        {
            Writer.Write(new InvoiceItemRecord[0]);

            using (var reader = GetReader())
            {
                var line = reader.ReadLine();
                Assert.AreEqual(';', line[line.Length - 1]);
            }
        }

        [Test]
        public void Should_write_separator_after_last_field()
        {
            var record = new InvoiceItemRecord { PostOpombe = "soem notes" };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                //header
                reader.ReadLine();
                var line = reader.ReadLine();
                Assert.AreEqual(';', line[line.Length - 1]);
            }
        }
    }
}
