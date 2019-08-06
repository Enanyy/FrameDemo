using System;
using System.Collections.Generic;
using UnityEngine;
public class TestDetector : MonoBehaviour, ISeparateDetector
{
    public Vector3 position { get { return  new Vector3(transform.position.x,0,transform.position.z);} }

    public Bounds bounds { get { return  new Bounds(new Vector3(transform.position.x,0,transform.position.z),new Vector3(10,0,10) );} }


    public Test mTest;

    public bool Detecte(Bounds bounds,ISeparateEntity entity)
    {
        if (entity == null)
        {
            return bounds.ContainsEx(this.bounds);
        }
        else
        {
            bool result = entity.bounds.OverlapsEx(this.bounds);
            if (result)
            {
                OnTrigger(entity);
            }
            return result;
        }

    }

    public void OnTrigger(ISeparateEntity entity)
    {
        Debug.Log("OnTrigger");
    }

    void Update()
    {
        if (mTest != null && mTest.mTree!= null)
        {
            mTest.mTree.Trigger(this);
        }
        
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}

