using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ZDavP2D2.FileWriters;
using ZDavP2D2.Tests;

namespace ZDavP2D2.Test
{
    [TestFixture]
    public class InvoiceAggregateFileWriterTest : FileWriterTestBase<InvoiceAggregateFileWriter>
    {
        [SetUp]
        public void SetUp()
        {
            Writer = new InvoiceAggregateFileWriter();
        }
        [Test]
        public void Should_write_to_IZPIS_RAČUNI_GLAVE_TXT_in_working_directory_When_no_filename_specified()
        {
            Writer.Write(new InvoiceAggregateRecord[0]);

            Assert.IsTrue(File.Exists("IZPIS RAČUNI GLAVE.TXT"));
        }

        [Test]
        public void Should_write_one_line_when_no_records_present()
        {
            Writer.Write(new InvoiceAggregateRecord[0]);

            using (var reader = GetReader())
            {
                var line = reader.ReadLine();
                Assert.AreNotEqual(null, line);
            }
        }

        [Test]
        public void Should_use_colon_for_separator()
        {
            Writer.Write(new InvoiceAggregateRecord[0]);

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
            Writer.Write(new InvoiceAggregateRecord[0]);

            using (var reader = GetReader())
            {
                var headerLine = reader.ReadLine();
                var header = headerLine.Split(new[] { ";" }, StringSplitOptions.None);
                HeaderFieldAsserter
                    .From(header)
                    .AssertField("Dav st")
                    .AssertField("Rac dat")
                    .AssertField("Rac cas")
                    .AssertField("Rac nac")
                    .AssertField("Racst pp")
                    .AssertField("Racst en")
                    .AssertField("Racst zap")
                    .AssertField("Kupec")
                    .AssertField("Kupec id")
                    .AssertField("Rac vred")
                    .AssertField("Rac povr")
                    .AssertField("Rac plac")
                    .AssertField("Plac got")
                    .AssertField("Plac kart")
                    .AssertField("Plac ostalo")
                    .AssertField("Dav st zav")
                    .AssertField("Rac 9,5 % DDV osn")
                    .AssertField("Rac 9,5 % DDV")
                    .AssertField("Rac 22 % DDV osn")
                    .AssertField("Rac 22 % DDV")
                    .AssertField("Rac 8 % pav osn")
                    .AssertField("Rac 8 % pav")
                    .AssertField("Rac davki ostalo")
                    .AssertField("Rac oprosc")
                    .AssertField("Rac dob76a")
                    .AssertField("Rac neobd")
                    .AssertField("Rac poseb")
                    .AssertField("Oper oznaka")
                    .AssertField("Oper dav st")
                    .AssertField("Zoi")
                    .AssertField("Eor")
                    .AssertField("Eor nakn")
                    .AssertField("Sprem racst pp")
                    .AssertField("Sprem racst en")
                    .AssertField("Sprem racst zap")
                    .AssertField("Sprem rac dat")
                    .AssertField("Sprem rac cas")
                    .AssertField("Sprem vkr st")
                    .AssertField("Sprem vkr set")
                    .AssertField("Sprem vkr ser")
                    .AssertField("Sprem vkr dat")
                    .AssertField("Sprem nep dat")
                    .AssertField("Sprem nep cas")
                    .AssertField("Sprem nep st")
                    .AssertField("Rac opombe");
            }
        }

        private class HeaderFieldAsserter
        {
            private readonly string[] _headers;
            private readonly int _index;

            private HeaderFieldAsserter(string[] headers, int index)
            {
                _headers = headers;
                _index = index;
            }

            public static HeaderFieldAsserter From(string[] headers)
            {
                return new HeaderFieldAsserter(headers, 0);
            }

            public HeaderFieldAsserter AssertField(string title)
            {
                Assert.AreEqual(title, _headers[_index], "Expected \"" + title + "\" at index " + 0 + ", got \"" + _headers[_index] + "\" instead.");
                return new HeaderFieldAsserter(_headers, _index + 1);
            }
        }

