using AlgorithmExercises.LowestCommonAncestor;

namespace AlgorithmExercises.Tests.LowestCommonAncestor;

public class LowestCommonAncestorTests
{
    [Fact]
    public void TwoEmployeesInSameNode()
    {
        var sut = new Solution();

        var mona = new Node("Mona");
        var lisa = new Node("Lisa");
        var company = new Node("Company", [mona, lisa]);

        var result = sut.GetCommonGroupForEmployees(company, ["Mona", "Lisa"]);

        result.Should().Be("Company");
    }

    [Fact]
    public void ThreeEmployeesInSameNode()
    {
        var sut = new Solution();

        var mona = new Node("Mona");
        var lisa = new Node("Lisa");
        var leo = new Node("Leo");
        var company = new Node("Company", [mona, lisa, leo]);

        var result = sut.GetCommonGroupForEmployees(company, ["Mona", "Lisa", "Leo"]);

        result.Should().Be("Company");
    }

    [Fact]
    public void OneEmployeeNotInTheTree()
    {
        var sut = new Solution();

        var mona = new Node("Mona");
        var company = new Node("Company", [mona]);

        var result = sut.GetCommonGroupForEmployees(company, ["Mona", "Lisa"]);

        result.Should().BeNull();
    }

    [Fact]
    public void EmployeesInDifferentNodes()
    {
        var sut = new Solution();

        var mona = new Node("Mona");
        var lisa = new Node("Lisa");
        var engineering = new Node("Engineering", [mona]);
        var finance = new Node("Finance", [lisa]);
        var company = new Node("Company", [engineering, finance]);

        var result = sut.GetCommonGroupForEmployees(company, ["Mona", "Lisa"]);

        result.Should().Be("Company");
    }

    [Fact]
    public void EmployeesInSameNodeThatIsNotTheTopNode()
    {
        var sut = new Solution();

        var mona = new Node("Mona");
        var lisa = new Node("Lisa");
        var engineering = new Node("Engineering", [mona, lisa]);
        var company = new Node("Company", [engineering]);

        var result = sut.GetCommonGroupForEmployees(company, ["Mona", "Lisa"]);

        result.Should().Be("Engineering");
    }

    [Fact]
    public void EmployeesInDifferentNodesAtDifferentLevels()
    {
        var sut = new Solution();

        var mona = new Node("Mona");
        var lisa = new Node("Lisa");
        var fe = new Node("FE", [lisa]);
        var engineering = new Node("Engineering", [mona, fe]);
        var company = new Node("Company", [engineering]);

        var result = sut.GetCommonGroupForEmployees(company, ["Mona", "Lisa"]);

        result.Should().Be("Engineering");
    }
}
