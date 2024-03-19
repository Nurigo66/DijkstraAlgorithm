using DijkstraAlgorithm;

var nodes = new Dictionary<string, Node>()
{
    ["A"] = new Node("A"),
    ["B"] = new Node("B"),
    ["C"] = new Node("C"),
    ["D"] = new Node("D"),
    ["E"] = new Node("E"),
    ["F"] = new Node("F")
};
nodes["A"].AddEdge(nodes["B"], 2).AddEdge(nodes["D"], 8);
nodes["B"].AddEdge(nodes["A"], 2).AddEdge(nodes["D"], 5).AddEdge(nodes["E"], 6);
nodes["C"].AddEdge(nodes["E"], 9).AddEdge(nodes["F"], 3);
nodes["D"].AddEdge(nodes["A"], 8).AddEdge(nodes["B"], 5).AddEdge(nodes["E"], 3).AddEdge(nodes["F"], 2);
nodes["E"].AddEdge(nodes["B"], 6).AddEdge(nodes["C"], 9).AddEdge(nodes["F"], 1).AddEdge(nodes["D"], 3);
nodes["F"].AddEdge(nodes["D"], 2).AddEdge(nodes["E"], 1).AddEdge(nodes["C"], 3);

var finalNode = new Node("C");


var distances = nodes.ToDictionary(kvp => kvp.Value, kvp => (int?)int.MaxValue);
var parent = new Dictionary<Node, Node>();
var undiscoveredNodes = new HashSet<Node>(nodes.Values);

distances[nodes["A"]] = 0;

while (undiscoveredNodes.Count > 0)
{
    var current = undiscoveredNodes.MinBy(node => distances[node]);
    undiscoveredNodes.Remove(current);

    if (current == finalNode) { break; }

    foreach (var (adjacentNode, distance) in current.Edges)
    {
        var subDistance = distances[current] + distance;
        if (subDistance < distances[adjacentNode])
        {
            distances[adjacentNode] = subDistance;
            parent[adjacentNode] = current;

        }

    }

}

var pathNodes = new List<Node>();
var currentNode = finalNode;

while (currentNode != null)
{
    pathNodes.Insert(0, currentNode);
    currentNode = parent.TryGetValue(currentNode, out var parentNode) ? parentNode : null;
}
Console.WriteLine(string.Join("=>", pathNodes.Select(i => i.Name)));
        Console.WriteLine($"Total Distance = {distances[finalNode]}");
    



