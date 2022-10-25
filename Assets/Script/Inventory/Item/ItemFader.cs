using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]//必须挂载SpriteRenderer组件
public class ItemFader : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 逐渐恢复颜色
    /// </summary> 
    public void fadeIn()
    {
        Color TargetColor = new Color(1, 1, 1, 1);
        SpriteRenderer.DOColor(TargetColor, Settings.itemFadeDuretion);
    }

    /// <summary>
    /// 逐渐半透明
    /// </summary>
    public void fadeOut()
    {
        Color TargetColor = new Color(1, 1, 1, Settings.targetAlpha);
        SpriteRenderer.DOColor(TargetColor, Settings.itemFadeDuretion);
    }
}
