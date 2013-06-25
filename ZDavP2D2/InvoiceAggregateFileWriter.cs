using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

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
            using (var writer = new StreamWriter(stream, Encoding.GetEncoding("windows-1250")))
            {
                var csvConfiguration = new CsvConfiguration { Delimiter = ";" };
                csvConfiguration.RegisterClassMap<InvoiceAggregateRecordMapping>();
                using (var csvHelper = new CsvWriter(writer, csvConfiguration))
                {
                    csvHelper.WriteHeader<InvoiceAggregateRecord>();
                    foreach (var record in records)
                    {
                        csvHelper.WriteRecord(record);
                    }
                }
            }
        }

        public string Path
        {
            get { return _path; }
        }
    }
}
