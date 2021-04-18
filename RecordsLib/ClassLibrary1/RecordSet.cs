using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordsLib
{
    public class RecordSet
    {
        private List<Record> data = new List<Record>();
        
        public IEnumerable<Record> ByGender()
        {
            return data.OrderBy(x => x.Gender).
                ThenBy(x=>x.LastName);
        }      
        
        public IEnumerable<Record> ByBirthDate()
        {
            return data.OrderBy(x => x.DateOfBirth);
        }
        
        public IEnumerable<Record> ByLastName()
        {
            return data.OrderByDescending(x => x.LastName);
        }

        public void Populate(List<Record> csvRecords)
        {
            this.data = csvRecords;
        }
    }
}
