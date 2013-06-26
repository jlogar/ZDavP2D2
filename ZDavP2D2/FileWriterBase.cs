using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace ZDavP2D2
{
    public interface IHavePath
    {
        string Path { get; }
    }
    public class FileWriterBase<TRecord, TRecordMapping>: IHavePath where TRecordMapping : CsvClassMap
    {
        protected string RelativePath;
        protected string FileName;
        protected string _path;

        protected FileWriterBase(string relativePath, string fileName)
        {
            RelativePath = relativePath;
            FileName = fileName;
            _path = System.IO.Path.GetFullPath(System.IO.Path.Combine(RelativePath, FileName));
        }

        public string Path
        {
            get { return _path; }
        }

        public void Write(IEnumerable<TRecord> records)
        {
            Debug.WriteLine(string.Format("writing to {0}", _path));
            var csvConfiguration = new CsvConfiguration { Delimiter = ";" };
            csvConfiguration.RegisterClassMap<TRecordMapping>();
            using (var stream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream, Encoding.GetEncoding("windows-1250")))
            {
                using (var csvHelper = new CsvWriter(writer, csvConfiguration))
                {
                    csvHelper.WriteHeader<TRecord>();
                    foreach (var record in records)
                    {
                        csvHelper.WriteRecord(record);
                    }
                }
            }
        }
    }
}