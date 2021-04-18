using NUnit.Framework;
using RecordsLib;
using System;
using System.IO;

namespace RecordsLibTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string fileText = this.GetTextFromFile("DataCSV.txt");
            var zzz = fileText;

        }

        private string GetTextFromFile(string filename)
        {
            string result = string.Empty;

            using (Stream stream = typeof(Record).Assembly.
                GetManifestResourceStream(filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }


    }
}