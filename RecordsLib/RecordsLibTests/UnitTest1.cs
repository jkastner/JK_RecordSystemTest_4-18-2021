using FluentAssertions;
using NUnit.Framework;
using RecordsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecordsLibTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("DataCSV.txt")]
        [TestCase("DataPipe.txt")]
        [TestCase("DataSpace.txt")]
        public void TestCSVParsing(string target)
        {
            string fileText = this.GetTextFromFile(target);
            var fileLines = fileText.Split(Environment.NewLine);

            RecordParser p = GetParser();
            var csvRecords = p.FromString(fileLines).ToList();
            VerifyRecord(csvRecords);

        }

        private void VerifyRecord(List<Record> recs)
        {
            recs.Count.Should().Be(14);
            recs[0].FirstName.Should().Be("Mykhaylo");
            recs[0].LastName.Should().Be("Shwetz");
            recs[0].Gender.Should().Be("Male");
            recs[0].FavoriteColor.Should().Be("Red");
            recs[0].DateOfBirth.Should().Be("1952-03-04");
               
            recs[13].FirstName.Should().Be("Dominic");
            recs[13].LastName.Should().Be("Leavitt");
            recs[13].Gender.Should().Be("Male");
            recs[13].FavoriteColor.Should().Be("NeonGreen");
            recs[13].DateOfBirth.Should().Be("2023-12-08");

            recs.Count(x => string.IsNullOrEmpty(x.FirstName)).Should().Be(0);
            recs.Count(x => string.IsNullOrEmpty(x.LastName)).Should().Be(1);
            recs.Count(x => string.IsNullOrEmpty(x.Gender)).Should().Be(0);
            recs.Count(x => string.IsNullOrEmpty(x.FavoriteColor)).Should().Be(0);
            recs.Count(x => string.IsNullOrEmpty(x.DateOfBirth)).Should().Be(0);

            HashSet<string> uniqueness = new HashSet<string>();

            foreach(var curRecord in recs)
            {
                uniqueness.Should().NotContain(curRecord.FirstName);
                uniqueness.Add(curRecord.FirstName);

                uniqueness.Should().NotContain(curRecord.LastName);
                uniqueness.Add(curRecord.LastName);

                uniqueness.Should().NotContain(curRecord.FavoriteColor);
                uniqueness.Add(curRecord.FavoriteColor);

                uniqueness.Should().NotContain(curRecord.DateOfBirth);
                uniqueness.Add(curRecord.DateOfBirth);
            }


        }

        private RecordParser GetParser()
        {
            return new RecordParser(new DummyErrorLogger());
        }

        private string GetTextFromFile(string filename)
        {
            string result = string.Empty;


            using (Stream stream = typeof(Record).Assembly.
                GetManifestResourceStream($"RecordsLib.Data.{filename}"))
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