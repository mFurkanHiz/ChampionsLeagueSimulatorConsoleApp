namespace ChampionsLeagueSimulatorConsoleApp
{
    public class Group
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
        public List<Match> Matches { get; set; }

        public Group(string name)
        {
            Name = name;
            Teams = new List<Team>();
            Matches = new List<Match>();
        }

        public void AddTeam(Team team)
        {
            Teams.Add(team);
        }
        public void AddMatch(Match match)
        {
            Matches.Add(match);
        }
        public List<Team> GetQualifyingTeams() // Sort the teams by point, average and total goals
        {
            var sortedTeams = Teams.OrderByDescending(t => t.Points)
                                  .ThenByDescending(t => t.GoalDifference)
                                  .ThenByDescending(t => t.HomeGoals)
                                  .ToList();

            List<Team> qualifyingTeams = new List<Team>();

            if (sortedTeams.Count > 0) // First qualified team
                qualifyingTeams.Add(sortedTeams[0]);

            if (sortedTeams.Count > 1)
            {
                int i = 1;
                while (i < sortedTeams.Count && sortedTeams[i].Points == sortedTeams[0].Points
                       && sortedTeams[i].GoalDifference == sortedTeams[0].GoalDifference
                       && sortedTeams[i].HomeGoals == sortedTeams[0].HomeGoals)
                { // Very low posibility. Just in case if everything is the same for some teams
                    qualifyingTeams.Add(sortedTeams[i]);
                    i++;
                    if (qualifyingTeams.Count > 2)
                    {
                        Random randomDesignation = new Random();
                        int randomTeamDesignation1 = randomDesignation.Next(qualifyingTeams.Count);
                        int randomTeamDesignation2 = randomDesignation.Next(qualifyingTeams.Count);

                        Team temp = qualifyingTeams[randomTeamDesignation1];
                        qualifyingTeams[randomTeamDesignation1] = qualifyingTeams[randomTeamDesignation2];
                        qualifyingTeams[randomTeamDesignation2] = temp;
                    }
                }
            }
            if (qualifyingTeams.Count < 2) qualifyingTeams.Add(sortedTeams[1]); // Second qualified team
            if (qualifyingTeams.Count > 2) qualifyingTeams.RemoveRange(2, qualifyingTeams.Count - 2);
            
            return qualifyingTeams;
        }

        public void PrintTable() // Print score table
        {
            Console.WriteLine("SCORE LIST\n");
            Console.WriteLine($"Group: {Name}");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("| Team                     |  P  |  A  |  HG |  AG |");
            Console.WriteLine("----------------------------------------------------");
            foreach (Team team in Teams)
            {
                Console.WriteLine($"| {team.Name,-24} | {team.Points,3} | {team.GoalDifference,3} | {team.HomeGoals,3} | {team.AwayGoals,3} |");
                Console.WriteLine("----------------------------------------------------");
            }
            Console.WriteLine();
        }
    }
}
