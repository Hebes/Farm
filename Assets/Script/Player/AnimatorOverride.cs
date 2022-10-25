using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    private Animator[] animators;

    /// <summary>被举起的物品的图片</summary>
    public SpriteRenderer holdItem;

    [Header("各部分动画列表")]
    public List<AnimatorType> animatorTypes;

    private Dictionary<string, Animator> animatorNameDic = new Dictionary<string, Animator>();

    private void OnEnable()
    {
        EventHandler.ItemSelectEvent += OnItemSelectEvent;
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
    }
    private void OnDisable()
    {
        EventHandler.ItemSelectEvent -= OnItemSelectEvent;
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
    }
    private void Awake()
    {
        animators = GetComponentsInChildren<Animator>();
        foreach (var anim in animators)
            animatorNameDic.Add(anim.name, anim);
    }

    private void OnBeforeSceneUnloadEvent()
    {
        holdItem.enabled = false;
        SwitchAnimator(EPartType.None);
    }

    /// <summary>物品选中类型，播放对应动画</summary>
    /// <param name="itemDatails"></param><param name="idSelect">是否选中</param>
    private void OnItemSelectEvent(ItemDetails itemDatails, bool idSelect)
    {
        //WORKFLOW:不同的工具返回不同的动画，在这里补全
        EPartType currentType = itemDatails.eItemType switch
        {
            EItemType.Seed => EPartType.Carry,
            EItemType.Commdity => EPartType.Carry,
            _ => EPartType.None,
        };

        if (idSelect == false)
        {
            currentType = EPartType.None;//如果没有选中物品的话，就切换普通动画状态
            holdItem.enabled = false;//是否启动头顶的物品
        }
        else
        {
            if (currentType== EPartType.Carry)
            {
                holdItem.enabled = true;
                holdItem.sprite = itemDatails.itemOnWorldSprite;
            }
        }

        SwitchAnimator(currentType);
    }

    /// <summary>根据物体类型播放对应动画</summary>
    private void SwitchAnimator(EPartType ePartType)
    {
        foreach (var item in animatorTypes)
        {
            if (item.ePartType == ePartType)
                animatorNameDic[item.ePartName.ToString()].runtimeAnimatorController = item.overrideController;
        }
    }
}