        [Test]
        public void Should_write_header_and_one_record()
        {
            Writer.Write(new List<InvoiceAggregateRecord> { new InvoiceAggregateRecord() });

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
            Writer.Write(new List<InvoiceAggregateRecord> { new InvoiceAggregateRecord(), new InvoiceAggregateRecord() });

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
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.DavSt, 0);
            }
        }

        [Test]
        public void Should_write_RacDat()
        {
            var record = new InvoiceAggregateRecord { RacDat = new DateTime(2013, 6, 25, 18, 07, 12) };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("20130625", 1);
            }
        }

        [Test]
        public void Should_write_RacCas()
        {
            var record = new InvoiceAggregateRecord { RacDat = new DateTime(2013, 6, 25, 18, 07, 12) };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("18:07:12", 2);
            }
        }

        [Test]
        public void Should_write_RacNac()
        {
            var record = new InvoiceAggregateRecord { RacNac = "B" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.RacNac, 3);
            }
        }

        [Test]
        public void Should_write_RacStPp()
        {
            var record = new InvoiceAggregateRecord { RacStPp = "ZOC" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.RacStPp, 4);
            }
        }

        [Test]
        public void Should_write_RacStEn()
        {
            var record = new InvoiceAggregateRecord { RacStEn = "BL1" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.RacStEn, 5);
            }
        }

        [Test]
        public void Should_write_RacStZap()
        {
            var record = new InvoiceAggregateRecord { RacStZap = "16008529" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.RacStZap, 6);
            }
        }

        [Test]
        public void Should_write_Kupec()
        {
            var record = new InvoiceAggregateRecord { Kupec = "John Doe" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.Kupec, 7);
            }
        }

        [Test]
        public void Should_write_Kupec_with_trim()
        {
            var record = new InvoiceAggregateRecord { Kupec = "John Doe 01234567890123456789012345678901234567890123456789" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.Kupec.Substring(0, 50), 7);
            }
        }

        [Test]
        public void Should_write_KupecId()
        {
            var record = new InvoiceAggregateRecord { KupecId = "12345678" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue(record.KupecId, 8);
            }
        }

        [Test]
        public void Should_write_RacVred()
        {
            var record = new InvoiceAggregateRecord { RacVred = 987654.67m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("987654,67", 9);
            }
        }

        [Test]
        public void Should_write_RacPovr()
        {
            var record = new InvoiceAggregateRecord { RacPovr = 1.17m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("1,17", 10);
            }
        }

        [Test]
        public void Should_write_RacPlac()
        {
            var record = new InvoiceAggregateRecord { RacPlac = 1333.99m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("1333,99", 11);
            }
        }

        [Test]
        public void Should_write_RacVred_with_correct_form_when_no_decimals_given()
        {
            var record = new InvoiceAggregateRecord { RacVred = 125m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("125,00", 9);
            }
        }

        [Test]
        public void Should_write_negative_RacVred()
        {
            var record = new InvoiceAggregateRecord { RacVred = -987654.67m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("-987654,67", 9);
            }
        }

        [Test]
        public void Should_write_PlacGot()
        {
            var record = new InvoiceAggregateRecord { PlacGot = 1333.99m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("1333,99", 12);
            }
        }

        [Test]
        public void Should_write_PlacKart()
        {
            var record = new InvoiceAggregateRecord { PlacKart = 1333.99m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("1333,99", 13);
            }
        }

        [Test]
        public void Should_write_PlacOstalo()
        {
            var record = new InvoiceAggregateRecord { PlacOstalo = 1333.99m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("1333,99", 14);
            }
        }

        [Test]
        public void Should_write_DavStZav()
        {
            var record = new InvoiceAggregateRecord { DavStZav = "87654321" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("87654321", 15);
            }
        }

        [Test]
        public void Should_write_Rac95Osn()
        {
            var record = new InvoiceAggregateRecord { Rac95Osn = 12.55m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("12,55", 16);
            }
        }

        [Test]
        public void Should_write_Rac95()
        {
            var record = new InvoiceAggregateRecord { Rac95 = 13.55m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("13,55", 17);
            }
        }

        [Test]
        public void Should_write_Rac95_with_0_value()
        {
            var record = new InvoiceAggregateRecord { Rac95 = 0m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("0,00", 17);
            }
        }

        [Test]
        public void Should_write_Rac22Osn()
        {
            var record = new InvoiceAggregateRecord { Rac22Osn = 12.55m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("12,55", 18);
            }
        }

        [Test]
        public void Should_write_Rac22()
        {
            var record = new InvoiceAggregateRecord { Rac22 = 13.55m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("13,55", 19);
            }
        }

        [Test]
        public void Should_write_Rac8PavOsn()
        {
            var record = new InvoiceAggregateRecord { Rac8PavOsn = 12.55m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("12,55", 20);
            }
        }

        [Test]
        public void Should_write_Rac8Pav()
        {
            var record = new InvoiceAggregateRecord { Rac8Pav = 13.55m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("13,55", 21);
            }
        }

        [Test]
        public void Should_write_RacDavkiOstalo()
        {
            var record = new InvoiceAggregateRecord { RacDavkiOstalo = 120.77m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("120,77", 22);
            }
        }

        [Test]
        public void Should_write_RacOprosc()
        {
            var record = new InvoiceAggregateRecord { RacOprosc = 120.77m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("120,77", 23);
            }
        }

        [Test]
        public void Should_write_RacDob76A()
        {
            var record = new InvoiceAggregateRecord { RacDob76A = 120.77m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("120,77", 24);
            }
        }

        [Test]
        public void Should_write_RacNeobd()
        {
            var record = new InvoiceAggregateRecord { RacNeobd = 120.77m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("120,77", 25);
            }
        }

        [Test]
        public void Should_write_RacPoseb()
        {
            var record = new InvoiceAggregateRecord { RacPoseb = 120.77m };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("120,77", 26);
            }
        }

        [Test]
        public void Should_write_OperOznaka()
        {
            var record = new InvoiceAggregateRecord { OperOznaka = "JLO"};
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("JLO", 27);
            }
        }

        [Test]
        public void Should_write_OperDavSt()
        {
            var record = new InvoiceAggregateRecord { OperDavSt = "10345678"};
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("10345678", 28);
            }
        }

        [Test]
        public void Should_write_Zoi()
        {
            var record = new InvoiceAggregateRecord { Zoi = "01234567890123456789012345678901"};
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("01234567890123456789012345678901", 29);
            }
        }

        [Test]
        public void Should_write_Eor()
        {
            var record = new InvoiceAggregateRecord { Eor = "012345678901234567890123456789012345"};
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("012345678901234567890123456789012345", 30);
            }
        }

        [Test]
        public void Should_write_EorNakn()
        {
            var record = new InvoiceAggregateRecord { EorNakn = "012345678901234567890123456789012345"};
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("012345678901234567890123456789012345", 31);
            }
        }

        [Test]
        public void Should_write_SpremRacStPp()
        {
            var record = new InvoiceAggregateRecord { SpremRacStPp = "ZOT" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("ZOT", 32);
            }
        }

        [Test]
        public void Should_write_SpremRacStEn()
        {
            var record = new InvoiceAggregateRecord { SpremRacStEn = "BL2" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("BL2", 33);
            }
        }

        [Test]
        public void Should_write_SpremRacStZap()
        {
            var record = new InvoiceAggregateRecord { SpremRacStZap = "16" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("16", 34);
            }
        }

        [Test]
        public void Should_write_SpremRacDat()
        {
            var record = new InvoiceAggregateRecord { SpremRacDat = new DateTime(2016, 9, 12, 19, 19, 23) };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("20160912", 35);
            }
        }

        [Test]
        public void Should_write_SpremRacCas()
        {
            var record = new InvoiceAggregateRecord { SpremRacDat = new DateTime(2016, 9, 12, 19, 19, 23) };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("19:19:23", 36);
            }
        }

        [Test]
        public void Should_write_SpremVkrSt()
        {
            var record = new InvoiceAggregateRecord { SpremVkrSt = "1" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("1", 37);
            }
        }

        [Test]
        public void Should_write_SpremVkrSet()
        {
            var record = new InvoiceAggregateRecord { SpremVkrSet = "2" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("2", 38);
            }
        }

        [Test]
        public void Should_write_SpremVkrSer()
        {
            var record = new InvoiceAggregateRecord { SpremVkrSer = "3" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("3", 39);
            }
        }

        [Test]
        public void Should_write_SpremVkrDat()
        {
            var record = new InvoiceAggregateRecord { SpremVkrDat = new DateTime(2013, 6, 25, 18, 07, 12) };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("20130625", 40);
            }
        }

        [Test]
        public void Should_write_SpremNepDat()
        {
            var record = new InvoiceAggregateRecord { SpremNepDat = new DateTime(2014, 7, 26, 19, 08, 13) };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("20140726", 41);
            }
        }

        [Test]
        public void Should_write_SpremNepCas()
        {
            var record = new InvoiceAggregateRecord { SpremNepDat = new DateTime(2014, 7, 26, 19, 08, 13) };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("19:08:13", 42);
            }
        }

        [Test]
        public void Should_write_SpremNepSt()
        {
            var record = new InvoiceAggregateRecord { SpremNepSt = 4 };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("4", 43);
            }
        }

        [Test]
        public void Should_write_RacOpombe()
        {
            var record = new InvoiceAggregateRecord { RacOpombe = "opombe" };
            Writer.Write(new List<InvoiceAggregateRecord> { record });

            using (var reader = GetReader())
            {
                reader.AssertFieldValue("opombe", 44);
            }
        }

        //[Test]
        //public void Should_write_SpremSt()
        //{
        //    var record = new InvoiceAggregateRecord { SpremSt = 123 };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue("123", 16);
        //    }
        //}

        //[Test]
        //public void Should_write_SpremId_Storno()
        //{
        //    var record = new InvoiceAggregateRecord { SpremId = InvoiceChangeType.Storno };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue("S", 17);
        //    }
        //}

        //[Test]
        //public void Should_write_SpremId_Dobropis()
        //{
        //    var record = new InvoiceAggregateRecord { SpremId = InvoiceChangeType.Dobropis };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue("D", 17);
        //    }
        //}

        //[Test]
        //public void Should_write_SpremId_Ostalo()
        //{
        //    var record = new InvoiceAggregateRecord { SpremId = InvoiceChangeType.Ostalo };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue("O", 17);
        //    }
        //}

        //[Test]
        //public void Should_write_SpremRazlog()
        //{
        //    var record = new InvoiceAggregateRecord { SpremRazlog = "some reason" };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue(record.SpremRazlog, 18);
        //    }
        //}

        //[Test]
        //public void Should_write_SpremUpor()
        //{
        //    var record = new InvoiceAggregateRecord { SpremUpor = "some user" };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue(record.SpremUpor, 19);
        //    }
        //}

        //[Test]
        //public void Should_write_SpremOseba()
        //{
        //    var record = new InvoiceAggregateRecord { SpremOseba = "John 1 Doe" };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue(record.SpremOseba, 20);
        //    }
        //}

        //[Test]
        //public void Should_write_RacOpombe()
        //{
        //    var record = new InvoiceAggregateRecord { RacOpombe = "some notes" };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue(record.RacOpombe, 21);
        //    }
        //}

        //[Test]
        //public void Should_write_separator_after_last_header_field()
        //{
        //    Writer.Write(new InvoiceAggregateRecord[0]);

        //    using (var reader = GetReader())
        //    {
        //        var line = reader.ReadLine();
        //        Assert.AreEqual(';', line[line.Length - 1]);
        //    }
        //}

        //[Test]
        //public void Should_write_separator_after_last_field()
        //{
        //    var record = new InvoiceAggregateRecord { RacOpombe = "some notes" };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        //header
        //        reader.ReadLine();
        //        var line = reader.ReadLine();
        //        Assert.AreEqual(';', line[line.Length - 1]);
        //    }
        //}

        //[Test]
        //public void Should_write_empty_string_for_SpremUra_when_SpremDat_null()
        //{
        //    var record = new InvoiceAggregateRecord { SpremDat = null, SpremId = InvoiceChangeType.Storno };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue(string.Empty, 15);
        //    }
        //}

        //[Test]
        //public void Should_write_empty_string_for_SpremDat_when_SpremDat_null()
        //{
        //    var record = new InvoiceAggregateRecord { SpremDat = null };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue(string.Empty, 14);
        //    }
        //}

        //[Test]
        //public void Should_write_empty_string_for_SpremId_when_SpremId_null()
        //{
        //    var record = new InvoiceAggregateRecord { SpremId = null };
        //    Writer.Write(new List<InvoiceAggregateRecord> { record });

        //    using (var reader = GetReader())
        //    {
        //        reader.AssertFieldValue(string.Empty, 17);
        //    }
        //}
    }
}
