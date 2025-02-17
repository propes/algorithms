using System.Collections.Concurrent;

namespace AlgorithmExercises.RankTeamsByVotes;

// In a special ranking system, each voter gives a rank from highest to lowest to all teams participating in the competition.
// The ordering of teams is decided by who received the most position-one votes. If two or more teams tie in the first position, we consider the second position to resolve the conflict, if they tie again, we continue this process until the ties are resolved. If two or more teams are still tied after considering all positions, we rank them alphabetically based on their team letter.
// You are given an array of strings votes which is the votes of all voters in the ranking systems. Sort all teams according to the ranking system described above.
// Return a string of all teams sorted by the ranking system.
//
// Example 1:
// Input: votes = ["ABC","ACB","ABC","ACB","ACB"]
// Output: "ACB"
public class SolutionConcurrent
{
    public string RankTeams(string[] votes)
    {
        var voteCount = CountVotes(votes);

        var sortedTeams = SortTeamsByVote(voteCount);

        return string.Join(string.Empty, sortedTeams.Select(x => x.Letter));
    }

    public Dictionary<char, Team> CountVotes(string[] votes)
    {
        var voteCount = new ConcurrentDictionary<char, Team>();

        Parallel.ForEach(
            votes,
            vote =>
            {
                var letters = vote.ToCharArray();
                for (int i = 0; i < letters.Length; i++)
                {
                    var letter = letters[i];
                    voteCount.AddOrUpdate(
                        letter,
                        _ =>
                        {
                            var team = new Team
                            {
                                Letter = letter,
                                Votes = new int[letters.Length],
                            };
                            team.Votes[i] = 1;
                            return team;
                        },
                        (_, existingTeam) =>
                        {
                            lock (existingTeam.Votes)
                            {
                                existingTeam.Votes[i]++;
                            }
                            return existingTeam;
                        }
                    );
                }
            }
        );

        return voteCount.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    public IEnumerable<Team> SortTeamsByVote(Dictionary<char, Team> voteCount)
    {
        var unsortedTeams = voteCount.Values.AsParallel();

        return unsortedTeams.OrderBy(x => x, new TeamComparer());
    }

    public class TeamComparer : IComparer<Team>
    {
        public int Compare(Team? x, Team? y)
        {
            for (int i = 0; i < x.Votes.Length; i++)
            {
                var comparisonResult = y.Votes[i].CompareTo(x.Votes[i]);
                if (comparisonResult != 0)
                {
                    return comparisonResult;
                }
            }

            return x.Letter.CompareTo(y.Letter);
        }
    }

    public class Team
    {
        public required char Letter { get; init; }
        public required int[] Votes { get; init; }
    }
}
