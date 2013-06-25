using System;
using System.Collections.Generic;
using System.IO;

namespace ZDavP2D2
{
    /// <summary>
    /// Writes a CSV named "IZPIS RAČUNI GLAVE.TXT"
    /// <see cref="http://www.uradni-list.si/files/RS_-2013-035-01312-OB~P001-0000.PDF#!/pdf"/>
    /// </summary>
    public class InvoiceAggregateFileWriter
    {
        private readonly string _relativePath;

        public InvoiceAggregateFileWriter(String relativePath)
        {
            _relativePath = relativePath;
        }

        public void Write(IEnumerable<InvoiceAggregateRecord> records)
        {
            using (var stream = new FileStream(_relativePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream))
            //using (var csvHelper = null)
            {
                foreach (var record in records)
                {
                    Console.WriteLine(record);
                }
            }
        }
    }
}
