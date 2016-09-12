using System;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ZDavP2D2
{
    public class InvoiceAggregateRecordMapping : CsvClassMap<InvoiceAggregateRecord>
    {
        private const string TimeFormat = "hh\\:mm\\:ss";

        public InvoiceAggregateRecordMapping()
        {
            Map(m => m.DavSt).Name("Dav st").Index(0);
            Map(m => m.RacDat).Name("Rac dat").Index(1).TypeConverter<DateTimeConverter>().TypeConverterOption("yyyyMMdd");
            Map(m => m.RacCas).Name("Rac cas").Index(2).TypeConverter<TimeSpanConverter>().TypeConverterOption(TimeFormat);
            Map(m => m.RacNac).Name("Rac nac").Index(3);
            Map(m => m.RacStPp).Name("Racst pp").Index(4);
            Map(m => m.RacStEn).Name("Racst en").Index(5);
            Map(m => m.RacStZap).Name("Racst zap").Index(6);
            Map(m => m.Kupec).Name("Kupec").Index(7).TypeConverter<TrimStringConvenrter>().TypeConverterOption("50");
            Map(m => m.KupecId).Name("Kupec id").Index(8);
            Map(m => m.RacVred).Name("Rac vred").Index(9).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.RacPovr).Name("Rac povr").Index(10).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.RacPlac).Name("Rac plac").Index(11).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.PlacGot).Name("Plac got").Index(12).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.PlacKart).Name("Plac kart").Index(13).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.PlacOstalo).Name("Plac ostalo").Index(14).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.DavStZav).Name("Dav st zav").Index(15);
            Map(m => m.Rac95Osn).Name("Rac 9,5 % DDV osn").Index(16).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.Rac95).Name("Rac 9,5 % DDV").Index(17).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.Rac22Osn).Name("Rac 22 % DDV osn").Index(18).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.Rac22).Name("Rac 22 % DDV").Index(19).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.Rac8PavOsn).Name("Rac 8 % pav osn").Index(20).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.Rac8Pav).Name("Rac 8 % pav").Index(21).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.RacDavkiOstalo).Name("Rac davki ostalo").Index(22).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.RacOprosc).Name("Rac oprosc").Index(23).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.RacDob76A).Name("Rac dob76a").Index(24).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.RacNeobd).Name("Rac neobd").Index(25).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.RacPoseb).Name("Rac poseb").Index(26).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.OperOznaka).Name("Oper oznaka").Index(27).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.OperDavSt).Name("Oper dav st").Index(28).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.Zoi).Name("Zoi").Index(29).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.Eor).Name("Eor").Index(30).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.EorNakn).Name("Eor nakn").Index(31).TypeConverter<DecimalConverter>().TypeConverterOption("0.00");
            Map(m => m.SpremRacStPp).Name("Sprem racst pp").Index(32);
            Map(m => m.SpremRacStEn).Name("Sprem racst en").Index(33);
            Map(m => m.SpremRacStZap).Name("Sprem racst zap").Index(34);
            Map(m => m.SpremRacDat).Name("Sprem rac dat").Index(35).TypeConverter<DateTimeConverter>().TypeConverterOption("yyyyMMdd");
            Map(m => m.SpremRacCas).Name("Sprem rac cas").Index(36).TypeConverter<TimeSpanConverter>().TypeConverterOption(TimeFormat);
            Map(m => m.SpremVkrSt).Name("Sprem vkr st").Index(37);
            Map(m => m.SpremVkrSet).Name("Sprem vkr set").Index(38);
            Map(m => m.SpremVkrSer).Name("Sprem vkr ser").Index(39);
            Map(m => m.SpremVkrDat).Name("Sprem vkr dat").Index(40).TypeConverter<DateTimeConverter>().TypeConverterOption("yyyyMMdd");
            Map(m => m.SpremNepDat).Name("Sprem nep dat").Index(41).TypeConverter<DateTimeConverter>().TypeConverterOption("yyyyMMdd");
            Map(m => m.SpremNepCas).Name("Sprem nep cas").Index(42).TypeConverter<TimeSpanConverter>().TypeConverterOption(TimeFormat);
            Map(m => m.SpremNepSt).Name("Sprem nep st").Index(43);
            Map(m => m.RacOpombe).Name("Rac opombe").Index(44);
            Map(m => m.DelimiterAfterLastField).Index(45).Name("").ConvertUsing(x => "");
        }
    }

    public class TrimStringConvenrter : ITypeConverter
    {
        public string ConvertToString(TypeConverterOptions options, object value)
        {
            return (value == null)
                ? ""
                : ((string)value).Substring(0, Math.Min(((string)value).Length, int.Parse(options.Format)));
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