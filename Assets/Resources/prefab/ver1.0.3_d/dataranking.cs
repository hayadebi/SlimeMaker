using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.IO;
using System;
using UnityEngine.UI;

public class dataranking : MonoBehaviour
{
    private string class_name = "DX0";
    private string field_name = "ct_ranking";
    private string pname_name = "player_name";
    private string time_name = "clear_time";
    private string view_time = "time_text";
    public int query_limit = 50;
    public InputField get_namefield;
    public Text set_rankingtext;
    private List<NCMBObject> tmp_objlist = null;
    private NCMBObject CreateClass = null;
    [Multiline]
    public string none_ranking;
    public UItext ut;
    private List<float> rank_time;
    private List<int> rank_index;
    private bool rankintrg = false;
    public string appkey_set = "6389c8004926f5e7281c224c976dedfaea075e8d22898f17912ae056a104cdd0";
    public string clientkey_set = "d007162d93318e4054fe5893ad770e5d761c358949dfd05ceb5e2d59f0944449";
    // Start is called before the first frame update
    private void Awake()
    {
        if(NCMBSettings.ApplicationKey != appkey_set) NCMBSettings.ApplicationKey = appkey_set;
        if(NCMBSettings.ClientKey != clientkey_set) NCMBSettings.ClientKey = clientkey_set;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GManager.instance.globalev_id != -1)
        {
            class_name = "DX" + GManager.instance.dx_stageid.ToString();
            FetchStage();
        }
    }

    public void ClickRnakingSet()
    {
        if (!rankintrg)
        {
            Resources.UnloadUnusedAssets();
            rankintrg = true;
            GManager.instance.setrg = 0;
            
            bool tmp_notword = false;
            if (get_namefield.text == "") get_namefield.text = "No name";
            for (int w = 0; w < GManager.instance.not_word.Length;)
            {
                if (get_namefield.text.Contains(GManager.instance.not_word[w]))
                {
                    tmp_notword = true;
                    break;
                }
                w++;
            }
            bool notname = false;
            if (!tmp_notword)
            {
                ;
            }
            else if (tmp_notword)
            {
                if (GManager.instance.isEnglish == 0)
                    get_namefield.text = "※規制された名前です";
                else
                    get_namefield.text = "※Regulated name.";
                notname = true;
                GManager.instance.setrg = 1;
                rankintrg = false;
            }
            if (!notname)
            {
                CreateClass = new NCMBObject(class_name);
                CreateClass[pname_name] = get_namefield.text;
                CreateClass[time_name] = ut.tmp_cleartime;
                CreateClass[view_time] = ut._text.text;
                CreateClass.SaveAsync();
                FetchStage();
            }
        }
        else GManager.instance.setrg = 1;
    }
    public void FetchStage()
    {
        GManager.instance.setrg = 0;
        //取得から配置
        NCMBQuery<NCMBObject> query = null;
        query = new NCMBQuery<NCMBObject>(class_name);
        //Scoreフィールドの降順でデータを取得
        query.OrderByAscending(time_name);
        //検索件数を設定
        query.Limit = query_limit;
        int i = 1;
        //データストアでの検索を行う
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //検索失敗時の処理
                set_rankingtext.text = none_ranking;
            }
            else
            {
                set_rankingtext.text = "";
                //検索成功時の処理
                foreach (NCMBObject obj in objList)
                {
                    set_rankingtext.text += i.ToString() + "." + obj[pname_name].ToString() + "  " +obj[view_time]+"\n";
                    i +=1;
                }
            }
        });

    }
}
