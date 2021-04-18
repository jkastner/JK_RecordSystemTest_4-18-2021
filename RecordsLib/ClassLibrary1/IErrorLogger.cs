using System;

namespace RecordsLib
{
    public interface IErrorLogger
    {
        void LogError(string txt);
    }
    public class DummyErrorLogger
    {
        public void LogError(string txt)
        {
            //no op, just for compilation. Real logger needed for production - console or a logfile or something.

        }
    }
}