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
    }
}
