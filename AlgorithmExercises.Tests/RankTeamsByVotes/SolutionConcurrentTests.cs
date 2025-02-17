using AlgorithmExercises.RankTeamsByVotes;

namespace AlgorithmExercises.Tests.RankTeamsByVotes;

public class SolutionConcurrentTests
{
    [Fact]
    public void CountVotes_TestCase1()
    {
        var sut = new SolutionConcurrent();

        var result = sut.CountVotes(["ABC", "ACB", "ABC", "ACB", "ACB"]);

        result['A'].Votes.Should().BeEquivalentTo([5, 0, 0]);
        result['B'].Votes.Should().BeEquivalentTo([0, 2, 3]);
        result['C'].Votes.Should().BeEquivalentTo([0, 3, 2]);
    }

    [Fact]
    public void RankTeams_TestCase1()
    {
        var sut = new SolutionConcurrent();

        var result = sut.RankTeams(["ABC", "ACB", "ABC", "ACB", "ACB"]);

        result.Should().Be("ACB");
    }

    [Fact]
    public void RankTeams_TestCase2()
    {
        var sut = new SolutionConcurrent();

        var result = sut.RankTeams(["WXYZ", "XYZW"]);

        result.Should().Be("XWYZ");
    }
}
