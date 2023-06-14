namespace ChampionsLeagueSimulatorConsoleApp
{
    public class ChampionsLeagueSimulator
    {
        private List<Team> teams;
        private List<Group> groups;

        public ChampionsLeagueSimulator()
        {
            teams = new List<Team>();
            groups = new List<Group>();
        }

        public void DrawGroups() // Draw groups
        {
            // Create groups
            for (int i = 0; i < 8; i++)
            {
                groups.Add(new Group($"Grup {i + 1}"));
            }

            // Group the teams from their bags
            int groupIndex = 0;
            int errorIteration = 0;
            int failCounter = 0;
            while (teams.Count > 0 && errorIteration < 32)
            {

                Team team = GetRandomTeam();
                if (IsValidTeam(team, groupIndex)) // group the team
                {
                    groups[groupIndex].AddTeam(team);
                    teams.Remove(team);
                    groupIndex = (groupIndex + 1) % 8;
                    errorIteration = 0;
                }
                else // fail case for team grouping
                {
                    groupIndex = (groupIndex + 1) % 8;
                    errorIteration++;
                }
                if (errorIteration >= 32) // Infinite loop case. Refresh the program
                {
                    Console.WriteLine($"Warning: teams are regrouping ({++failCounter})");

                    teams.AddRange(groups.SelectMany(g => g.Teams)); // reset teams

                    foreach (Group group in groups)
                    {
                        group.Teams.Clear(); // clear teams in the groups
                    }

                    groupIndex = 0;
                    errorIteration = 0;
                    continue;
                }
            }
        }

        private Team GetRandomTeam()
        {
            Random random = new Random();
            int index = random.Next(0, teams.Count);
            return teams[index];
        }

        private bool IsValidTeam(Team team, int groupIndex) // Validation for grouping
        {
            foreach (Team groupTeam in groups[groupIndex].Teams)
            {
                if (team.Bag == groupTeam.Bag || team.Country == groupTeam.Country)
                    return false;
            }
            return true;
        }

        public void CreateFixture()
        {
            Random random = new Random();

            foreach (Group group in groups)
            {
                List<Team> shuffledTeams = ShuffleTeams(group.Teams);

                for (int i = 0; i < shuffledTeams.Count - 1; i++)
                {
                    Team homeTeam = shuffledTeams[i];
                    for (int j = i + 1; j < shuffledTeams.Count; j++)
                    {
                        Team awayTeam = shuffledTeams[j];
                        Match match = new Match { HomeTeam = homeTeam, AwayTeam = awayTeam };

                        // Create random score
                        int homeGoals = random.Next(0, 9);
                        int awayGoals = random.Next(0, 9);

                        match.HomeTeamScore = homeGoals;
                        match.AwayTeamScore = awayGoals;

                        // Update team statistics
                        UpdateTeamStatistics(homeTeam, homeGoals, awayGoals);
                        UpdateTeamStatistics(awayTeam, awayGoals, homeGoals);

                        group.AddMatch(match);
                    }
                }
            }
        }

        private List<Team> ShuffleTeams(List<Team> teams)
        {
            Random random = new Random();
            List<Team> shuffledList = new List<Team>(teams);
            int n = shuffledList.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Team value = shuffledList[k];
                shuffledList[k] = shuffledList[n];
                shuffledList[n] = value;
            }
            return shuffledList;
        }

        private void UpdateTeamStatistics(Team team, int goalsFor, int goalsAgainst)
        {
            team.HomeGoals += goalsFor;
            team.AwayGoals += goalsAgainst;

            if (goalsFor > goalsAgainst)
                team.Points += 3;
            else if (goalsFor == goalsAgainst)
                team.Points += 1;
        }

        public void PrintGroupsTeams() // Print groups
        {
            Console.WriteLine("\nGROUPS\n");
            foreach (Group group in groups)
            {
                Console.WriteLine(group.Name);
                Console.WriteLine("-------------------------------------------------");
                foreach (Team team in group.Teams)
                {
                    string countryStr = "(" + team.Country + ")";
                    string bagStr = "(Bag " + team.Bag + ")";
                    Console.WriteLine($"- {team.Name,-20} {countryStr,-16} - {bagStr}");
                }
                Console.WriteLine("-------------------------------------------------\n");
            }
        }
        public void PrintFixtures() // Print Fixtures
        {
            Console.WriteLine("MATCH FIXTURES\n");
            foreach (Group group in groups)
            {
                Console.WriteLine(group.Name);
                Console.WriteLine("------------------------------------------------------------");
                foreach (Match match in group.Matches)
                {
                    Console.WriteLine($"{match.HomeTeam.Name,24} {match.HomeTeamScore,2} - {match.AwayTeamScore,-2} {match.AwayTeam.Name,-24}");
                }
                Console.WriteLine("------------------------------------------------------------\n");
            }
        }
        public void PrintScoreList() // Print score list
        {
            foreach (Group group in groups)
            {
                group.PrintTable();
            }
        }
        public void PrintQualifyingTeams() // Print qualified teams
        {
            Console.WriteLine("QUALIFYING TEAMS\n");
            Console.WriteLine("-------------------------------------------");
            foreach (Group group in groups)
            {
                List<Team> qualifyingTeams = group.GetQualifyingTeams();
                foreach (Team team in qualifyingTeams)
                {
                    if (qualifyingTeams.IndexOf(team) % 2 == 0)
                    {
                        Console.WriteLine($"{group.Name,-9} {"1st Team",-11} {team.Name,-24}  ");
                    }
                    else
                    {
                        Console.WriteLine($"{group.Name,-9} {"2nd Team",-11} {team.Name,-24}  ");
                        Console.WriteLine("-------------------------------------------");
                    }
                }
            }
        }
        public void InitializeTeams() // Initialize teams
        {
            // Bag 1
            teams.Add(new Team { Name = "Bayern Munich", Country = "Almanya", Bag = 1 });
            teams.Add(new Team { Name = "Sevilla", Country = "İspanya", Bag = 1 });
            teams.Add(new Team { Name = "Real Madrid", Country = "İspanya", Bag = 1 });
            teams.Add(new Team { Name = "Liverpool", Country = "İngiltere", Bag = 1 });
            teams.Add(new Team { Name = "Juventus", Country = "İtalya", Bag = 1 });
            teams.Add(new Team { Name = "Paris Saint-Germain", Country = "Fransa", Bag = 1 });
            teams.Add(new Team { Name = "Zenit", Country = "Rusya", Bag = 1 });
            teams.Add(new Team { Name = "Porto", Country = "Portekiz", Bag = 1 });

            // Bag 2
            teams.Add(new Team { Name = "Barcelona", Country = "İspanya", Bag = 2 });
            teams.Add(new Team { Name = "Atlético Madrid", Country = "İspanya", Bag = 2 });
            teams.Add(new Team { Name = "Manchester City", Country = "İngiltere", Bag = 2 });
            teams.Add(new Team { Name = "Manchester United", Country = "İngiltere", Bag = 2 });
            teams.Add(new Team { Name = "Borussia Dortmund", Country = "Almanya", Bag = 2 });
            teams.Add(new Team { Name = "Shakhtar Donetsk", Country = "Ukrayna", Bag = 2 });
            teams.Add(new Team { Name = "Chelsea", Country = "İngiltere", Bag = 2 });
            teams.Add(new Team { Name = "Ajax", Country = "Hollanda", Bag = 2 });

            // Bag 3
            teams.Add(new Team { Name = "Dynamo Kiev", Country = "Ukrayna", Bag = 3 });
            teams.Add(new Team { Name = "Red Bull Salzburg", Country = "Almanya", Bag = 3 });
            teams.Add(new Team { Name = "RB Leipzig", Country = "Almanya", Bag = 3 });
            teams.Add(new Team { Name = "Internazionale", Country = "İtalya", Bag = 3 });
            teams.Add(new Team { Name = "Olympiacos", Country = "Yunanistan", Bag = 3 });
            teams.Add(new Team { Name = "Lazio", Country = "İtalya", Bag = 3 });
            teams.Add(new Team { Name = "Krasnodar", Country = "Rusya", Bag = 3 });
            teams.Add(new Team { Name = "Atalanta", Country = "İtalya", Bag = 3 });

            // Bag 4
            teams.Add(new Team { Name = "Lokomotiv Moskova", Country = "Rusya", Bag = 4 });
            teams.Add(new Team { Name = "Marseille", Country = "Fransa", Bag = 4 });
            teams.Add(new Team { Name = "Club Brugge", Country = "Belçika", Bag = 4 });
            teams.Add(new Team { Name = "Bor. Mönchengladbach", Country = "Almanya", Bag = 4 });
            teams.Add(new Team { Name = "Galatasaray", Country = "Türkiye", Bag = 4 });
            teams.Add(new Team { Name = "Midtjylland", Country = "Danimarka", Bag = 4 });
            teams.Add(new Team { Name = "Rennes", Country = "Fransa", Bag = 4 });
            teams.Add(new Team { Name = "Ferencváros", Country = "Macaristan", Bag = 4 });
        }
    }
}
