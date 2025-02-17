namespace AlgorithmExercises.TopNCollectionsBySize;

public class File
{
    public required string Name { get; init; }
    public required string Collection { get; init; }
    public required int Size { get; init; }
}

public class FileGrouper
{
    public (string Collection, long Size)[] GetTopCollectionsBySize(File[] files, int topN)
    {
        // Compute total sizes per collection
        var collectionSizes = new Dictionary<string, long>();
        long totalSystemSize = 0;

        foreach (var file in files)
        {
            if (!collectionSizes.ContainsKey(file.Collection))
                collectionSizes[file.Collection] = 0;
            collectionSizes[file.Collection] += file.Size;
            totalSystemSize += file.Size;
        }

        // Use a min-heap to track top N largest collections
        var minHeap = new SortedSet<(long Size, string Collection)>(
            Comparer<(long Size, string Collection)>.Create(
                (a, b) =>
                    a.Size == b.Size
                        ? a.Collection.CompareTo(b.Collection)
                        : a.Size.CompareTo(b.Size)
            )
        );

        foreach (var kvp in collectionSizes)
        {
            minHeap.Add((kvp.Value, kvp.Key));
            if (minHeap.Count > topN)
                minHeap.Remove(minHeap.Min);
        }

        return minHeap.Reverse().Select(x => (x.Collection, x.Size)).ToArray();
    }
}
