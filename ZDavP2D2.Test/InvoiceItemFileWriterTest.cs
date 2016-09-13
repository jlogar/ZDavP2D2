using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ZDavP2D2.FileWriters;

namespace ZDavP2D2.Test
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
                HeaderFieldAsserter
                    .From(header)
                    .AssertField("Dav st")
                    .AssertField("Rac dat")
                    .AssertField("Rac cas")
                    .AssertField("Racst pp")
                    .AssertField("Racst en")
                    .AssertField("Racst zap")
                    .AssertField("Dav st zav")
                    .AssertField("Post id")
                    .AssertField("Post opis")
                    .AssertField("Post kol")
                    .AssertField("Post em")
                    .AssertField("Post em cena")
                    .AssertField("Post vrednost")
                    .AssertField("Post 9,5 % DDV")
                    .AssertField("Post 22 % DDV")
                    .AssertField("Post 8 % pav")
                    .AssertField("Post davki ostalo")
                    .AssertField("Post oprosc")
                    .AssertField("Post dob76a")
                    .AssertField("Post neobd")
                    .AssertField("Post poseb")
                    .AssertField("Sprem nep st")
                    .AssertField("Post opombe")
                    ;
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

        public class FieldAsserter
        {
            private int _index;
            private readonly string[] _fieldValues;

            private FieldAsserter(string line)
            {
                _index = 0;
                _fieldValues = line.Split(new[] { ';' });
            }

            public static FieldAsserter FromReader(StreamReader reader)
            {
                //header
                reader.ReadLine();
                return new FieldAsserter(reader.ReadLine());
            }

            public FieldAsserter AssertNextValue(string value)
            {
                Assert.AreEqual(value, _fieldValues[_index],
                    string.Format("Expected \"{0}\" at index {1}. Got \"{2}\" instead.", value, _index, _fieldValues[_index]));
                _index++;
                return this;
            }
        }

        [Test]
        public void Should_write_values()
        {
            var record = new InvoiceItemRecord
            {
                DavSt = "12345678",
                RacDat = new DateTime(2016, 9, 13, 18, 41, 53),
                RacStPp = "ZOC",
                RacStEn = "BL3",
                RacStZap = 33,
                DavStZav = "87654321",
                PostId = "45",
                PostOpis = "Ekstrakcija",
                PostKol = 12.3m,
                PostEm = "KOS",
                PostEmCena = 3.11m,
                PostVrednost = 12.3m * 3.11m,
                Post95Ddv = 3.41m,
                Post22Ddv = 2.43m,
                Post8Pav = 1.35m,
                PostDavkiOstalo = 6.35m,
                PostOprosc = 11.35m,
                PostDob76A = 99.63m,
                PostNeobd = 12.53m,
                PostPoseb = 3.4m,
                SpremNepSt = 2,
                PostOpombe = "opombe za FURS"
            };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                FieldAsserter
                    .FromReader(reader)
                    .AssertNextValue(record.DavSt)
                    .AssertNextValue(record.RacDat.ToString("yyyyMMdd"))
                    .AssertNextValue(record.RacCas.ToString("hh\\:mm\\:ss"))
                    .AssertNextValue("ZOC")
                    .AssertNextValue("BL3")
                    .AssertNextValue("33")
                    .AssertNextValue("87654321")
                    .AssertNextValue("45")
                    .AssertNextValue("Ekstrakcija")
                    .AssertNextValue("12,30")
                    .AssertNextValue("KOS")
                    .AssertNextValue("3,11")
                    .AssertNextValue("38,25")
                    .AssertNextValue("3,41")
                    .AssertNextValue("2,43")
                    .AssertNextValue("1,35")
                    .AssertNextValue("6,35")
                    .AssertNextValue("11,35")
                    .AssertNextValue("99,63")
                    .AssertNextValue("12,53")
                    .AssertNextValue("3,40")
                    .AssertNextValue("2")
                    .AssertNextValue("opombe za FURS")
                    ;
            }
        }

        [Test]
        public void Should_write_empty_DavStZav_when_null()
        {
            var record = new InvoiceItemRecord { DavStZav = null };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("", 6);
            }
        }
        [Test]
        public void Should_write_PostKol_with_correct_form_when_no_decimals_give()
        {
            var record = new InvoiceItemRecord { PostKol = 125m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("125,00", 9);
            }
        }

        [Test]
        public void Should_write_negative_PostKol()
        {
            var record = new InvoiceItemRecord { PostKol = -548795.78m };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("-548795,78", 9);
            }
        }

        [Test]
        public void Should_write_empty_string_when_SpremNepSt_is_null()
        {
            var record = new InvoiceItemRecord { PostPoseb = 1.3m,SpremNepSt = null };
            Writer.Write(new List<InvoiceItemRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(string.Empty, 21);
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
