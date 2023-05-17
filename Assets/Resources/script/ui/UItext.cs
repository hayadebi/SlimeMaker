using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UItext : MonoBehaviour
{
    public Text _text;
    public string ui_mode = "gamemode";
    public float tmp_cleartime = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (ui_mode == "gamemode")
        {
            if (GManager.instance.storymode && !GManager.instance.debug_trg)
            {
                if (GManager.instance.isEnglish == 0)
                    _text.text = "ストーリーモード";
                else
                    _text.text = "Story mode.";
            }
            else if (!GManager.instance.storymode && !GManager.instance.debug_trg)
            {
                if (GManager.instance.isEnglish == 0)
                    _text.text = "みんなのステージモード";
                else
                    _text.text = "Everyone's stage mode.";
            }
            if (GManager.instance.debug_trg)
            {
                if (GManager.instance.isEnglish == 0)
                    _text.text = "テストプレイモード";
                else
                    _text.text = "Test play mode.";
            }
            if(GManager.instance.dx_stageid != -1)
            {
                if (GManager.instance.isEnglish == 0)
                    _text.text = "チャレンジモード";
                else
                    _text.text = "Challenge mode.";
            }
        }
        if (ui_mode == "cleartime")
        {
            tmp_cleartime = GManager.instance.cleartime;
            if (GManager.instance.isEnglish == 0)
            {
                _text.fontSize = 28;
                _text.text = "クリアタイム：" + ((int)(GManager.instance.cleartime / 60)).ToString() + "分" + ((int)(GManager.instance.cleartime % 60)).ToString() + "秒";
            }
            else
            {
                _text.fontSize = 24;
                _text.text = "Clear time：" + ((int)(GManager.instance.cleartime / 60)).ToString() + " minutes and " + ((int)(GManager.instance.cleartime % 60)).ToString() + " seconds";
            }
        }
        else if (ui_mode == "miniscore")
        {
            _text.text = "score:" + GManager.instance.minigame_score.ToString();
        }
        else if (ui_mode == "minigameend")
        {
            GManager.instance.adstrg = false;
            if (GManager.instance.minigame_score >= 100000)
            {
                GManager.instance.dx_mode = true;
                PlayerPrefs.SetString("notdxtrg", "TRUE");
                PlayerPrefs.SetInt("daily_year", DateTime.Today.Year);
                PlayerPrefs.SetInt("daily_month", DateTime.Today.Month);
                PlayerPrefs.SetInt("daily_day", DateTime.Today.Day);
                PlayerPrefs.SetInt("daily_hour", DateTime.Now.Hour);
                PlayerPrefs.SetInt("daily_min", DateTime.Now.Minute);
                PlayerPrefs.SetInt("daily_sec", DateTime.Now.Second);
                PlayerPrefs.Save();
                if (GManager.instance.isEnglish == 0)
                {
                    _text.fontSize = 21;
                    _text.text = "10万スコア達成！\n報酬としてDXコンテンツが1時間解放されます！";
                }
                else
                {
                    _text.fontSize = 19;
                    _text.text = "100,000 score achieved! As a reward,\n DX content will be released for one hour!";
                }
            }
            else
            {
                if (GManager.instance.isEnglish == 0)
                    _text.text = "100000スコアまで残り"+(100000-GManager.instance.minigame_score).ToString()+"！";
                else
                    _text.text = (100000 - GManager.instance.minigame_score).ToString()+" left to score 100,000!";
            }
        }
        if (ui_mode == "appversion")
        {
            _text.text = "Version：" + Application.version;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
