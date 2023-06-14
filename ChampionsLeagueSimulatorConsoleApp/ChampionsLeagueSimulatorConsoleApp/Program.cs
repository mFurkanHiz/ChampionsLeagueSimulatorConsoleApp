namespace ChampionsLeagueSimulatorConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChampionsLeagueSimulator simulator = new ChampionsLeagueSimulator();
            // Initializing teams
            simulator.InitializeTeams();
            // Drawing groups from bags
            simulator.DrawGroups();
            // Print Groups
            simulator.PrintGroupsTeams();
            // Match fixtures
            simulator.CreateFixture();
            // Print Fixtures
            simulator.PrintFixtures();
            // Print Scores
            simulator.PrintScoreList();
            // Print qualified teams
            simulator.PrintQualifyingTeams();

            Console.ReadLine();
        }
    }
}