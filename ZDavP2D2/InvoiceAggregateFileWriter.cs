using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ZDavP2D2
{
    /// <summary>
    /// Writes a CSV named "IZPIS RAČUNI GLAVE.TXT"
    /// <see cref="http://www.uradni-list.si/files/RS_-2013-035-01312-OB~P001-0000.PDF#!/pdf"/>
    /// </summary>
    public class InvoiceAggregateFileWriter
    {
        private readonly string _relativePath;
        private readonly string _fileName;
        private readonly string _path;

        public InvoiceAggregateFileWriter(String relativePath = "", String fileName = "IZPIS RAČUNI GLAVE.TXT")
        {
            _relativePath = relativePath;
            _fileName = fileName;
            _path = System.IO.Path.GetFullPath(System.IO.Path.Combine(_relativePath, _fileName));
        }

        public void Write(IEnumerable<InvoiceAggregateRecord> records)
        {
            Debug.WriteLine(string.Format("writing to {0}", _path));
            using (var stream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream, ASCIIEncoding.GetEncoding("windows-1250")))
            //using (var csvHelper = null)
            {
                foreach (var record in records)
                {
                    Console.WriteLine(record);
                }
            }
        }

        public string Path
        {
            get { return _path; }
        }
    }
}
