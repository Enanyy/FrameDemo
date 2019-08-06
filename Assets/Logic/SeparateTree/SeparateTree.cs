using UnityEngine;

public enum SeparateType
{
    OcTree, //八叉树
    QuadTree, //四叉树
}

public class SeparateTree
{

    public Bounds bounds
    {
        get
        {
            if (root != null)
                return root.bounds;
            return default(Bounds);
        }
    }

    public int maxDepth { get; private set; }

    public SeparateType treeType { get; private set; }

    /// <summary>
    /// 根节点
    /// </summary>
    public SeparateNode root { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="treeType">树类型</param>
    /// <param name="center">树中心</param>
    /// <param name="size">树区域大小</param>
    /// <param name="maxDepth">树最大深度</param>
    public SeparateTree(SeparateType treeType, Vector3 center, Vector3 size, int maxDepth)
    {
        this.treeType = treeType;
        this.maxDepth = maxDepth;

        root = new SeparateNode(this, new Bounds(center, size), 0, this.treeType == SeparateType.QuadTree ? 4 : 8);
    }

    public void Add(ISeparateEntity entity)
    {
        root.Insert(entity, 0, maxDepth);
    }

    public void Clear()
    {
        root.Clear();
    }

    public bool Contains(ISeparateEntity entity)
    {
        return root.Contains(entity);
    }

    public void Remove(ISeparateEntity entity)
    {
         root.Remove(entity);
    }

    public SeparateNode GetNode(Vector3 position)
    {
        return root.GetNode(position);
    }

    public void Trigger(ISeparateDetector detector)
    {
        if (detector == null)
            return;
        root.Trigger(detector);
    }

    public static implicit operator bool(SeparateTree tree)
    {
        return tree != null;
    }

#if UNITY_EDITOR
    public void DrawTree(Color minDepthColor, Color maxDepthColor, Color entityColor, int minDepth, int maxDepth,
        bool drawEntity)
    {
        if (root != null)
        {
            root.DrawNode(minDepthColor, maxDepthColor, entityColor, minDepth, maxDepth, drawEntity);
        }
    }
#endif
}
