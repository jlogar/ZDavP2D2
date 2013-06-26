using System.IO;
using NUnit.Framework;

namespace ZDavP2D2.Tests
{
    [TestFixture]
    public class InvoiceItemFileWriterTest
    {
        private InvoiceItemFileWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _writer = new InvoiceItemFileWriter();
        }

        [Test]
        public void Should_write_to_IZPIS_RAČUNI_POSTAVKE_TXT_by_default_When_no_filename_specified()
        {
            _writer.Write(new InvoiceItemRecord[0]);

            Assert.IsTrue(File.Exists("IZPIS RAČUNI POSTAVKE.TXT"));
        }
    }
}
