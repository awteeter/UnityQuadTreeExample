using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad
{
    public GameObject quadObject;
    public readonly Vector3 center;

    public readonly int generation;
    private readonly int limit;

    public Quad parent;
    public Quad[] children;

    public Quad(int _g, int _l, Quad _par, Vector3 _c)
    {
        generation = _g;
        limit = _l;
        parent = _par;
        center = _c;
    }

    public void Subdivide() // divides this quad into 4 smaller quads, referencing this as their parent
    {
        if (generation < limit) // if greater than maximum allowed generations
        {
            children = new Quad[4];

            children[0] = new Quad(generation + 1, limit, this, new Vector3(-0.25f, -0.25f, 0));
            children[1] = new Quad(generation + 1, limit, this, new Vector3(0.25f, -0.25f, 0));
            children[2] = new Quad(generation + 1, limit, this, new Vector3(0.25f, 0.25f, 0));
            children[3] = new Quad(generation + 1, limit, this, new Vector3(-0.25f, 0.25f, 0));

            if (generation + 1 < limit)
            {
                for (int i = 0; i < 4; i++)
                {
                    children[i].Subdivide(); // recursively subdivide children
                }
            }
        }
    }

    public Quad[] GetDescendants() // returns array filled with child quads, travelling down the tree recursively
    {
        List<Quad> descendants = new List<Quad>();

        if (children == null || children.Length == 0) // if reached end of the branch, return
        {
            return descendants.ToArray();
        }
        else
        {
            descendants.AddRange(children); // add all children collected thus far
            foreach (Quad child in children)
            {
                descendants.AddRange(child.GetDescendants()); // recursively search each child's descendants
            }
        }

        return descendants.ToArray();
    }
}
