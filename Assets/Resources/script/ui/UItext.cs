using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UItext : MonoBehaviour
{
    public Text _text;
    public string ui_mode = "gamemode";
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
            if (GManager.instance.isEnglish == 0)
            {
                _text.fontSize = 30;
                _text.text = "クリアタイム："+((int)(GManager.instance.cleartime/60)).ToString ()+"分"+ ((int)(GManager.instance.cleartime % 60)).ToString()+"秒";
            }
            else
            {
                _text.fontSize = 24;
                _text.text = "Clear time：" + ((int)(GManager.instance.cleartime / 60)).ToString() + " minutes and " + ((int)(GManager.instance.cleartime % 60)).ToString() + " seconds";
            }
        }
        if(ui_mode == "appversion")
        {
            _text.text = "Version：" + Application.version;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
