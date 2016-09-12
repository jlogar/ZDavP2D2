using System;

namespace ZDavP2D2
{
    /// <summary>
    /// "Zbirni podatki" as defined at 2.1 of http://www.uradni-list.si/files/RS_-2013-035-01312-OB~P001-0000.PDF#!/pdf
    /// </summary>
    public class InvoiceAggregateRecord
    {
        public string DavSt { get; set; }
        public DateTime RacDat { get; set; }
        public TimeSpan RacCas { get { return RacDat.TimeOfDay; } }
        public string RacNac { get; set; }
        public string RacStPp { get; set; }
        public string RacStEn { get; set; }
        public string RacStZap { get; set; }
        public string Kupec { get; set; }
        public string KupecId { get; set; }
        public decimal RacVred { get; set; }
        public decimal RacPovr { get; set; }
        public decimal RacPlac { get; set; }
        public decimal PlacGot { get; set; }
        public decimal PlacKart { get; set; }
        public decimal PlacOstalo { get; set; }
        public string DavStZav { get; set; }
        public decimal Rac95Osn { get; set; }
        public decimal Rac95 { get; set; }
        public decimal Rac22Osn { get; set; }
        public decimal Rac22 { get; set; }
        public decimal Rac8PavOsn { get; set; }
        public decimal Rac8Pav { get; set; }
        public decimal RacDavkiOstalo { get; set; }
        public decimal RacOprosc { get; set; }
        public decimal RacDob76A { get; set; }
        public decimal RacNeobd { get; set; }
        public decimal RacPoseb { get; set; }
        public string OperOznaka { get; set; }
        public string OperDavSt { get; set; }
        public string Zoi { get; set; }
        public string Eor { get; set; }
        public string EorNakn { get; set; }
        public string SpremRacStPp { get; set; }
        public string SpremRacStEn { get; set; }
        public string SpremRacStZap { get; set; }
        public DateTime? SpremRacDat { get; set; }
        public TimeSpan? SpremRacCas { get { return SpremRacDat.HasValue ? SpremRacDat.Value.TimeOfDay : (TimeSpan?)null; } }
        public string SpremVkrSt { get; set; }
        public string SpremVkrSet { get; set; }
        public string SpremVkrSer { get; set; }
        public DateTime? SpremVkrDat { get; set; }
        public DateTime? SpremNepDat { get; set; }
        public TimeSpan? SpremNepCas { get { return SpremNepDat.HasValue ? SpremNepDat.Value.TimeOfDay : (TimeSpan?)null; } }
        public int? SpremNepSt { get; set; }
        public string RacOpombe { get; set; }
        public string DelimiterAfterLastField { get; set; }
    }
}
