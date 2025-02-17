using System.Collections.Concurrent;

namespace AlgorithmExercises.ApiRateLimiter;

public class RateLimiter
{
    private readonly int _requestLimit;
    private readonly int _windowInSeconds;
    private readonly ConcurrentDictionary<string, (Queue<DateTime> queue, object lockObj)> _userMap = new();

    public RateLimiter(int requestLimit = 10, int windowInSeconds = 30)
    {
        _requestLimit = requestLimit;
        _windowInSeconds = windowInSeconds;
    }

    public bool[] ProcessRequests((string user, DateTime timestamp)[] requests)
    {
        return requests.Select(x => ProcessRequest(x.user, x.timestamp)).ToArray();
    }

    public bool ProcessRequest(string user, DateTime timestamp)
    {
        var userRequests = _userMap.GetOrAdd(user, _ => (new Queue<DateTime>(), new object()));

        lock (userRequests.lockObj)
        {
            var userQueue = userRequests.queue;
            userQueue.Enqueue(timestamp);

            while (userQueue.Count > 0)
            {
                var timeDifference = timestamp - userQueue.Peek();
                if (timeDifference > TimeSpan.FromSeconds(_windowInSeconds))
                {
                    userQueue.Dequeue();
                }
                else
                {
                    break;
                }
            }

            return userQueue.Count <= _requestLimit;
        }
    }
}
