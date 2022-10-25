using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBounds : MonoBehaviour
{
    private void OnEnable() => EventHandler.AfterSceneLoadedEvent += SwichConfinerShape;
    private void OnDisable() => EventHandler.AfterSceneLoadedEvent -= SwichConfinerShape;

    /// <summary>
    /// 切换场景的时候找到 限定范围的组件
    /// </summary>
    private void SwichConfinerShape()
    {
        PolygonCollider2D ConfinerShape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = ConfinerShape;
        //Call this if the bounding shape's points change at runtime
        confiner.InvalidatePathCache();
    }
}
 