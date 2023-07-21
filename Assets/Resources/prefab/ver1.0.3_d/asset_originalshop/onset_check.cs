using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class onset_check : MonoBehaviour
{
    private string tmp_text;
    private string[] tmp_arr = null;
    private string[] del = { "\n" };
    [System.Serializable]
    public struct EventDateTime
    {
        public int year;
        public int month;
        public int day;
    }
    [Header("ここからは各イベントのボタン")]
    public EventDateTime[] eventday;
    public int[] eventseasontime;
    public GameObject[] eventbtn;
    public int[] limit_maxhour;
    public int[] limit_minhour;
    [System.Serializable]
    public struct EventGimmickList
    {
        public int[] ev_gimmicks;
    }
    public EventGimmickList[] EvGimmicks;
    // Start is called before the first frame update
    void Start()
    {
      
        if(PlayerPrefs.GetString("notdxtrg", "FALSE")=="TRUE")
            GManager.instance.dx_mode = true;
        Invoke("TimeEventset", 0.3f);
    }
    void TimeEventset()
    {
        if (eventday.Length > 1)
        {
            DateTime devday = new DateTime(GManager.instance.devdays.year, GManager.instance.devdays.month, GManager.instance.devdays.day);

            for (int i = 1; i < eventday.Length;)
            {
                DateTime evday = new DateTime(eventday[i].year, eventday[i].month, eventday[i].day);
                DateTime get_now = DateTime.Now;
                if (GManager.instance.checkdev != devday && limit_maxhour[i] != -1)
                    get_now = new DateTime(GManager.instance.devdays.year, GManager.instance.devdays.month, GManager.instance.devdays.day, limit_minhour[i], 0, 0);
                if (GManager.instance.AllSpanCheck(evday) >= 0 && GManager.instance.AllSpanCheck(evday) <= eventseasontime[i] && (limit_maxhour[i] == -1 || (limit_maxhour[i] != -1 && limit_maxhour[i] >= get_now.Hour && limit_minhour[i] <= get_now.Hour)))
                {
                    if (eventbtn[i] != null)
                    {
                        eventbtn[0].SetActive(false);
                        eventbtn[i].SetActive(true);
                        if (GManager.instance.dx_mode || EvGimmicks[i].ev_gimmicks.Length > 0)
                        {
                            for (int l = 0; l < EvGimmicks[i].ev_gimmicks.Length;)
                            {
                                GManager.instance.stageobj_onset[EvGimmicks[i].ev_gimmicks[l]] = 1;
                                l++;
                            }
                        }
                    }
                    break;
                }
                i++;
            }

        }


        tmp_text = PlayerPrefs.GetString("all_onset", "");
        if (tmp_text != "")
            tmp_arr = tmp_text.Split(del, StringSplitOptions.None);
        if (tmp_arr != null)
        {
            for (int i = 0; i < tmp_arr.Length;)
            {
                try
                {
                    GManager.instance.stageobj_onset[int.Parse(tmp_arr[i])] = 1;
                }
                catch(System.Exception e)
                {
                    print("error:" + e.ToString());
                }
                i++;
            }
        }
        if (GManager.instance.dx_mode)
        {
            for (int i = 1; i < eventday.Length;)
            {
                for (int l = 0; l < EvGimmicks[i].ev_gimmicks.Length;)
                {
                    GManager.instance.stageobj_onset[EvGimmicks[i].ev_gimmicks[l]] = 1;
                    l++;
                }
                i++;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
