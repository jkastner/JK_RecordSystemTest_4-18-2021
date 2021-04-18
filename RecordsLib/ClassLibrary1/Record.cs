using System;

namespace RecordsLib
{
    /// <summary>
    /// record for serialization - all as strings
    /// </summary>
    public class Record
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string FavoriteColor { get; set; }
        
        public DateTime DateOfBirth { get; set; }
    }
}
