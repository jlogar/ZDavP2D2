using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ZDavP2D2
{
    public class InvoiceItemRecordMapping : CsvClassMap<InvoiceItemRecord>
    {
        public InvoiceItemRecordMapping()
        {
            Map(m => m.DavSt).Name("Dav st");
            Map(m => m.RacDat).Name("Rac dat").TypeConverter<DateTimeConverter>().TypeConverterOption("yyyyMMdd");
            Map(m => m.RacCas).Name("Rac cas").TypeConverter<TimeSpanConverter>().TypeConverterOption(InvoiceAggregateRecordMapping.TimeFormat);
            Map(m => m.RacStPp).Name("Racst pp");
            Map(m => m.RacStEn).Name("Racst en");
            Map(m => m.RacStZap).Name("Racst zap");
            Map(m => m.DavStZav).Name("Dav st zav");
            Map(m => m.PostId).Name("Post id");
            Map(m => m.PostOpis).Name("Post opis");
            Map(m => m.PostKol).Name("Post kol").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.PostEm).Name("Post em");
            Map(m => m.PostEmCena).Name("Post em cena").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.PostVrednost).Name("Post vrednost").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.Post95Ddv).Name("Post 9,5 % DDV").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.Post22Ddv).Name("Post 22 % DDV").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.Post8Pav).Name("Post 8 % pav").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.PostDavkiOstalo).Name("Post davki ostalo").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.PostOprosc).Name("Post oprosc").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.PostDob76A).Name("Post dob76a").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.PostNeobd).Name("Post neobd").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.PostPoseb).Name("Post poseb").TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.SpremNepSt).Name("Sprem nep st");
            Map(m => m.PostOpombe).Name("Post opombe");
            Map(m => m.DelimiterAfterLastField).Name("").ConvertUsing(x=>"");
        }
    }
}