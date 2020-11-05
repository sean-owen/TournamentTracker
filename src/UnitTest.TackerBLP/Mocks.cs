using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using TrackerBLP;
using TrackerBLP.Models;

namespace UnitTest.TackerBLP
{
    public static class Mocks
    {
        static Mocks()
        {
            // ensure file path for text files exists
            if (!Directory.Exists(GlobalConfig.TextFileOutputFolderPath))
            {
                Directory.CreateDirectory(GlobalConfig.TextFileOutputFolderPath);
            }

            FileName = "MockFile.csv";
            Person1 = new Person()
            {
                Id = 1,
                FirstName = "Sean",
                LastName = "Owen",
                EmailAddress = "sean.owen@mock.co.uk",
                PhoneNumber = "0800 00 1066"
            };

            Person2 = new Person()
            {
                Id = 2,
                FirstName = "Sean",
                LastName = "Owen",
                EmailAddress = "sean.owen@mock.co.uk",
                PhoneNumber = "0800 00 1066"
            };

            PersonList = new List<Person>()
            {
                Person1,
                Person2
            };

            Team = new Team()
            {
                Id = 1,
                TeamMembers = PersonList,
                TeamName = "Mock Team Name"
            };

            TeamList = new List<Team>()
            {
                Team
            };
        }

        public static List<Team> TeamList { get; set; }
        public static Team Team { get; set; }
        public static List<Person> PersonList { get; set; }
        public static Person Person1 { get; set; }
        public static Person Person2 { get; set; }
        public static string FileName { get; set; }
    }
}
