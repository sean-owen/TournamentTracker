using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnectorExtensions
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)
        {
            //return @$"{GlobalConfig.TextFileOutputFolderPath}\{fileName}";
            return Path.Combine(GlobalConfig.TextFileOutputFolderPath, fileName);
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<Prize> ConvertToPrizes(this List<string> lines)
        {
            List<Prize> prizes = new List<Prize>();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Prize prize = new Prize();
                prize.Id = int.Parse(columns[ColumnFormats.Prize.Id]);
                prize.PlaceNumber = int.Parse(columns[ColumnFormats.Prize.PlaceNumber]);
                prize.PlaceName = columns[ColumnFormats.Prize.PlaceName];
                prize.PrizeAmount = decimal.Parse(columns[ColumnFormats.Prize.PrizeAmount]);
                prize.PrizePercentage = double.Parse(columns[ColumnFormats.Prize.PrizePercentage]);

                prizes.Add(prize);
            }

            return prizes;
        }

        public static List<Person> ConvertToPeople(this List<string> lines)
        {
            List<Person> people = new List<Person>();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Person person = new Person();
                person.Id = int.Parse(columns[ColumnFormats.Person.Id]);
                person.FirstName = columns[ColumnFormats.Person.FirstName];
                person.LastName = columns[ColumnFormats.Person.LastName];
                person.EmailAddress = columns[ColumnFormats.Person.EmailAddress];
                person.PhoneNumber = columns[ColumnFormats.Person.PhoneNumber];

                people.Add(person);
            }

            return people;
        }

        // TODO - consider writing generic convert method (?)
        public static List<Team> ConvertToTeams(this List<string> lines, string peopleFileName)
        {
            List<Team> teamList = new List<Team>();
            List<Person> people = peopleFileName.FullFilePath().LoadFile().ConvertToPeople();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Team team = new Team();
                team.Id = int.Parse(columns[ColumnFormats.Team.Id]);
                team.TeamName = columns[ColumnFormats.Team.TeamName];

                string[] personIds = columns[ColumnFormats.Team.TeamMembersIds].Split('|');

                List<Person> teamMemberList = new List<Person>();
                foreach (var id in personIds)
                {
                    int parsedId = int.Parse(id);
                    teamMemberList.Add(people.FirstOrDefault(x => x.Id == parsedId));
                }

                team.TeamMembers = teamMemberList;
                teamList.Add(team);
            }

            return teamList;
        }


        public static void SaveToPrizeFile(this List<Prize> prizes, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Prize prize in prizes)
            {
                lines.Add($"{ prize.Id },{ prize.PlaceNumber },{ prize.PlaceName },{ prize.PrizeAmount },{ prize.PrizePercentage }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPeopleFile(this List<Person> people, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Person person in people)
            {
                lines.Add($"{ person.Id },{ person.FirstName },{ person.LastName },{ person.EmailAddress },{ person.PhoneNumber }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamsFile(this List<Team> teamList, string fileName)
        {
            List<string> lines = new List<string>();
            
            foreach (Team team in teamList)
            {
                StringBuilder personIds = new StringBuilder();
                foreach (var person in team.TeamMembers)
                {
                    personIds.Append($"{person.Id}|");
                }
                personIds.Remove((personIds.Length - 1), 1);

                lines.Add($"{ team.Id },{ personIds },{ team.TeamName }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
