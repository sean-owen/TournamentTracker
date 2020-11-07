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
        /// <summary>
        /// Converts a list of string to a list of Person.
        /// </summary>
        /// <param name="lines">List of strings to be converted to a list of Person.</param>
        /// <returns>List of Person.</returns>
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

        /// <summary>
        /// Saves a list of Person to a text file.
        /// </summary>
        /// <param name="people">List of Person to be saved to a text file.</param>
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
