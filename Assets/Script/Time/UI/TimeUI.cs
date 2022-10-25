using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{

    public RectTransform dayNightImage;
    /// <summary>季节</summary>
    public RectTransform clockParent;

    //时间文本
    public TextMeshProUGUI dateText;//显示日期
    public TextMeshProUGUI timeText;//显示12:12

    /// <summary>季节的图标</summary>
    public Image seasonImage;
    /// <summary>季节的图片</summary>
    public Sprite[] seasonSprites;

    private List<GameObject> clockBlocks = new List<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < clockParent.childCount; i++)
        {
            clockBlocks.Add(clockParent.GetChild(i).gameObject);
            clockParent.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        EventHandler.GameDateEvent += OnGameDateEvent;
        EventHandler.GameMinuteEvent += OnGameMinuteEvent;
    }
    private void OnDisable()
    {
        EventHandler.GameDateEvent -= OnGameDateEvent;
        EventHandler.GameMinuteEvent -= OnGameMinuteEvent;
    }


    /// <summary>
    /// 显示时间
    /// </summary>
    /// <param name="minute"></param>
    /// <param name="hour"></param>
    private void OnGameMinuteEvent(int minute, int hour) => timeText.text = hour.ToString("00") + ":" + minute.ToString("00");
    /// <summary>
    /// 显示日期和春夏秋冬
    /// </summary>
    /// <param name="hour"></param>
    /// <param name="day"></param>
    /// <param name="month"></param>
    /// <param name="year"></param>
    /// <param name="season"></param>
    private void OnGameDateEvent(int hour, int day, int month, int year, ESeason season)
    {
        dateText.text = year + "年" + month.ToString("00") + "月" + day.ToString("00") + "日";
        seasonImage.sprite = seasonSprites[(int)season];//切换春夏秋冬
        SwitchHourImage(hour);
        DayNightImageRotate(hour);
    }
    /// <summary>显示时间的格子</summary>
    private void SwitchHourImage(int hour)
    {
        int index = hour / 4;
        if (index == 0)//如果是0点的话
        {
            foreach (var item in clockBlocks)
                item.SetActive(false);
        }
        else
        {
            for (int i = 0; i < clockBlocks.Count; i++)
                clockBlocks[i].SetActive(i < index+1);
        }
    }
    /// <summary>夜晚等时间图片的切换</summary>
    private void DayNightImageRotate(int hour)
    {
        var target = new Vector3(0, 0, hour * 15-90);//保证从黑天开始
        dayNightImage.DORotate(target, 1f, RotateMode.Fast);
    }

}
