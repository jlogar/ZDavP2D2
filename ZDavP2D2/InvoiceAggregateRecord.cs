using System;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ZDavP2D2
{
    /// <summary>
    /// "Zbirni podatki" as defined at 2.1 of http://www.uradni-list.si/files/RS_-2013-035-01312-OB~P001-0000.PDF#!/pdf
    /// </summary>
    public class InvoiceAggregateRecord
    {
        public string DavSt { get; set; }
        public string RacSt { get; set; }
        public DateTime RacDat { get; set; }
        public TimeSpan RacUra { get { return RacDat.TimeOfDay; } }
        public string PeId { get; set; }
        public string BlagId { get; set; }
        public string Kupec { get; set; }
        public string IsZaDdv { get; set; }
        public decimal RacZnesek { get; set; }
        public decimal Rac85Ddv { get; set; }
        public decimal Rac20Ddv { get; set; }
        public decimal PlacGot { get; set; }
        public decimal PlacKart { get; set; }
        public decimal PlacOstalo { get; set; }
        public DateTime SpremDat { get; set; }
        public TimeSpan SpremUra { get { return SpremDat.TimeOfDay; } }
        public int SpremSt { get; set; }
        public InvoiceChangeType? SpremId { get; set; }
        public string SpremRazlog { get; set; }
        public string SpremUpor { get; set; }
        public string SpremOseba { get; set; }
        public string RacOpombe { get; set; }
    }

    public class InvoiceAggregateRecordMapping : CsvClassMap<InvoiceAggregateRecord>
    {
        public override void CreateMap()
        {
            Map(m => m.DavSt).Name("Dav št").Index(0);
            Map(m => m.RacSt).Name("Rac st").Index(1);
            Map(m => m.RacDat).Name("Rac dat").Index(2).TypeConverter<DateTimeConverter>().TypeConverterOption("ddMMyyyy");
            Map(m => m.RacUra).Name("Rac ura").Index(3).TypeConverter<TimeSpanConverter>().TypeConverterOption("hh:mm");
            Map(m => m.PeId).Name("PE id").Index(4);
            Map(m => m.BlagId).Name("Blag id").Index(5);
            Map(m => m.Kupec).Name("Kupec").Index(6);
            Map(m => m.IsZaDdv).Name("IŠ za DDV").Index(7);
            Map(m => m.RacZnesek).Name("Rac znesek").Index(8);
            Map(m => m.Rac85Ddv).Name("Rac 8,5 % DDV").Index(9);
            Map(m => m.Rac20Ddv).Name("Rac 20 % DDV").Index(10);
            Map(m => m.PlacGot).Name("Plac got").Index(11);
            Map(m => m.PlacKart).Name("Plac kart").Index(12);
            Map(m => m.PlacOstalo).Name("Plac ostalo").Index(13);
            Map(m => m.SpremDat).Name("Sprem dat").Index(14).TypeConverter<DateTimeConverter>().TypeConverterOption("ddMMyyyy");
            Map(m => m.SpremUra).Name("Sprem ura").Index(15).TypeConverter<TimeSpanConverter>().TypeConverterOption("hh:mm");
            Map(m => m.SpremSt).Name("Sprem st").Index(16);
            Map(m => m.SpremId).Name("Sprem id").Index(17).TypeConverter<SpremIdConverter>();
            Map(m => m.SpremRazlog).Name("Sprem razlog").Index(18);
            Map(m => m.SpremUpor).Name("Sprem upor").Index(19);
            Map(m => m.SpremOseba).Name("Sprem oseba").Index(20);
            Map(m => m.RacOpombe).Name("Rac opombe").Index(21);
        }
    }

    public class SpremIdConverter : ITypeConverter
    {
        public string ConvertToString(TypeConverterOptions options, object value)
        {
            return ((InvoiceChangeType)value).ToString().Substring(0, 1);
        }

        public object ConvertFromString(TypeConverterOptions options, string text)
        {
            throw new NotImplementedException();
        }

        public bool CanConvertFrom(Type type)
        {
            throw new NotImplementedException();
        }

        public bool CanConvertTo(Type type)
        {
            // We only care about strings.
            return type == typeof(string);
        }
    }

    public class TimeSpanConverter : ITypeConverter
    {
        public string ConvertToString(TypeConverterOptions options, object value)
        {
            return ((TimeSpan)value).ToString("hh\\:mm");
        }

        public object ConvertFromString(TypeConverterOptions options, string text)
        {
            throw new NotImplementedException();
        }

        public bool CanConvertFrom(Type type)
        {
            throw new NotImplementedException();
        }

        public bool CanConvertTo(Type type)
        {
            // We only care about strings.
            return type == typeof(string);
        }
    }
}
