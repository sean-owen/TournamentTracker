﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerBLP.DataAccess.TextConnectorExtensions;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnection.ExtensionMethods
{
    public static class TeamExtensions
    {
        /// <summary>
        /// Converts a list of string to a list of Team.
        /// </summary>
        /// <param name="lines">List of strings to be converted to a list of Team.</param>
        /// <returns>List of Team.</returns>
        public static List<Team> ConvertToTeams(this List<string> lines)
        {
            List<Team> teamList = new List<Team>();
            List<Person> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPeople();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Team team = new Team();
                team.Id = int.Parse(columns[ColumnFormats.Team.Id]);
                team.TeamName = columns[ColumnFormats.Team.TeamName];

                team.TeamMembers = new List<Person>();
                string[] personIds = columns[ColumnFormats.Team.TeamMembersIds].Split('|');
                foreach (var id in personIds)
                {
                    int parsedId = int.Parse(id);
                    team.TeamMembers.Add(people.First(x => x.Id == parsedId));
                }

                teamList.Add(team);
            }

            return teamList;
        }

        /// <summary>
        /// Saves a list of Team to a text file.
        /// </summary>
        /// <param name="teamList">List of Team to be saved to a text file.</param>
        public static void SaveToTeamsFile(this List<Team> teamList)
        {
            List<string> lines = new List<string>();

            foreach (Team team in teamList)
            {
                StringBuilder personIds = new StringBuilder();
                foreach (Person person in team.TeamMembers)
                {
                    personIds.Append($"{person.Id}|");
                }
                personIds.Remove((personIds.Length - 1), 1);

                lines.Add($"{ team.Id },{ personIds },{ team.TeamName }");
            }

            File.WriteAllLines(GlobalConfig.TeamsFile.FullFilePath(), lines);
        }
    }
}
