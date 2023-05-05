using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class clickbutton : MonoBehaviour
{
    public bool destroytrg = true;
    [Header("シーンチェンジ用")]
    public string next_scene = "loadstage";
    public bool check_debug = true;
    public bool check_story = false;

    private bool clicktrg = false;
    public bool storyadd = false;
    public qr_imgread Webcm_off = null;
    [Header("アニメーション用")]
    public Animator anim;
    public string animname = "Abool";
    public GameObject destroy_obj = null;
    public float destroy_time = 1f;
    [Header("UI生成用")]
    public GameObject uiobj;
    public bool title_starttrg = false;
    public bool notsay = true;
    [Header("テキスト")]
    public Text _text;
    public int dxtrg = -1;
    [Header("スクロールバー固定ギミック")]
    public int fixed_selectid = -1;
    public Image fixed_idobjimg = null;
    public Text fixed_idobjname = null;
    public GameObject EnableUI = null;
    public float start_posy = -36;
    public float posy_add = 87;
    public Button dxbtn=null;
    public Text dxtext = null;
    public GameObject dxmark = null;
    // Start is called before the first frame update
    void Start()
    {
        if (!GManager.instance.slime_titleui && title_starttrg)
        {
            GManager.instance.slime_titleui = true;
            SetUI();
        }
        if (EnableUI != null&&fixed_selectid == -1)
        {
            if (GManager.instance.isEnglish == 0)
            {
                fixed_idobjname.fontSize = 14;
                fixed_idobjname.text = "最後に設置したギミックを固定化";
            }
            else
            {
                fixed_idobjname.fontSize = 12;
                fixed_idobjname.text = "Fixing the last placed gimmick.";
            }
        }
        else if (EnableUI != null && fixed_selectid == 0)
        {
            if (GManager.instance.isEnglish == 0)
            {
                fixed_idobjname.fontSize = 14;
                fixed_idobjname.text = "消しゴムツール";
            }
            else
            {
                fixed_idobjname.fontSize = 12;
                fixed_idobjname.text = "Eraser tool";
            }
        }
        DateTime silver = new DateTime(2023, 9, 24);
        DateTime gold = new DateTime(2023, 5, 5);
        if (dxbtn != null && (GManager.instance.dx_mode ||((GManager.instance.AllSpanCheck(gold)>=0&& GManager.instance.AllSpanCheck(gold) <= 6) ||(GManager.instance.AllSpanCheck(silver) >= 0 && GManager.instance.AllSpanCheck(silver) <= 6))))
        {
            ;
        }
        else if (dxbtn != null)
        {
            ColorBlock tmpb=dxbtn.colors;
            Color tmpc = tmpb.normalColor;
            tmpc.a = 0.35f;
            tmpb.normalColor = tmpc;
            tmpc = dxtext.color;
            tmpc.a = 0.35f;
            dxtext.color = tmpc;
            dxbtn.colors = tmpb;
            dxmark.SetActive(false);
        }

    }
    private void Update()
    {
        
    }
    public void FixedUITime(int tmpid = 0)
    {
        fixed_selectid = tmpid;
        if (EnableUI != null)
            GManager.instance.setmenu = 1;
        if (fixed_idobjimg != null )
            fixed_idobjimg.sprite = GManager.instance.stageobj_createimg[fixed_selectid];
        if (fixed_idobjname != null)
        {
            fixed_idobjname.fontSize = 22;
            fixed_idobjname.text = GManager.instance.stageobj_data[fixed_selectid].name[GManager.instance.isEnglish];
        }
    }
    public void FixedCreate()
    {
        GManager.instance.setrg = 8;
        GManager.instance.fixed_createid = !GManager.instance.fixed_createid;
        if (fixed_selectid != -1)
            GManager.instance.set_createid = fixed_selectid;
        if (GManager.instance.fixed_createid)
        {
            if (GManager.instance.isEnglish == 0)
                _text.text = "ギミック固定：ON";
            else
                _text.text = "Gimmick fixation: ON";
        }
        else if (!GManager.instance.fixed_createid)
        {
            if (GManager.instance.isEnglish == 0)
                _text.text = "ギミック固定：OFF";
            else
                _text.text = "Gimmick fixation: OFF";
        }
        GManager.instance.setmenu = 0;
        EnableUI.SetActive(false);
    }
    public void NextScene()
    {
        if (!clicktrg)
        {
            clicktrg = true;
            Instantiate(GManager.instance.all_ui[0], transform.position, transform.rotation);
            GManager.instance.setrg = 2;
            //if (Webcm_off != null)
            //{
            //    Webcm_off._webCam.Stop();
            //    Webcm_off._webCam = null;
            //}
            Invoke(nameof(SceneChange), 1f);
        }
    }
    public void SetUI()
    {
        DateTime silver = new DateTime(2023, 9, 24);
        DateTime gold = new DateTime(2023, 5, 5);
        if (GManager.instance.setmenu <= 0 &&(dxtrg == -1 || (dxtrg != -1 && GManager.instance.dx_mode )|| ((GManager.instance.AllSpanCheck(gold) >= 0 && GManager.instance.AllSpanCheck(gold) <= 6) || (GManager.instance.AllSpanCheck(silver) >= 0 && GManager.instance.AllSpanCheck(silver) <= 6))))
        {
            GManager.instance.setrg = 0;
            GManager.instance.setmenu = 1;
            GManager.instance.walktrg = false;
            Instantiate(uiobj, transform.position, transform.rotation);
        }
        else
            GManager.instance.setrg = 1;

    }
    void SceneChange()
    {
        if(dxtrg != 999)
            GManager.instance.dx_stageid = dxtrg;
        GManager.instance.over = false;
        GManager.instance.walktrg = true;
        GManager.instance.setmenu = 0;
        GManager.instance.goal_num = 0;
        GManager.instance.debug_trg = check_debug;
        if(next_scene == "title") Resources.UnloadUnusedAssets();
        if (!notsay)
            GManager.instance.notsay = false;
        if (check_story)
            GManager.instance.storymode = check_story;
        if (storyadd)
            GManager.instance.select_stage += 1;
        if (!storyadd || (storyadd && GManager.instance.select_stage < 7))
            SceneManager.LoadScene(next_scene);
        else
        {
            Resources.UnloadUnusedAssets();
            GManager.instance.select_stage = 0;
            SceneManager.LoadScene("title");
        }
    }
    public void ResetCreate()
    {
        GManager.instance.setrg = 8;
        GManager.instance.reset_time = 0.3f;
    }
    public void AnimSetBool()
    {
        GManager.instance.setrg = 8;
        anim.SetBool(animname, !anim.GetBool(animname));

        if (destroy_obj != null)
        {
            GManager.instance.setmenu = 0;
            GManager.instance.walktrg = true;
            GManager.instance.ESCtrg = false;
            if (destroytrg)
                Destroy(destroy_obj.gameObject, destroy_time);
            else
                destroy_obj.SetActive(false);
        }
    }
    public void SetTargetActive()
    {
        if (GManager.instance.setmenu <= 0 && !GManager.instance.fixed_createid)
        {
            GManager.instance.setmenu = 1;
            GManager.instance.setrg = 0;
            uiobj.SetActive(true);
        }
        else if (GManager.instance.setmenu <= 0 && GManager.instance.fixed_createid)
        {
            GManager.instance.setmenu = 0;
            GManager.instance.setrg = 0;
            GManager.instance.fixed_createid = false;
            uiobj.SetActive(false);
            if (GManager.instance.isEnglish == 0)
                _text.text = "ギミック固定：OFF";
            else
                _text.text = "Gimmick fixation: OFF";
        }
    }
    public void quitClick()
    {
        Application.Quit();
    }
}
