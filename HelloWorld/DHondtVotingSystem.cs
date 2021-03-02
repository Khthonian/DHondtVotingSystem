using System;

namespace DHondtVotingCalculator
{
    class VoteCount
    {
        public static void Main(string[] args)
        {
            // Ask how many votes the Brexit Party won
            Console.Write("How many votes did the Brexit Party win: ");
            string rawBPVotes = Console.ReadLine();
            int BPVotes = Convert.ToInt16(rawBPVotes);
            Console.WriteLine($"The Brexit Party won {BPVotes} votes");

            // Ask how many votes the Liberal Democrats won
            Console.Write("How many votes did the Liberal Democrats win: ");
            string rawLDVotes = Console.ReadLine();
            int LDVotes = Convert.ToInt16(rawLDVotes);
            Console.WriteLine($"The Liberal Democrats won {LDVotes} votes");

            // Ask how many votes the Labour Party won
            Console.Write("How many votes did the Labour Party win: ");
            string rawLPVotes = Console.ReadLine();
            int LPVotes = Convert.ToInt16(rawLPVotes);
            Console.WriteLine($"The Labour Party won {LPVotes} votes");

            // Ask how many votes the Conservative Party won
            Console.Write("How many votes did the Conservative Party win: ");
            string rawCPVotes = Console.ReadLine();
            int CPVotes = Convert.ToInt16(rawCPVotes);
            Console.WriteLine($"The Conservative Party won {CPVotes} votes");

            // Ask how many votes the Green Party won
            Console.Write("How many votes did the Green Party win: ");
            string rawGPVotes = Console.ReadLine();
            int GPVotes = Convert.ToInt16(rawGPVotes);
            Console.WriteLine($"The Green Party won {GPVotes} votes");

            // Ask how many votes UKIP won
            Console.Write("How many votes did UKIP win: ");
            string rawUKIPVotes = Console.ReadLine();
            int UKIPVotes = Convert.ToInt16(rawUKIPVotes);
            Console.WriteLine($"UKIP won {UKIPVotes} votes");

            // Ask how many votes Change UK won
            Console.Write("How many votes did Change UK win: ");
            string rawCUKVotes = Console.ReadLine();
            int CUKVotes = Convert.ToInt16(rawCUKVotes);
            Console.WriteLine($"Change UK won {CUKVotes} votes");

            // Ask how many votes the Independent Network won
            Console.Write("How many votes did the Independent Network win: ");
            string rawIKVotes = Console.ReadLine();
            int IKVotes = Convert.ToInt16(rawIKVotes);
            Console.WriteLine($"The Independent Network won {IKVotes} votes");

            // Ask how many votes the Independents won
            Console.Write("How many votes did the Independents win: ");
            string rawIVotes = Console.ReadLine();
            int IVotes = Convert.ToInt16(rawIVotes);
            Console.WriteLine($"The Independents won {IVotes} votes");
        }
    }
}
