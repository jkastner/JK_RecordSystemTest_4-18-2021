using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordsLib
{
    public interface IRecordSet
    {
        IEnumerable<IRecord> ByGender();
        IEnumerable<IRecord> ByBirthDate();
        IEnumerable<IRecord> ByLastName();
        IReadOnlyList<IRecord> Data { get; }
    }

    public abstract class BaseRecordSet : IRecordSet
    {
        public List<IRecord> data = new List<IRecord>();

        public IReadOnlyList<IRecord> Data => data;

        public abstract void InitializeRecords();
        
        public IEnumerable<IRecord> ByGender()
        {
            return Data.OrderBy(x => x.Gender).
                ThenBy(x=>x.LastName);
        }      
        
        public IEnumerable<IRecord> ByBirthDate()
        {
            return Data.OrderBy(x => x.DateOfBirth);
        }
        
        public IEnumerable<IRecord> ByLastName()
        {
            return Data.OrderByDescending(x => x.LastName);
        }

        public void AddRecords(IEnumerable<IRecord> records)
        {
            this.data.AddRange(records);
        }


    }


}
