using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public const float itemFadeDuretion = .35f;
    public const float targetAlpha = .45f;

    //时间相关
    public const float secondThreshold = 0.01f; //数值越小时间越快
    public const int secondHold = 59;
    public const int minuteHold = 59;
    public const int hourHold = 23;
    public const int dayHold = 30;
    /// <summary>季节</summary>
    public const int seasonHold = 3;

    /// <summary>Loading画面的结束需要的时间</summary>
    public const float fadeDuretion = 1.5f;

}
