using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Globalization;

namespace RecordsLib
{
    public class LocalRecordSet : BaseRecordSet
    {
        private readonly string fileName;

    
 
        public LocalRecordSet(string fileName)
        {
            this.fileName = fileName;
        }

        public override void InitializeRecords()
        {
            string fileText = this.GetTextFromFile(fileName);
            fileText = fileText.Replace('\r', '\n');
            var lines = fileText.Split('\n');
            var fileLines = lines.Where(x=>!string.IsNullOrWhiteSpace(x)).ToArray();

            RecordParser p = new RecordParser(new DummyErrorLogger());
            var csvRecords = p.FromString(fileLines).ToList();
            this.AddRecords(csvRecords);
        }


        private string GetTextFromFile(string filename)
        {
            string result = string.Empty;


            using (Stream stream = typeof(Record).Assembly.
                GetManifestResourceStream($"RecordsLib.Data.{filename}"))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }

    }
}
