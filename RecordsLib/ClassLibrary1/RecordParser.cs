using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordsLib
{
    public class RecordParser
    {
        private readonly IErrorLogger logger;

        public RecordParser(IErrorLogger logger)
        {
            this.logger = logger;
        }

        readonly IReadOnlyList<char> allDelimiters = new List<char>()
        {
            ' ',
            '|',
            ','
        };


        public const int LastNameIndex = 0;
        public const int FirstNameIndex = 1;
        public const int GenderIndex = 2;
        public const int FavColorIndex = 3;
        public const int DOBIndex = 4;
        public const int ExpectedLen = 5;

        

        public IEnumerable<Record> FromString(string [] recordLines)
        {
            if(recordLines == null || recordLines.Length==0)
            {
                this.logger.LogError($"No record text provided");
                yield break;
            }
            var firstLine = recordLines[0];
            bool found = false;
            var delim = ' ';
            foreach(var curDelimiter in allDelimiters)
            {
                if (firstLine.Contains(curDelimiter.ToString()))
                {
                    delim = curDelimiter;
                    found = true;
                    break;
                }
            }
            if(!found)
            {
                logger.LogError("Text did not contain expected delimiters");
                yield break;
            }
            foreach(var curLine in recordLines.Where(x=>!string.IsNullOrEmpty(x)))
            {
                //assuming all lines behave after that first check
                Record ret = new Record();
                var curSplit = curLine.Split(delim);
                ret.LastName = curSplit[LastNameIndex];
                ret.FirstName = curSplit[FirstNameIndex];
                ret.Gender = curSplit[GenderIndex];
                ret.FavoriteColor = curSplit[FavColorIndex];
                ret.DateOfBirth = ParseDate(curSplit[DOBIndex]);
                yield return ret;
            }

        }

        //for now, presuming dates all come in the same format
        private DateTime ParseDate(string v)
        {
            return DateTime.Parse(v);
        }
    }
}
