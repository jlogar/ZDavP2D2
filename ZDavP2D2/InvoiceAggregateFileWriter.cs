using System;

namespace ZDavP2D2
{
    /// <summary>
    /// Writes a CSV named "IZPIS RAČUNI GLAVE.TXT"
    /// <see cref="http://www.uradni-list.si/files/RS_-2013-035-01312-OB~P001-0000.PDF#!/pdf"/>
    /// </summary>
    public class InvoiceAggregateFileWriter : FileWriterBase<InvoiceAggregateRecord, InvoiceAggregateRecordMapping>
    {
        public InvoiceAggregateFileWriter(String relativePath = "", String fileName = "IZPIS RAČUNI GLAVE.TXT")
            : base(relativePath, fileName)
        {
        }
    }
}
