using System;
using System.Collections.Generic;
using UnityEngine;

public class TestEntity : MonoBehaviour, ISeparateEntity
{
    public Bounds bounds
    {
        get
        {
            Vector3 center = new Vector3(transform.position.x,0,transform.position.z);
            return new Bounds(center,new Vector3(10,0,10));
        }
    }

    public SeparateNode node { get ; set ; }

    void Update()
    {
        if (node != null)
        {
            if (node.bounds.ContainsEx(bounds)==false || node.depth < node.tree.maxDepth)
            {
                node.Remove(this);
                node.tree.Add(this);
            }
        }
    }
}

