using System;

namespace ZDavP2D2
{
    public class InvoiceItemFileWriter : FileWriterBase<InvoiceItemRecord, InvoiceItemRecordMapping>
    {
        public InvoiceItemFileWriter(String relativePath = "", String fileName = "IZPIS RAČUNI POSTAVKE.TXT")
            : base(relativePath, fileName)
        { }
    }
}