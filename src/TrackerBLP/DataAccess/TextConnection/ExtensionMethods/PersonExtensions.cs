using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TrackerBLP.DataAccess.TextConnectorExtensions;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnection.ExtensionMethods
{
    public static class PersonExtensions
    {
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

        public static void SaveToPeopleFile(this List<Person> people)
        {
            List<string> lines = new List<string>();

            foreach (Person person in people)
            {
                lines.Add($"{ person.Id },{ person.FirstName },{ person.LastName },{ person.EmailAddress },{ person.PhoneNumber }");
            }

            File.WriteAllLines(GlobalConfig.PeopleFile.FullFilePath(), lines);
        }
    }
}
