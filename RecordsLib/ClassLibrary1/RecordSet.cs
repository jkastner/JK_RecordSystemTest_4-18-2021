using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordsLib
{
    public abstract class BaseRecordSet
    {
        public List<Record> data = new List<Record>();

        public IReadOnlyList<Record> Data => data;

        public abstract void InitializeRecords();
        
        public IEnumerable<Record> ByGender()
        {
            return Data.OrderBy(x => x.Gender).
                ThenBy(x=>x.LastName);
        }      
        
        public IEnumerable<Record> ByBirthDate()
        {
            return Data.OrderBy(x => x.DateOfBirth);
        }
        
        public IEnumerable<Record> ByLastName()
        {
            return Data.OrderByDescending(x => x.LastName);
        }

        public void Populate(List<Record> csvRecords)
        {
            this.data.AddRange(csvRecords);
        }
    }
}
