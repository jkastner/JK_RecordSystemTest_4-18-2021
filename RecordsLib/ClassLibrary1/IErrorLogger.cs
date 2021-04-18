using System;
using System.Collections.Generic;

namespace RecordsLib
{
    public interface IErrorLogger
    {
        void LogError(string txt);
    }
    public class DummyErrorLogger : IErrorLogger
    {
        public List<string> errors = new List<string>();
        public void LogError(string txt)
        {
            //Real logger needed for production - console or a logfile or something.
            //just to verify log in unit tests
            errors.Add(txt);
            

        }
    }
}