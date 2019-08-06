using UnityEngine;
using System.Collections;

/// <summary>
/// 检测器接口，用于检测和场景物件的触发
/// </summary>
public interface ISeparateDetector
{
    /// <summary>
    /// 是否检测成功
    /// </summary>
    /// <param name="bounds">包围盒</param>
    /// <returns></returns>
    bool Detecte(Bounds bounds,ISeparateEntity entity);


    /// <summary>
    /// 触发器位置
    /// </summary>
    Vector3 position { get; }
}
