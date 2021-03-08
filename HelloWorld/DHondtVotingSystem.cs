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
            List<Incumbent> parties = FindData(inputFileData);

            // Begin the calculations, according to the D'Hondt Method
            var incumbent = AccumIncumbent(inputFileData);
            DhondtCalculations(parties, incumbent.Item2);
            OutputVictors(parties);
            Console.ReadKey();
        }

        private static List<Incumbent> FindData(string exactpath)
        {
            // Extract the data from the file and assort the values in to a list
            List<string> dataSource = File.ReadAllLines(exactpath).ToList();
            List<Incumbent> parties = new List<Incumbent>();
            foreach (string line in dataSource.Skip(3))
            {
                string[] dataParts = line.Split(',');
                Incumbent p = new Incumbent(dataParts[0], Convert.ToInt32(dataParts[1]), dataParts.Skip(2).ToArray());
                parties.Add(p);
            }
            return parties;
        }

        // Print out the seat winners to the console
        private static void OutputVictors(List<Incumbent> parties)
        {
            foreach (Incumbent p in parties)
            {
                if (p.SeatScore > 0)
                {
                    Console.WriteLine(p);
                }
            }
        }

        // Describe total votes for each party and calculate the number of seats to be awarded to each party
        private static (int, int) AccumIncumbent(string exactpath)
        {
            List<string> dataSource = File.ReadAllLines(exactpath).ToList();
            int totalVoteTally = Convert.ToInt32(dataSource[2]);
            int seatsAwarded = Convert.ToInt32(dataSource[1]);

            return (totalVoteTally, seatsAwarded);
        }
          
        // Function to perform the calculations according to the D'Hondt method
        private static void DhondtCalculations(List<Incumbent> parties, int seatsElected)
        {
            // Describe the first party to hold the most votes in the election
            Incumbent greatestVotes = parties.Aggregate((v1, v2) => v1.VoteTally > v2.VoteTally ? v1 : v2);
            greatestVotes.SeatScore += 1;
            greatestVotes.DhondtCalculation();

            // Continue the loop until all possible incumbents have been elected
            int totalSeatsElected = 0;
            while (totalSeatsElected != seatsElected)
            {
                // If the amount of seats has not been reached, the progrram will reset the variable
                totalSeatsElected = 0;

                Incumbent maxVotes = parties.Aggregate((v1, v2) => v1.ChangedVotes > v2.ChangedVotes ? v1 : v2);
                maxVotes.SeatScore += 1;
                maxVotes.DhondtCalculation();

                foreach (Incumbent p in parties)
                {
                    totalSeatsElected += p.SeatScore;
                }
            }
            Console.WriteLine($"\nThe {seatsElected} seats that were awarded are:");
        }
    }

    class Incumbent
    {
        // Fields
        private string partyName = "unknown";
        private int countedVotes;
        private string[] _seatsCodeValues;
        private int _changedVotes;
        private int _seatScore;

        // Applies D'Hondt method of division 
        public void DhondtCalculation()
        {
            ChangedVotes = VoteTally / (1 + SeatScore);
        }

        // Properties 
        public string PartyTitle
        {
            get { return partyName; }
            private set { partyName = value; }
        }

        public int VoteTally
        {
            get { return countedVotes; }
            private set { countedVotes = value; }
        }

        public string[] SeatsCodeValues
        {
            get { return _seatsCodeValues; }
            private set { _seatsCodeValues = value; }
        }

        public int ChangedVotes
        {
            get { return _changedVotes; }
            private set { _changedVotes = value; }
        }

        public int SeatScore
        {
            get { return _seatScore; }
            set { _seatScore = value; }
        }

        // Construct the party properties 
        public Incumbent(string name, int votes, string[] seatsCodeValues)
        {
            PartyTitle = name;
            VoteTally = votes;
            ChangedVotes = votes;
            SeatsCodeValues = seatsCodeValues;
        }

        // Returns percentage of votes for your party
        public double PercentOfVotes(double totalVotes) => (VoteTally / totalVotes) * 100;

        public override string ToString()
        {
            return $"Name: {partyName}, Votes: {VoteTally}, {SeatScore} " +
                   $"Seats Values : {string.Join(",", SeatsCodeValues.Take(SeatScore))}";
        }
              
       
    }
}