using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrackerBLP.DataAccess.TextConnectorExtensions;
using TrackerBLP.Models;
using Xunit;

namespace UnitTest.TackerBLP
{
    public class TextConnectorProcessorTests
    {
        [Fact]
        public void SaveToTeamsFile_Valid()
        {
            //Act
            Mocks.TeamList.SaveToTeamsFile(Mocks.FileName);

            //Assert
            Assert.True(File.Exists(Mocks.FileName.FullFilePath()));


            List<string> retrievedFile = File.ReadAllLines(Mocks.FileName.FullFilePath()).ToList();
            if (!retrievedFile.Any())
            {
                Assert.True(false);
            }
            else
            {
                string[] firstLine = retrievedFile.FirstOrDefault().Split(',');
                Assert.True(int.Parse(firstLine[ColumnFormats.Team.Id]) == Mocks.Team.Id);
                Assert.True(firstLine[ColumnFormats.Team.TeamName] == Mocks.Team.TeamName);

                string[] teamMembersIds = firstLine[ColumnFormats.Team.TeamMembersIds].Split('|');
                for (int i = 0; i < teamMembersIds.Length; i++)
                {
                    Assert.True(int.Parse(teamMembersIds[i]) == Mocks.TeamList.FirstOrDefault().TeamMembers.ToArray()[i].Id);
                }
            }
        }
    }
}