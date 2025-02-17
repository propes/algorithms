using AlgorithmExercises.TopNCollectionsBySize;

namespace AlgorithmExercises.Tests.TopNCollectionsBySize;

public class FileGrouperTests
{
    [Fact]
    public void GetTopCollectionsBySize()
    {
        var files = new AlgorithmExercises.TopNCollectionsBySize.File[]
        {
            new()
            {
                Name = "file1.txt",
                Collection = "A",
                Size = 100,
            },
            new()
            {
                Name = "file2.txt",
                Collection = "B",
                Size = 1200,
            },
            new()
            {
                Name = "file3.txt",
                Collection = "A",
                Size = 1150,
            },
            new()
            {
                Name = "file4.txt",
                Collection = "C",
                Size = 1300,
            },
            new()
            {
                Name = "file5.txt",
                Collection = "B",
                Size = 1250,
            },
            new()
            {
                Name = "file6.txt",
                Collection = "C",
                Size = 1100,
            },
            new()
            {
                Name = "file7.txt",
                Collection = "A",
                Size = 150,
            },
        };

        var sut = new FileGrouper();

        var result = sut.GetTopCollectionsBySize(files, 1);
    }
}
