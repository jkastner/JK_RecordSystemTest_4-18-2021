using System;

namespace RecordsLib
{
    public interface IRecord
    {
        string LastName { get; }
        string FirstName { get; }
        string Gender { get; }
        string FavoriteColor { get; }

        DateTime? DateOfBirth { get; }
    }

    internal class Record : IRecord
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string FavoriteColor { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
    }
}
