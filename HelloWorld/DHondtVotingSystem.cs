using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace dhondtVotingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ask the user to input the file path to the chosen data packet
            Console.Write("Please input the file path to the chosen data: ");
            string inputFileData = Console.ReadLine();
            List<Party> parties = FindData(inputFileData);

            // Begin the calculations, according to the D'Hondt Method
            var incumbent = accumIncumbent(inputFileData);
            dhondtCalculations(parties, incumbent.Item2);
            outputVictors(parties);
            Console.ReadKey();
        }

        private static List<Party> FindData(string exactpath)
        {
            // Extract the data from the file and assort the values in to a list
            List<string> dataSource = File.ReadAllLines(exactpath).ToList();
            List<Party> parties = new List<Party>();
            foreach (string line in dataSource.Skip(3))
            {
                string[] dataParts = line.Split(',');
                Party p = new Party(dataParts[0], Convert.ToInt32(dataParts[1]), dataParts.Skip(2).ToArray());
                parties.Add(p);
            }
            return parties;
        }

        // Print out the seat winners to the console
        private static void outputVictors(List<Party> parties)
        {
            foreach (Party p in parties)
            {
                if (p.seatScore > 0)
                {
                    Console.WriteLine(p);
                }
            }
        }

        // Describe total votes for each party and calculate the number of seats to be awarded to each party
        private static (int, int) accumIncumbent(string exactpath)
        {
            List<string> dataSource = File.ReadAllLines(exactpath).ToList();
            int totalVoteTally = Convert.ToInt32(dataSource[2]);
            int seatsAwarded = Convert.ToInt32(dataSource[1]);

            return (totalVoteTally, seatsAwarded);
        }
          
        // Function to perform the calculations according to the D'Hondt method
        private static void dhondtCalculations(List<Party> parties, int seatsElected)
        {
            // Describe the first party to hold the most votes in the election
            Party greatestVotes = parties.Aggregate((v1, v2) => v1.voteTally > v2.voteTally ? v1 : v2);
            greatestVotes.seatScore += 1;
            greatestVotes.dhondtCalculation();

            // Continue the loop until all possible incumbents have been elected
            int totalSeatsElected = 0;
            while (totalSeatsElected != seatsElected)
            {
                // If the amount of seats has not been reached, the progrram will reset the variable
                totalSeatsElected = 0;

                Party maxVotes = parties.Aggregate((v1, v2) => v1.changedVotes > v2.changedVotes ? v1 : v2);
                maxVotes.seatScore += 1;
                maxVotes.dhondtCalculation();

                foreach (Party p in parties)
                {
                    totalSeatsElected += p.seatScore;
                }
            }
            Console.WriteLine($"\nThe {seatsElected} seats that were awarded are:");
        }
    }

    class Party
    {
        // Fields
        private string partyName = "unknown";
        private int countedVotes;
        private string[] _seatsCodeValues;
        private int _changedVotes;
        private int _seatScore;

        // Properties 
        public string partyTitle
        {
            get { return partyName; }
            private set { partyName = value; }
        }

        public int voteTally
        {
            get { return countedVotes; }
            private set { countedVotes = value; }
        }

        public string[] SeatsCodeValues
        {
            get { return _seatsCodeValues; }
            private set { _seatsCodeValues = value; }
        }

        public int changedVotes
        {
            get { return _changedVotes; }
            private set { _changedVotes = value; }
        }

        public int seatScore
        {
            get { return _seatScore; }
            set { _seatScore = value; }
        }

        // Construct the party properties 
        public Party(string name, int votes, string[] seatsCodeValues)
        {
            partyTitle = name;
            voteTally = votes;
            changedVotes = votes;
            SeatsCodeValues = seatsCodeValues;
        }

        // Returns percentage of votes for your party
        public double PercentOfVotes(double totalVotes) => (voteTally / totalVotes) * 100;

        public override string ToString()
        {
            return $"Name: {partyName}, Votes: {voteTally}, {seatScore} " +
                   $"Seats Values : {string.Join(",", SeatsCodeValues.Take(seatScore))}";
        }

        // Applies D'Hondt method of division 
        public void dhondtCalculation()
        {
            changedVotes = voteTally / (1 + seatScore);
        }
    }
}