using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeparateNode
{
    /// <summary>
    /// 节点包围盒
    /// </summary>
    public Bounds bounds { get; private set; }

    /// <summary>
    /// 节点当前深度
    /// </summary>
    public int depth { get; private set; }

    /// <summary>
    /// 节点数据列表
    /// </summary>
    public LinkedList<ISeparateEntity> entities { get; private set; }

    /// <summary>
    /// 子节点，可能为空
    /// </summary>
    public SeparateNode[] children { get; private set; }


    public SeparateTree tree { get; private set; }

    /// <summary>
    /// 子节点个数
    /// </summary>
    private int mChildCount = 0;


    public SeparateNode(SeparateTree tree, Bounds bounds, int depth, int childCount)
    {
        this.tree = tree;
        this.bounds = bounds;
        this.depth = depth;
        mChildCount = childCount;
        entities = new LinkedList<ISeparateEntity>();
    }

    public void Clear()
    {
        if (children != null)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != null)
                {
                    children[i].Clear();
                }
            }
        }

        if (entities != null)
        {
            entities.Clear();
        }
    }

    public bool Contains(ISeparateEntity entity)
    {   
        if (children != null)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != null && children[i].Contains(entity))
                    return true;
            }
        }
        if (entities != null && entities.Contains(entity))
        {
            return true;
        }
        return false;
    }

    public SeparateNode GetNode(Vector3 position)
    {
        if (bounds.Contains(position))
        {
            if(children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    var node = children[i].GetNode(position);
                    if (node != null)
                    {
                        return null;
                    }
                }
            }
        }

        return null;
    }

    public SeparateNode Insert(ISeparateEntity entity, int depth, int maxDepth)
    {
        if (entity == null)
        {
            return null;
        }       
        if (depth < maxDepth)
        {
            SeparateNode node = GetContainerNode(entity, depth);
            if (node != null)
            {
                if (entities.Contains(entity))
                {
                    entities.Remove(entity);
                }

                return node.Insert(entity, depth + 1, maxDepth);
            }
        }

        if (bounds.ContainsEx(entity.bounds))
        {
            entities.AddFirst(entity);

            entity.node = this;

            return this;
        }

        return null;
    }

    public bool Remove(ISeparateEntity entity)
    { 
        if (entities != null && entities.Contains(entity))
        {
           return entities.Remove(entity);
        }
        else if(children != null)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != null)
                {
                    if(children[i].Remove(entity))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void Trigger(ISeparateDetector detector)
    {
        if (detector == null)
        {
            return;
        }

        if (detector.Detecte(bounds, null))
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    var child = children[i];
                    if (child != null)
                    {
                        child.Trigger(detector);
                    }
                }
            }

            var node = entities.First;
            while (node != null)
            {
                detector.Detecte(node.Value.bounds, node.Value);

                node = node.Next;
            }
        }

    }

    protected SeparateNode GetContainerNode(ISeparateEntity entity, int depth)
    {
        SeparateNode result = null;
        int ix = -1;
        int iz = -1;

        int iy = mChildCount == 4 ? 0 : -1;

        if(children == null)
        {
            children = new SeparateNode[mChildCount];
        }

        int nodeIndex = 0;

        Vector3 halfSize = halfSize = new Vector3(bounds.size.x / 2, mChildCount == 4? bounds.size.y : bounds.size.y / 2, bounds.size.z / 2);
        
        for (int i = ix; i <= 1; i += 2) //i = -1, 1
        {
            for (int j = iz; j <= 1; j += 2) //j = -1, 1
            {
                for (int k = iy; k <= 1; k += 2) //k = 4 or -1,1
                {
                    result = CreateNode(ref children[nodeIndex],
                                        depth,
                                        bounds.center + new Vector3(i * halfSize.x / 2, k * halfSize.y / 2, j * halfSize.z / 2),
                                        halfSize,
                                        entity);

                    if (result != null)
                    {
                        return result;
                    }

                    nodeIndex += 1;
                }
            }
        }
        return null;
    }

    protected SeparateNode CreateNode(ref SeparateNode node, int depth, Vector3 center, Vector3 size, ISeparateEntity entity) 
    {
        SeparateNode result = null;
        if (node == null)
        {
            Bounds bounds = new Bounds(center, size);
            if (bounds.ContainsEx(entity.bounds))
            {
                SeparateNode newNode = new SeparateNode(tree, bounds, depth + 1, mChildCount);
                node = newNode;
                result = node;
            }
        }
        else if (node.bounds.ContainsEx(entity.bounds))
        {
            result = node;
        }
        return result;
    }

#if UNITY_EDITOR
    public void DrawNode(Color minDepthColor, Color maxDepthColor, Color entityColor, int minDepth, int maxDepth, bool drawEntity)
    {
        if (children != null)
        {
            for (int i = 0; i < children.Length; i++)
            {
                var node = children[i];
                if (node != null)
                    node.DrawNode(minDepthColor, maxDepthColor, entityColor, minDepth, maxDepth, drawEntity);
            }
        }

        if (depth >= minDepth && depth <= maxDepth)
        {
            float d = ((float)depth) / maxDepth;
            Color color = Color.Lerp(minDepthColor, maxDepthColor, d);

            DrawBounds(bounds, color);
        }
        if (drawEntity)
        {
            var node = entities.First;
            while (node != null)
            {
                var entity = node.Value ;
                if (entity != null)
                {
                   
                    DrawBounds(entity.bounds, entityColor);
                }
                node = node.Next;
            }  
        }

    }

    private void DrawBounds(Bounds bounds, Color color)
    {
        Gizmos.color = color;

        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

#endif
}
