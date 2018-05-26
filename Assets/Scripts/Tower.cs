using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int CurrentDiskCount
    {
        get { return stack.Count; }
    }
    [NonSerialized]
    public Vector3 BottomPosition;
    [NonSerialized]
    public Vector3 TopPosition;
    
    private Stack<Disk> stack;

    public void Initialization(int heightTower)
    {
        Bounds bounds = GetComponent<Renderer>().bounds;
        Transform mTransform = GetComponent<Transform>();

        BottomPosition = new Vector3(mTransform.position.x, bounds.center.y + bounds.min.y, mTransform.position.z);
        TopPosition = new Vector3(mTransform.position.x, bounds.center.y + bounds.max.y, mTransform.position.z);
        
        stack = new Stack<Disk>(heightTower);
    }

    public IEnumerator MoveDisk(Tower targetTower)
    {
        yield return stack.Peek().MoveDisk(targetTower);
        
        if (stack.Count >0)
        {
            targetTower.AddDisk(RemoveDisk());
        }
    }
    
    public void AddDisk(Disk disk)
    {
        stack.Push(disk);
    }

    public Disk RemoveDisk()
    {
       return stack.Pop();
    }
}
