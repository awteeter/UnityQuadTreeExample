using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree
{
    public List<Quad> nodes;
    private readonly int limit;

    public QuadTree(int _l)
    {
        limit = _l;
    }

    public void GenerateTree()
    {
        nodes = new List<Quad>();

        AddRoot();
        AddGenerations();
    }

    private void AddRoot()
    {
        nodes.Add(new Quad(0, limit - 1, null, Vector3.zero));
    }

    private void AddGenerations()
    {
        nodes[0].Subdivide();
        nodes.AddRange(nodes[0].GetDescendants());
    }
}
