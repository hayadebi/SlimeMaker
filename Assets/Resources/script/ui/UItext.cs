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
                    _text.text = "ストーリーモード";
            }
            else if (!GManager.instance.storymode && !GManager.instance.debug_trg)
            {
                    _text.text = "みんなのステージモード";
            }
            if (GManager.instance.debug_trg)
            {
                    _text.text = "テストプレイモード";
            }
            if (GManager.instance.dx_stageid != -1)
            {
                _text.text = "チャレンジモード";
            }
        }
        if (ui_mode == "cleartime")
        {
            tmp_cleartime = GManager.instance.cleartime;
                _text.fontSize = 28;
                _text.text = "クリアタイム：" + ((int)(GManager.instance.cleartime / 60)).ToString() + "分" + ((int)(GManager.instance.cleartime % 60)).ToString() + "秒";
           
        }
        else if (ui_mode == "miniscore")
        {
            _text.text = "スコア:" + GManager.instance.minigame_score.ToString();
        }
        else if (ui_mode == "minigameend")
        {
            GManager.instance.adstrg = false;
            //if (GManager.instance.minigame_score >= 150000)
            //{
            //    GManager.instance.dx_mode = true;
            //    PlayerPrefs.SetString("notdxtrg", "TRUE");
            //    PlayerPrefs.SetInt("daily_year", DateTime.Today.Year);
            //    PlayerPrefs.SetInt("daily_month", DateTime.Today.Month);
            //    PlayerPrefs.SetInt("daily_day", DateTime.Today.Day);
            //    PlayerPrefs.SetInt("daily_hour", DateTime.Now.Hour);
            //    PlayerPrefs.SetInt("daily_min", DateTime.Now.Minute);
            //    PlayerPrefs.SetInt("daily_sec", DateTime.Now.Second);
            //    PlayerPrefs.Save();
            //        _text.fontSize = 21;
            //        _text.text = "15万スコア達成！\n報酬としてDXコンテンツが1時間解放されます！";
            //}
            //else
            //{
            //        _text.text = "150000スコアまで残り"+(150000-GManager.instance.minigame_score).ToString()+"！";
            //}
            _text.text = "獲得デビコイン：+"+GManager.instance.tmpget_devcoin.ToString()+"DC";
            var tmpalldc = PlayerPrefs.GetFloat("getdc", 0);
            tmpalldc += GManager.instance.tmpget_devcoin;
            PlayerPrefs.SetFloat("getdc", tmpalldc);
            PlayerPrefs.Save();
            GManager.instance.tmpget_devcoin = 0;

        }
        if (ui_mode == "appversion")
        {
            _text.text = "バージョン：" + Application.version+GManager.instance.plusalpha;
        }
    }

}
