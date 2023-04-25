﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class stage_create : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public int this_y;
    public int this_x;
    public int select_id=0;
    public bool addtrg = false;
    private Image img;
    [Multiline]
    private string result_stage;
    public bool not_click = false;
    private float cooltime = 0;
    public bool entertrg = true;
    public Text not_goalslimetext;
    public Text objscript;
    [Multiline]
    public string quick_stage = "";
    private bool wheelstrg = false;
    private int addnum = 1;
    private int reset_id;
    private float reset_cool = 0;
    // Start is called before the first frame update
    void Start()
    {
        GManager.instance.debug_trg = false;
        if (this_y == 0 || this_x == 0 || this_y == 17 || this_x == 25)
            reset_id = 1;
        else if(this_y == 2 && this_x == 2 )
            reset_id = 2;
        else if (this_y == 16 && this_x == 12)
            reset_id = 11;
        else if (this_y == 16 && this_x == 13)
            reset_id = 12;
        else
            reset_id = 0;
        img = this.GetComponent<Image>();
        select_id = GManager.instance.test_y[this_y].test_x[this_x];
        addtrg = false;
        StartBoard();
    }
    void ResetBoard()
    {
        select_id = reset_id;
        addtrg = false;
        StartBoard();
    }
    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.reset_time > 0 && reset_cool <= 0)
        {
            reset_cool = 1f;
            ResetBoard();
        }
        else if (reset_cool >= 0)
            reset_cool -= Time.deltaTime;
        if (cooltime >= 0)
            cooltime -= Time.deltaTime;
        else if(wheelstrg)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if(cooltime <= 0 && scroll > 0)
            {
                cooltime = 0.03f;
                addnum = 1;
                ClickBoard();
            }
            else if (cooltime <= 0 && scroll < 0)
            {
                cooltime = 0.03f;
                addnum = -1;
                ClickBoard();
            }
        }
        else if(!wheelstrg && cooltime <= 0)
        {
            addnum = 1;
        }
    }
    public void ClickBoard()
    {
        if (!GManager.instance.debug_trg && !not_click)
        {
            GManager.instance.setrg = 0;
            if (addtrg)
                select_id += addnum;
            if (select_id > GManager.instance.gimmick_length - 1)
                select_id = 0;
            else if (select_id < 0)
                select_id = GManager.instance.gimmick_length - 1;

            if (!GManager.instance.fixed_createid)
                GManager.instance.set_createid = select_id;
            else if (GManager.instance.fixed_createid)
                select_id = GManager.instance.set_createid;

            GManager.instance.test_y[this_y].test_x[this_x] = select_id;
            img.sprite = GManager.instance.stageobj_createimg[select_id];
            if (GManager.instance.isEnglish == 0)
            {
                objscript.fontSize = 28;
                objscript.text = GManager.instance.stageobj_data[select_id].name[0] + "：" + GManager.instance.stageobj_data[select_id].script[0];
            }
            else
            {
                objscript.fontSize = 24;
                objscript.text = GManager.instance.stageobj_data[select_id].name[1] + "：" + GManager.instance.stageobj_data[select_id].script[1];
            }
            addtrg = true;
        }
        else
        {
            GManager.instance.setrg = 1;
        }
    }
    public void StartBoard()
    {
        if (addtrg)
            select_id += 1;
        if (select_id > GManager.instance.gimmick_length - 1)
            select_id = 0;
        GManager.instance.test_y[this_y].test_x[this_x] = select_id;
        img.sprite = GManager.instance.stageobj_createimg[select_id];
        addtrg = true;
    }
    public void StageTestPlay()
    {
        if (!GManager.instance.debug_trg)
        {
            bool goaltrg = false;
            int blueslime = 0;
            int redslime = 0;
            for (int y = 0; y < GManager.instance.test_y.Length;)
            {
                for (int x = 0; x < GManager.instance.test_y[y].test_x.Length;)
                {
                    if (!goaltrg && GManager.instance.test_y[y].test_x[x] == 2)
                        goaltrg = true;
                    else if (GManager.instance.test_y[y].test_x[x] == 11)
                        blueslime += 1;
                    else if (GManager.instance.test_y[y].test_x[x] == 12)
                        redslime += 1;
                    x++;
                }
                y++;
            }
            if (goaltrg && blueslime == 1 && redslime == 1) 
            {
                GManager.instance.setrg = 2;
                GManager.instance.debug_trg = true;
                GManager.instance.storymode = false;
                result_stage = "stage";
                //------------------------------------
                for (int y = 0; y < GManager.instance.test_y.Length;)
                {
                    result_stage += "\n";
                    for (int x = 0; x < GManager.instance.test_y[y].test_x.Length;)
                    {
                        result_stage += GManager.instance.test_y[y].test_x[x].ToString();
                        if (x < GManager.instance.test_y[y].test_x.Length - 1)
                            result_stage += ",";
                        x++;
                    }
                    y++;
                }
                print(result_stage);
                //------------------------------------
                string path = Application.persistentDataPath + "/stage00.txt";
                bool isAppend = false; // 上書き or 追記
                using (var fs = new StreamWriter(path, isAppend, System.Text.Encoding.GetEncoding("UTF-8")))
                {
                    if (quick_stage == "")
                        fs.Write(result_stage);
                    else if (quick_stage != "")
                        fs.Write(quick_stage);
                }
                Instantiate(GManager.instance.all_ui[0], transform.position, transform.rotation);
                Invoke(nameof(SceneChange), 1f);
               
            }
            else
            {
                GManager.instance.setrg = 1;
                if (GManager.instance.isEnglish == 0)
                {
                    not_goalslimetext.fontSize = 32;
                    not_goalslimetext.text = "<color=red>※最低でもゴールは1つの設置、そして兄弟スライムは両方\"1匹ずつ\"配置</color>";
                }
                else
                {
                    not_goalslimetext.fontSize = 28;
                    not_goalslimetext.text = "<color=red>*At least one goal should be set up, and both sibling slimes should be placed one at a time.</color>";
                }
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cooltime <= 0 && (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))&&entertrg)
        {
            cooltime = 1f;
            ClickBoard();
        }
        if (!wheelstrg)
            wheelstrg = true;

    }
    public void OnPointerExit(PointerEventData eventDara)
    {
        if (wheelstrg)
            wheelstrg = false;
    }
    void SceneChange()
    {
        SceneManager.LoadScene("loadstage");
    }
}