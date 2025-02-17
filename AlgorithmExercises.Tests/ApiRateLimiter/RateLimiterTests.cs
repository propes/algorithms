using AlgorithmExercises.ApiRateLimiter;

namespace AlgorithmExercises.Tests.ApiRateLimiter;

public class RateLimiterTests
{
    [Fact]
    public void FirstTimestampPasses()
    {
        var sut = new RateLimiter();

        var results = sut.ProcessRequests([("Bill", new DateTime(2025, 02, 16, 17, 39, 00))]);

        results.Should().BeEquivalentTo([true]);
    }

    [Fact]
    public void PassesWhenRequestsAreLessThanOrEqualToLimitCount()
    {
        var sut = new RateLimiter(requestLimit: 2);

        var results = sut.ProcessRequests(
            [
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 00)),
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 01)),
            ]
        );

        results.Should().BeEquivalentTo([true, true]);
    }

    [Fact]
    public void PassesWhenRequestsAreMoreThanLimitCountOutsideWindow()
    {
        var sut = new RateLimiter(requestLimit: 1, windowInSeconds: 30);

        var results = sut.ProcessRequests(
            [
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 00)),
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 31)),
            ]
        );

        results.Should().BeEquivalentTo([true, true]);
    }

    [Fact]
    public void FailsWhenRequestsExceedLimit()
    {
        var sut = new RateLimiter(requestLimit: 1, windowInSeconds: 30);

        var results = sut.ProcessRequests(
            [
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 00)),
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 30)),
            ]
        );

        results.Should().BeEquivalentTo([true, false]);
    }

    [Fact]
    public void MoreComplexTest()
    {
        var sut = new RateLimiter(requestLimit: 1, windowInSeconds: 30);

        var results = sut.ProcessRequests(
            [
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 00)),
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 30)),
                ("Bill", new DateTime(2025, 02, 16, 17, 40, 01)),
            ]
        );

        results.Should().BeEquivalentTo([true, false, true]);
    }

    [Fact]
    public void MoreComplexTest2()
    {
        var sut = new RateLimiter(requestLimit: 3, windowInSeconds: 30);

        var results = sut.ProcessRequests(
            [
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 00)),
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 01)),
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 02)),
            ]
        );

        results.Should().BeEquivalentTo([true, true, true]);
    }

    [Fact]
    public void MoreComplexTest3()
    {
        var sut = new RateLimiter(requestLimit: 3, windowInSeconds: 30);

        var results = sut.ProcessRequests(
            [
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 00)),
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 01)),
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 02)),
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 04)),
                ("Bill", new DateTime(2025, 02, 16, 17, 39, 32)),
            ]
        );

        results.Should().BeEquivalentTo([true, true, true, false, true]);
    }
}
