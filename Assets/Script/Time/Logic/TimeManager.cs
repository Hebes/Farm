using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>时间系统</summary>
public class TimeManager : MonoBehaviour
{
    private int gameSecond, gameMinute, gameHour, gameDay, gameMonth, gameYear;
    private ESeason eGameSeason = ESeason.春天;
    /// <summary>每个季度有多少个月</summary>
    private int monthInSeason = 3;
    /// <summary>时间的暂停</summary>
    public bool gameCLockPause;
    /// <summary>计时器</summary>
    private float tiktime;

    private void Awake() => NewGameTime();
    private void Start()
    {
        EventHandler.CallGameDateEvent(gameHour, gameDay, gameMonth, gameYear, eGameSeason);
        EventHandler.CallGameMinuteEvent(gameMinute, gameHour);
    }
    private void Update()
    {
        if (!gameCLockPause)
        {
            tiktime += Time.deltaTime;
            if (tiktime >= Settings.secondThreshold)
            {
                tiktime -= Settings.secondThreshold;
                UpdateGameTime();
            }
        }

        if (Input.GetKey(KeyCode.T))
        {
            for (int i = 0; i < 120; i++)
            {
                UpdateGameTime();
            }
        }
    }

    /// <summary>用于初始化时间</summary>
    private void NewGameTime()
    {
        gameSecond = 0;
        gameMinute = 0;
        gameHour = 7;
        gameDay = 1;
        gameMonth = 1;
        gameYear = 2022;
        eGameSeason = ESeason.春天;
    }
    /// <summary>时间更新</summary>
    private void UpdateGameTime()
    {
        gameSecond++;
        if (gameSecond > Settings.secondHold)
        {
            gameMinute++;//分

            gameSecond = 0;
            if (gameMinute > Settings.minuteHold)
            {
                gameHour++;//小时
                gameMinute = 0;
                if (gameHour > Settings.hourHold)
                {
                    gameDay++;
                    gameHour = 0;
                    if (gameDay > Settings.dayHold)
                    {
                        gameDay = 1;
                        gameMonth++;
                        if (gameMonth > 12)
                            gameMonth = 1;

                        monthInSeason--;
                        if (monthInSeason == 0)
                            monthInSeason = 3;
                        int seasonNumber = (int)eGameSeason;
                        seasonNumber++;

                        if (seasonNumber > Settings.seasonHold)
                        {
                            seasonNumber = 0;
                            gameYear++;
                        }
                        eGameSeason = (ESeason)seasonNumber;

                        if (gameYear > 9999)
                        {
                            gameYear = 2022;
                        }
                    }
                }
            }
            EventHandler.CallGameDateEvent(gameHour, gameDay, gameMonth, gameYear, eGameSeason);
        }
        EventHandler.CallGameMinuteEvent(gameMinute, gameHour);
        //Debug.Log("Second 秒：" + gameSecond + "Minute 分：" + gameMinute);
    }
}
