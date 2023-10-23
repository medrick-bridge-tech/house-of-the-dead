using System.Collections.Generic;
using MyStateMachine;
public class Graph<T> where T : Node
{
    protected Dictionary<T, List<T>> statesGraph = new Dictionary<T, List<T>>();

    public void AddVertex(T vertex)
    {
        if (!statesGraph.ContainsKey(vertex))
        {
            statesGraph[vertex] = new List<T>();
        }
    }
    
    public void AddEdge(T source, T destination)
    {
        if (!statesGraph.ContainsKey(source))
        {
            AddVertex(source);
        }

        if (!statesGraph.ContainsKey(destination))
        {
            AddVertex(destination);
        }

        statesGraph[source].Add(destination);
    }
}