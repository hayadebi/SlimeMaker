using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class remove_checkday : MonoBehaviour
{
    public int max_minutes = 59;//この数の日を越えたら
    public int max_day = 0;
    private int check_result = 0;
    private int check_minutes = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("notdxtrg", "FALSE") == "TRUE")
            GManager.instance.dx_mode = true;
        if (!GManager.instance.dx_mode || PlayerPrefs.GetString("notdxtrg","FALSE")=="TRUE")//DX版以外を検知
        {
            DateTime today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            DateTime checkday = new DateTime(PlayerPrefs.GetInt("daily_year", DateTime.Today.Year), PlayerPrefs.GetInt("daily_month", DateTime.Today.Month), PlayerPrefs.GetInt("daily_day", DateTime.Today.Day - 1), PlayerPrefs.GetInt("daily_hour", DateTime.Now.Hour), PlayerPrefs.GetInt("daily_min", DateTime.Now.Minute), PlayerPrefs.GetInt("daily_sec", DateTime.Now.Second));
            TimeSpan tmpdiff = checkday - today;
            check_result = Math.Abs((int)tmpdiff.TotalDays);
            check_minutes = Math.Abs((int)tmpdiff.TotalMinutes);
            //if(GManager.instance.dx_mode && check_minutes>max_minutes)//デイリー1時間を堪能し終わったら
            //{
            //    GManager.instance.dx_mode = false;
            //    PlayerPrefs.SetString("notdxtrg", "FALSE");
            //    PlayerPrefs.Save();
            //}
            //if (check_result > max_day && GManager.instance.dx_mode)//別の日を検知
            //{
            //    GManager.instance.dx_mode = false;
            //    PlayerPrefs.SetString("notdxtrg", "FALSE");
            //    PlayerPrefs.Save();
            //    ;
            //}
            if (check_result > max_day&&!GManager.instance.dx_mode)//別の日を検知
            {
                ;
            }
            else if(check_result <= max_day)//||GManager.instance.dx_mode)
                Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
