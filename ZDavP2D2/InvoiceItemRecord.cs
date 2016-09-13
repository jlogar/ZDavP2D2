using System;

namespace ZDavP2D2
{
    /// <summary>
    /// "Podatki o postavkah" as defined at 2.2 of http://www.uradni-list.si/files/RS_-2016-018-00009-OB~P001-0000.PDF#!/pdf
    /// </summary>
    public class InvoiceItemRecord
    {
        public string DavSt { get; set; }
        public DateTime RacDat { get; set; }
        public TimeSpan RacCas { get { return RacDat.TimeOfDay; } }
        public string RacNac { get; set; }
        public string RacStPp { get; set; }
        public string RacStEn { get; set; }
        public int RacStZap { get; set; }
        public string DavStZav { get; set; }
        public string PostId { get; set; }
        public string PostOpis { get; set; }
        public decimal PostKol { get; set; }
        public string PostEm { get; set; }
        public decimal PostEmCena { get; set; }
        public decimal PostVrednost { get; set; }
        public decimal Post95Ddv { get; set; }
        public decimal Post22Ddv { get; set; }
        public decimal Post8Pav { get; set; }
        public decimal PostDavkiOstalo { get; set; }
        public decimal PostOprosc { get; set; }
        public decimal PostDob76A { get; set; }
        public decimal PostNeobd { get; set; }
        public decimal PostPoseb { get; set; }
        public int? SpremNepSt { get; set; }
        public string PostOpombe { get; set; }
        public string DelimiterAfterLastField { get; set; }
    }
}
