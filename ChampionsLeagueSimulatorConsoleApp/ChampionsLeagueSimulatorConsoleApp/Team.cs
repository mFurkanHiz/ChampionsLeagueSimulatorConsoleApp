namespace ChampionsLeagueSimulatorConsoleApp
{
    public class Team
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int Bag { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public int Points { get; set; }
        public int GoalDifference => HomeGoals - AwayGoals;
    }
}
