using CsvHelper.Configuration;

namespace ZDavP2D2
{
    public class InvoiceItemRecordMapping : CsvClassMap<InvoiceItemRecord>
    {
        public override void CreateMap()
        {
            Map(m => m.DavSt);
        }
    }
}