using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeTest : MonoBehaviour
{
    private readonly List<GameObject> quads = new List<GameObject>();
    public static QuadTreeTest instance;

    [Range(1, 8)] public int maximumGenerations;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        Clear();

        instance = this;

        QuadTree quadTree = new QuadTree(maximumGenerations);
        quadTree.GenerateTree();

        foreach(Quad quad in quadTree.nodes)
        {
            CreateGameObject(quad);
        }

        Debug.Log($"Created {quadTree.nodes.Count} nodes");
    }

    public void Clear()
    {
        foreach (GameObject quadObject in quads)
        {
            DestroyImmediate(quadObject);
        }
        quads.Clear();
    }

    private void CreateGameObject(Quad quad)
    {
        GameObject quadObject;

        if (quad.children == null || quad.children.Length == 0)
        {
            quadObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        }
        else
        {
            quadObject = new GameObject();
        }

        if (quad.parent != null)
        {
            quadObject.transform.parent = quad.parent.quadObject.transform;
        }
        else
        {
            quadObject.transform.parent = transform;
            quadObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }

        quadObject.transform.localPosition = quad.center;

        if (quad.generation != 0)
        {
            quadObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            quadObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        quadObject.name = "Generation " + quad.generation;
        quad.quadObject = quadObject;
        quads.Add(quadObject);
    }
}
