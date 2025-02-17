namespace AlgorithmExercises.LowestCommonAncestor;

public record Node(string Value, Node[]? Children = null);

// A variation on the Lowest Common Ancestor of a binary tree problem with the twist that each node
// can have more than two child and the method can support matching more than two nodes.
public class Solution
{
    private Node? _commonAncestor;

    public string? GetCommonGroupForEmployees(Node currentNode, string[] targetEmployees)
    {
        HasCommonGroupForEmployees(currentNode, targetEmployees);
        return _commonAncestor?.Value;
    }

    private bool HasCommonGroupForEmployees(Node currentNode, string[] targetEmployees)
    {
        var matchCount = targetEmployees.Contains(currentNode.Value) ? 1 : 0;

        if (currentNode.Children != null)
        {
            foreach (var child in currentNode.Children)
            {
                var isMatch = HasCommonGroupForEmployees(child, targetEmployees);
                matchCount += isMatch ? 1 : 0;
            }
        }

        if (matchCount == targetEmployees.Length)
        {
            _commonAncestor = currentNode;
        }

        return matchCount > 0;
    }
}
