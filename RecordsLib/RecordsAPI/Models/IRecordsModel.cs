using RecordsLib;

namespace RecordsAPI.Models
{
    public interface IRecordsModel
    {
        BaseRecordSet RecordSet { get; }

        bool AddRecordFromString(string info);
    }
}
