using System;

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
        public int SpremSt { get; set; }
        public InvoiceChangeType SpremId { get; set; }
        public string SpremRazlog { get; set; }
        public string SpremUpor { get; set; }
        public string RacOpombe { get; set; }
    }
}
