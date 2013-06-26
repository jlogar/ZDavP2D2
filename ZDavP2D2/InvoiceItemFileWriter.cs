using System;

namespace ZDavP2D2
{
    public class InvoiceItemFileWriter : FileWriterBase<InvoiceItemRecord, InvoiceAggregateRecordMapping>
    {
        public InvoiceItemFileWriter(String relativePath = "", String fileName = "IZPIS RAČUNI GLAVE.TXT")
            : base(relativePath, fileName)
        {
            throw new NotImplementedException();
        }
    }
}