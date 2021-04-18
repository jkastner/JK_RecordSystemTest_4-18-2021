using RecordsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecordsAPI.Models
{
    public class LocalRecordsModel : IRecordsModel
    {
        public BaseRecordSet RecordSet { get; }
        private RecordParser parser;
        public LocalRecordsModel()
        {
            this.RecordSet = new LocalRecordSet("DataCSV.txt");
            this.RecordSet.InitializeRecords();
            this.parser = new RecordParser(new DummyErrorLogger());
        }

        
        public bool AddRecordFromString(string info)
        {
            var newItem = this.parser.FromString(new string[] { info }).ToList();
            if (newItem.Any())
            {
                this.RecordSet.AddRecords(newItem);
                return true;
            }
            return false;
        }
    }
}
