using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class clickbutton : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        if (!GManager.instance.slime_titleui && title_starttrg)
        {
            GManager.instance.slime_titleui = true;
            SetUI();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FixedCreate()
    {
        GManager.instance.setrg = 8;
        GManager.instance.fixed_createid = !GManager.instance.fixed_createid;
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
    }
    public void NextScene()
    {
        if (!clicktrg)
        {
            clicktrg = true;
            Instantiate(GManager.instance.all_ui[0], transform.position, transform.rotation);
            GManager.instance.setrg = 2;
            if (Webcm_off != null)
            {
                Webcm_off._webCam.Stop();
                Webcm_off._webCam = null;
            }
            Invoke(nameof(SceneChange), 1f);
        }
    }
    public void SetUI()
    {
        if (GManager.instance.setmenu <= 0 &&(dxtrg == -1 || (dxtrg != -1 && GManager.instance.dx_mode)))
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
            Destroy(destroy_obj.gameObject, destroy_time);
        }
    }
    public void quitClick()
    {
        Application.Quit();
    }
}
