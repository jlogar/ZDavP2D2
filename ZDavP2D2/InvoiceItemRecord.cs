using System;

namespace ZDavP2D2
{
    /// <summary>
    /// "Podatki o postavkah" as defined at 2.2 of http://www.uradni-list.si/files/RS_-2013-035-01312-OB~P001-0000.PDF#!/pdf
    /// </summary>
    public class InvoiceItemRecord
    {
        public string DavSt { get; set; }
        public string RacSt { get; set; }
        public DateTime RacDat { get; set; }
        public string PeId { get; set; }
        public string BlagId { get; set; }
        public int PostSt { get; set; }
        public string PostId { get; set; }
        public string PostOpis { get; set; }
        public decimal PostKol { get; set; }
        public string PostEm { get; set; }
        public decimal PostZnesek { get; set; }
        public decimal Post85Ddv { get; set; }
        public decimal Post20Ddv { get; set; }
        public int? SpremSt { get; set; }
        public string PostOpombe { get; set; }
    }
}
