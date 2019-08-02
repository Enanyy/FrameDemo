using UnityEngine;

/// <summary>
/// 节点实体
/// </summary>
public interface ISeparateEntity
{
    /// <summary>
    /// 该物体的包围盒
    /// </summary>
    Bounds bounds { get; }

    /// <summary>
    /// 该物体所在的节点
    /// </summary>
    SeparateNode node { get; set; }
}