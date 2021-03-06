using FluentAssertions;
using NUnit.Framework;
using RecordsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
 using System.Globalization;

namespace RecordsLibTests
{
    public class RecordTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("DataCSV.txt")]
        [TestCase("DataPipe.txt")]
        [TestCase("DataSpace.txt")]
        public void TestParsing(string target)
        {
            LocalRecordSet set = new LocalRecordSet(target);
            set.InitializeRecords();
            VerifyRecord(set.Data);
        }

        [TestCase(null)]
        [TestCase("Smith,Bob,Female,Orange,NotADate")]
        [TestCase("Bad,Length,Orange,1-1-1984")]
        [TestCase("Smith,Bob,Female,Orange,1-1-1984,ExtraStuff")]
        public void TestBadData(string data)
        {
            RecordParser rp = new RecordParser(new DummyErrorLogger());
            var items = rp.FromString(new string[] { data });
            items.Count().Should().Be(0);
        }   
        
        [TestCase("Smith,Bob,Female,Orange,1-1-1984")]
        public void TestGoodData(string data)
        {
            RecordParser rp = new RecordParser(new DummyErrorLogger());
            var items = rp.FromString(new string[] { data });
            items.Count().Should().Be(1);
        }

        private void VerifyRecord(IEnumerable<IRecord> recList)
        {
            var recs = recList.ToList();

            recs.Count.Should().Be(14);
            recs[0].FirstName.Should().Be("Mykhaylo");
            recs[0].LastName.Should().Be("Shwetz");
            recs[0].Gender.Should().Be("Male");
            recs[0].FavoriteColor.Should().Be("Red");
            recs[0].DateOfBirth.Value.ToString("yyyy-MM-dd").Should().Be("1952-03-04");
               
            recs[13].FirstName.Should().Be("Dominic");
            recs[13].LastName.Should().Be("Leavitt");
            recs[13].Gender.Should().Be("Male");
            recs[13].FavoriteColor.Should().Be("NeonGreen");
            recs[13].DateOfBirth.Value.ToString("yyyy-MM-dd").Should().Be("2023-12-08");

            recs.Count(x => string.IsNullOrEmpty(x.FirstName)).Should().Be(0);
            recs.Count(x => string.IsNullOrEmpty(x.LastName)).Should().Be(1);
            recs.Count(x => string.IsNullOrEmpty(x.Gender)).Should().Be(0);
            recs.Count(x => string.IsNullOrEmpty(x.FavoriteColor)).Should().Be(0);
            recs.Count(x => x.DateOfBirth==DateTime.MinValue).Should().Be(0);

            HashSet<string> uniqueness = new HashSet<string>();

            foreach(var curRecord in recs)
            {
                uniqueness.Should().NotContain(curRecord.FirstName);
                uniqueness.Add(curRecord.FirstName);

                uniqueness.Should().NotContain(curRecord.LastName);
                uniqueness.Add(curRecord.LastName);

                uniqueness.Should().NotContain(curRecord.FavoriteColor);
                uniqueness.Add(curRecord.FavoriteColor);

                uniqueness.Should().NotContain(curRecord.DateOfBirth.Value.ToString("yyyy-mm-dd"));
                uniqueness.Add(curRecord.DateOfBirth.Value.ToString("yyyy-mm-dd"));
            }
        }

       

        [Test]
        public void TestSorting()
        {
            LocalRecordSet rs = new LocalRecordSet("DataCSV.txt");
            rs.InitializeRecords();

            var byName = rs.ByLastName().ToList();
            byName.First().LastName.Should().Be("Upton");
            byName[12].LastName.Should().Be("Anson");
            byName.Last().LastName.Should().Be("");

            var byBirth = rs.ByBirthDate().ToList();
            byBirth.First().DateOfBirth.Should().Be(DateTime.Parse("1951-03-23"));
            byBirth.Last().DateOfBirth.Should().Be(DateTime.Parse("2034-01-05"));

            var byGender = rs.ByGender().ToList();
            byGender.First().LastName.Should().Be("");
            byGender.Last().LastName.Should().Be("Shwetz");

        }

    }
}