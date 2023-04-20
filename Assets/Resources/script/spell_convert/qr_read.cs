using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
public class qr_read : MonoBehaviour
{
    string _result = null;
    WebCamTexture _webCam;
    public RawImage raw;
    private bool check_trg = false;
    private string global_temp1;
    private string global_temp2;
    public InputManager input_manager;
    public Animator anim;
    public Image btimg;
    public Text bttxt;
    private bool is_start = false;
    public AudioSource audioSource;
    public AudioClip[] se;
    public Text qrtxt;
    private bool not_lost = false;
    public GameObject spellEffect;
    public GameObject summonEffect;
    public Transform summonPos;
    private GameObject summon_monster;
    private Animator summon_anim;
    public GameObject state_UI;
    public void ClickQR()
    {
        StartCoroutine(nameof(QRStart));
    }
    public IEnumerator QRStart()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        anim.SetInteger("Anumber",1);
        btimg.enabled = false;
        bttxt.enabled = false;
        audioSource.PlayOneShot(se[0]);
        GManager.instance.setmenu = 1;
        if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
        {
            Debug.LogFormat("no camera.");
            yield break;
        }
        Debug.LogFormat("camera ok.");
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices == null || devices.Length == 0)
            yield break;
        _webCam = new WebCamTexture(devices[0].name, Screen.width, Screen.height, 12);
        raw.texture = _webCam;
        _webCam.Play();
        is_start = true;
    }
    void AnimReset()
    {
        summon_anim.SetInteger("Anumber", 0);
    }
    void Update()
    {
        if (is_start)
        {
            if (!check_trg && _webCam != null)
            {
                _result = QRCodeHelper.Read(_webCam);
                if (_result != null && QRCodeHelper.Read(_webCam) != null && _result != "error" && !check_trg)
                {
                    print(_result);
                    GManager.instance.dungeon_player.qr_pass = _result;
                    global_temp1 = new string(_result.Reverse().ToArray());
                    print(global_temp1);
                    global_temp2 = input_manager.OnClickButton01(global_temp1);
                    print(global_temp2);
                    string temp_check = SetState(global_temp2);
                    if (temp_check == "false" && !not_lost )
                    {
                        not_lost = true;
                        print("ロストしたQRだよ");
                        audioSource.PlayOneShot(se[2]);
                        qrtxt.text = "召喚したい使い魔の魔法陣(QRコード)を映してね！\n<color=red><size=22>※一度でも死亡、または見逃した使い魔のQRコードは読み込みません</size></color>";
                    }
                    else if (temp_check == "true")
                    {
                        print("通ったよ");
                        check_trg = true;
                        spellEffect.SetActive(false);
                        Instantiate(summonEffect, summonPos.position, summonPos.rotation);
                        summon_monster = Instantiate(GManager.instance.all_monster[GManager.instance.dungeon_player.monster_id].mon_obj, transform.position, transform.rotation);
                        summon_anim = summon_monster.GetComponent<Animator>();
                        summon_anim.SetInteger("Anumber", -1);
                        state_UI.SetActive(true);
                        audioSource.PlayOneShot(se[1]);
                        GManager.instance.setmenu = 0;
                        anim.SetInteger("Anumber", 0);
                        Invoke("AnimReset", 1.3f);
                    }
                }
            }
            string SetState(string temptext = "")
            {
                string view_text = "";
                for (int i = 0; i < GManager.instance.lost_monster.Length;)
                {
                    if (GManager.instance.lost_monster[i] == GManager.instance.dungeon_player.qr_pass)
                    {
                        view_text = "false";
                        break;
                    }
                    i++;
                }
                if (view_text != "false")
                {
                    char[] temp_list = temptext.ToCharArray();

                    int _id = 0;
                    int rm_n = 10;
                    for (int i = 10; i < temp_list.Length;)
                    {
                        GManager.instance.temptext = temp_list[i].ToString();
                        if (GManager.instance.temptext == "0" || GManager.instance.temptext == "1" || GManager.instance.temptext == "2" || GManager.instance.temptext == "3" || GManager.instance.temptext == "4" || GManager.instance.temptext == "5" || GManager.instance.temptext == "6" || GManager.instance.temptext == "7" || GManager.instance.temptext == "8" || GManager.instance.temptext == "9" || GManager.instance.temptext == "10" || GManager.instance.temptext == "11")
                        {
                            rm_n += 1;
                        }
                        else
                        {
                            if (i - rm_n == 0)//種族
                            {
                                int temp = input_manager.StateConverter(GManager.instance.all_monster.Length);
                                _id = temp;
                                GManager.instance.dungeon_player.monster_id = temp;
                            }
                            else if (i - rm_n == 1)//体力
                            {
                                int temp = input_manager.StateConverter(16);
                                GManager.instance.dungeon_player.max_hp = GManager.instance.all_monster[_id].hp + temp;
                                GManager.instance.dungeon_player.hp = GManager.instance.dungeon_player.max_hp;
                            }
                            else if (i - rm_n == 2)//攻撃力
                            {
                                int temp = input_manager.StateConverter(8);
                                GManager.instance.dungeon_player.at = GManager.instance.all_monster[_id].at + temp;
                            }
                            else if (i - rm_n == 3)//防御力
                            {
                                int temp = input_manager.StateConverter(4);
                                GManager.instance.dungeon_player.df = GManager.instance.all_monster[_id].df + temp;
                            }
                            else if (i - rm_n == 4)//素早さ
                            {
                                int temp = input_manager.StateConverter(2);
                                GManager.instance.dungeon_player.speed = GManager.instance.all_monster[_id].speed + temp;
                            }
                            else if (i - rm_n == 5)//射程範囲
                            {
                                int temp = input_manager.StateConverter(2);
                                GManager.instance.dungeon_player.at_area = GManager.instance.all_monster[_id].at_area + temp;
                            }
                            else if (i - rm_n == 6)//連射速度
                            {
                                int temp = input_manager.StateConverter(2);
                                GManager.instance.dungeon_player.at_speed = GManager.instance.all_monster[_id].at_speed + temp;
                            }
                            else if (i - rm_n == 7)//成長速度
                            {
                                int temp = input_manager.StateConverter(1);
                                GManager.instance.dungeon_player.lvup_speed = GManager.instance.all_monster[_id].lvup_speed + temp;
                            }
                            //"才能技"
                            else if (i - rm_n == 9 && GManager.instance.all_monster[_id].get_specalatid != null && GManager.instance.all_monster[_id].get_specalatid.Length > 0)
                            {
                                int temp = input_manager.StateConverter(GManager.instance.all_monster[_id].get_specalatid.Length);
                                if (GManager.instance.all_monster[_id].get_specalatid[temp] != -1)
                                {
                                    GManager.instance.dungeon_player.special_spell = GManager.instance.all_monster[_id].get_specalatid[temp];
                                    GManager.instance.all_attack[GManager.instance.all_monster[_id].get_specalatid[temp]].lv = 1;
                                }
                                else
                                {
                                    GManager.instance.dungeon_player.special_spell = -1;
                                }
                            }
                            //"種族技"
                            else if (i - rm_n == 10 && GManager.instance.all_monster[_id].normal_atid != null && GManager.instance.all_monster[_id].normal_atid.Length > 0)
                            {
                                int temp = input_manager.StateConverter(GManager.instance.all_monster[_id].normal_atid.Length);
                                if (GManager.instance.all_monster[_id].normal_atid[temp] != -1)
                                {
                                    GManager.instance.dungeon_player.normal_spell = GManager.instance.all_monster[_id].normal_atid[temp];
                                    GManager.instance.all_attack[GManager.instance.all_monster[_id].normal_atid[temp]].lv = 1;
                                }
                                else
                                {
                                    GManager.instance.dungeon_player.normal_spell = -1;
                                }
                            }
                            else if (i - rm_n == 11)//固有名
                            {
                                int temp = input_manager.StateConverter(GManager.instance.all_monster[_id].unique_namejp.Length);
                                GManager.instance.dungeon_player.unique_name[0] = GManager.instance.all_monster[_id].unique_namejp[temp];
                                GManager.instance.dungeon_player.unique_name[1] = GManager.instance.all_monster[_id].unique_nameen[temp];
                                break;
                            }
                        }
                        i += 1;
                    }
                    view_text = "true";
                    
                }
                return view_text;
            }
        }
    }
}