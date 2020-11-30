using static System.Console;

namespace HashingWithStringKeys
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter the number of elements your hash table will hold: ");
            int.TryParse(ReadLine(), out int numberOfElements);
            if (numberOfElements == 0)
            {
                WriteLine("Invalid value.  Defaulting to 30.");
                numberOfElements = 30;
            }
            P260HashTable teams = new P260HashTable(numberOfElements);
            string[] teamNames = LoadTeamTable();
            string[] teamLocations = LoadTeamLocation();
            KeyAndValue team;

            //Add each entry from the parallel team name and location arrays to the dictionary
            WriteLine("Initial load of hash table");
            WriteLine("--------------------------");
            for (int i = 0; i < teamNames.Length; i++)
            {
                teams.AddItem(teamNames[i], teamLocations[i]);
            }

            //CRUD operations on items
            do
            {
                string teamName;
                string teamLocation;


                //Print option is for debug/demonstration purposes only
                Write("\n(C)reate, (R)ead, (U)pdate, (D)elete team, or (P)rint hash table state? => ");
                switch (ReadLine().ToUpper().Substring(0, 1))
                {
                    case "C":
                        Write("Enter new team name: ");
                        teamName = ReadLine();
                        Write("Enter team location: ");
                        teamLocation = ReadLine();

                        if (teams.AddItem(teamName, teamLocation))
                        {
                            WriteLine($"\nThe {teamLocation} {teamName} are now part of the NFL!");
                        }
                        else
                        {
                            WriteLine($"\n{teamLocation} {teamName} couldn't be added.  Bummer.");
                        }
                        break;
                    case "R":
                        Write("Enter team name to retrieve its location: ");
                        teamName = ReadLine();
                        team = teams.GetItem(teamName);
                        if (team != null)
                        {
                            WriteLine($"\nThe {team.Key} are in {team.Value}, at least for now.");
                        }
                        else
                        {
                            WriteLine($"Team \"{teamName}\" doesn't exist.");
                        }
                        break;
                    case "U":
                        Write("Enter team name to update: ");
                        teamName = ReadLine();

                        team = teams.GetItem(teamName);
                        if (team != null)
                        {
                            Write($"\nCurrent location is {team.Value}.  Where do you want to move them? ");
                            team = teams.UpdateItem(teamName, ReadLine());
                            WriteLine($"Okey doke.  The {team.Key} are now in {team.Value}.");
                        }
                        else
                        {
                            WriteLine($"Team \"{teamName}\" doesn't exist.");
                        }
                        break;
                    case "D":
                        Write("Enter team to delete: ");
                        teamName = ReadLine();

                        team = teams.GetItem(teamName);
                        if (team != null)
                        {
                            if (teams.DeleteItem(teamName))
                            {
                                WriteLine($"Okey doke.  The {teamName} are history.");
                            }
                            else
                            {
                                WriteLine($"Oops.  Something went wrong.");
                            }
                        }
                        else
                        {
                            WriteLine($"Team \"{teamName}\" doesn't exist.");
                        }
                        break;
                    case "P":
                        WriteLine("\nCurrent hash table state: ");
                        teams.PrintTableState();
                        break;
                    default:
                        WriteLine("Invalid command.");
                        break;
                }
                Write("\nContinue? ");


            } while (ReadLine().ToUpper().Substring(0, 1) != "N");

            WriteLine();
        }

        private static string[] LoadTeamTable()
        {
            return new string[32] {"Cardinals","Falcons","Ravens","Bills",
                "Panthers","Bears","Bengals","Browns",
                "Cowboys","Broncos","Lions","Packers",
                "Texans","Colts","Jaguars","Chiefs",
                "Chargers","Rams","Dolphins","Vikings",
                "Patriots","Saints","Giants","Jets",
                "Raiders","Eagles","Steelers","49ers",
                "Seahawks","Buccaneers","Titans","Football Team"};
        }

        private static string[] LoadTeamLocation()
        {
            return new string[32] {"Arizona","Atlanta","Baltimore","Buffalo",
            "Carolina","Chicago","Cincinnati","Cleveland",
            "Dallas","Denver","Detroit","Green Bay",
            "Houston","Indianapolis","Jacksonville","Kansas City",
            "Los Angeles","Los Angeles", "Miami","Minnesota",
            "New England","New Orleans","New York","New York",
            "Oakland","Philadelphia","Pittsburgh","San Francisco",
            "Seattle","Tampa Bay","Tennessee", "Washington"};
        }
    }
}

