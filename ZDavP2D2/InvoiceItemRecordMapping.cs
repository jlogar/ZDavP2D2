using CsvHelper.Configuration;

namespace ZDavP2D2
{
    public class InvoiceItemRecordMapping : CsvClassMap<InvoiceItemRecord>
    {
        public override void CreateMap()
        {
            Map(m => m.DavSt).Name("Dav št");
            Map(m => m.RacSt).Name("Rac st");
            Map(m => m.RacDat).Name("Rac dat");
            Map(m => m.PeId).Name("PE id");
            Map(m => m.BlagId).Name("Blag id");
            Map(m => m.PostSt).Name("Post st");
            Map(m => m.PostId).Name("Post id");
            Map(m => m.PostOpis).Name("Post opis");
            Map(m => m.PostKol).Name("Post kol");
            Map(m => m.PostEm).Name("Post em");
            Map(m => m.PostZnesek).Name("Post znesek");
            Map(m => m.Post85Ddv).Name("Post 8,5 % DDV");
            Map(m => m.Post20Ddv).Name("Post 20 % DDV");
            Map(m => m.SpremSt).Name("Sprem st");
            Map(m => m.PostOpombe).Name("Post opombe");
        }
    }
}