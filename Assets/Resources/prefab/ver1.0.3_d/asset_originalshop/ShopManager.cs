using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using NCMB;
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance = null;
    //public bool ESCtrg = false; //Escを押してるかどうか、または強制的にEscさせるための
    //public int[] EventNumber; //各イベント状態、0はそのイベントが進行していないことを示す
    //public int[] Triggers; //各トリガーの状態。イベントとは違い、この宝箱は一度取ってあるのか、この敵は討伐した奴かどうかなどを格納
    public int[] shopID; //一時的な、ショップIDに該当する商品内容(会話イベントから各NPCに応じてショップを変えるため)
    public int old_year = 2023;
    [System.Serializable]
    public struct DevDateTime
    {
        public int year;
        public int month;
        public int day;
    }
    public DevDateTime devdays;
    public DateTime checkdev = new DateTime(2003, 7, 28);
    //課金要素について
    public float get_devcoin = 0f;
    public float get_devnugget = 0f;
    public string mpurse_address = "";
    public bool mpurseuser_on = false;
    public int select_buyid = 0;
    public DateTime tmpdays;
    //[System.Serializable]
    //public struct AdsTips_ID
    //{
    //    [Multiline]
    //    public string jp_tips;
    //    [Multiline]
    //    public string en_tips;
    //}
    //public AdsTips_ID[] adstips;

    public bool dxtrg = false;
    public string tmpchildobj;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        //日替わり処理
        DateTime tmpdays = instance.GetGameDay();
        var oldYear = PlayerPrefs.GetInt("oldallYear", (tmpdays.Year - 1));
        var oldMonth = PlayerPrefs.GetInt("oldallMonth", (tmpdays.Month - 1));
        var oldDay = PlayerPrefs.GetInt("oldallDay", (tmpdays.Day - 1));
        DateTime olddays = new DateTime(oldYear, oldMonth, oldDay);
        if (Math.Abs(GManager.instance.AllSpanCheck(olddays)) > 0)
        {
            PlayerPrefs.SetInt("oldallYear", tmpdays.Year);
            PlayerPrefs.SetInt("oldallMonth", tmpdays.Month);
            PlayerPrefs.SetInt("oldallDay", tmpdays.Day);
            //その他日替わりセーブ
            PlayerPrefs.SetInt("DayAds", 0);
            PlayerPrefs.SetString("YpY9012nWJzXaBuS", "false");
            PlayerPrefs.Save();
            //日替わり処理
        }
    }
    public DateTime GetGameDay()
    {
        DateTime tmp = DateTime.Today;
        return tmp;
    }
    public int AllSpanCheck(DateTime tmp_time)
    {
        int check_result = 0;

        DateTime today = DateTime.Today;
        DateTime devday = new DateTime(instance.devdays.year, instance.devdays.month, instance.devdays.day);
        if (instance.checkdev != devday)
            today = devday;
        DateTime newday = new DateTime(today.Year, tmp_time.Month, tmp_time.Day);
        TimeSpan tmpdiff = newday - today;
        check_result = (int)tmpdiff.TotalDays;
        //print(check_result.ToString());
        return check_result;
    }
    public bool MonthBoolCheck(DateTime tmp_time)
    {
        bool check_result = false;
        DateTime today = DateTime.Today;
        DateTime devday = new DateTime(instance.devdays.year, instance.devdays.month, instance.devdays.day);
        if (instance.checkdev != devday)
            today = devday;
        DateTime newday = new DateTime(today.Year, tmp_time.Month, tmp_time.Day);
        if (newday.Month == today.Month)
            check_result = true;
        return check_result;
    }
    public int DaySpanCheck(DateTime tmp_time)
    {
        int check_result = 0;
        DateTime today = DateTime.Today;
        DateTime devday = new DateTime(instance.devdays.year, instance.devdays.month, instance.devdays.day);
        if (instance.checkdev != devday)
            today = devday;
        DateTime newday = new DateTime(today.Year, tmp_time.Month, tmp_time.Day);
        check_result = newday.Day - today.Day;
        print(check_result.ToString());
        return check_result;
    }
    public int NewOldSpanCheck(DateTime new_time, DateTime old_time)
    {
        int check_result = 0;
        TimeSpan tmpdiff = new_time - old_time;
        check_result = (int)tmpdiff.TotalDays;
        print(check_result.ToString());
        return check_result;
    }
}