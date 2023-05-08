using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.IO;
using System;
using UnityEngine.UI;

public class dataranking : MonoBehaviour
{
    private string class_name = "Ev1_Stage1";
    private string field_name = "ct_ranking";
    public int query_limit = 50;
    private List<string> all_namelist = null;
    private List<string> all_timelist = null;
    private List<string> all_datalist = null;
    public InputField get_namefield;
    public Text set_rankingtext;
    private List<NCMBObject> tmp_objlist = null;
    private NCMBObject CreateClass=null;
    [Multiline]
    public string none_ranking;
    private List<float> rank_time;
    private List<int> rank_index;
    // Start is called before the first frame update
    void Start()
    {
        if (GManager.instance.globalev_id != -1 && GManager.instance.globalev_stageselect != -1)
        {
            class_name = "Ev" + GManager.instance.globalev_id.ToString() + "_Stage" + GManager.instance.globalev_stageselect.ToString();
            FetchStage();
        }
    }

    public void ClickRnakingSet()
    {
        GManager.instance.setrg = 0;
        if(CreateClass==null)
            CreateClass = new NCMBObject(class_name);
        bool tmp_notword = false;
        for (int w = 0; w < GManager.instance.not_word.Length;)
        {
            if (get_namefield.text.Contains(GManager.instance.not_word[w]))
            {
                tmp_notword = true;
                break;
            }
            w++;
        }
        if(!tmp_notword )
        {
            ;
        }
        else if(tmp_notword)
        {
            if(GManager.instance.isEnglish == 0)
                get_namefield.text = "※規制された名前です";
            else
                get_namefield.text = "※Regulated name.";
        }
        CreateClass[field_name] = get_namefield.text +"\n"+GManager.instance.cleartime.ToString ();
        CreateClass.SaveAsync();
    }
    public void FetchStage()
    {
        GManager.instance.setrg = 0;
        //取得から配置
        NCMBQuery<NCMBObject> query = null;
        query = new NCMBQuery<NCMBObject>(class_name);
        if (query == null)
        {
            CreateClass = new NCMBObject(class_name);
        }
        //Scoreフィールドの降順でデータを取得
        if (GManager.instance.sorttrg)
        {
            query.OrderByDescending(field_name);
        }
        else if (!GManager.instance.sorttrg)
        {
            query.OrderByAscending(field_name);
        }


        //検索件数を設定
        query.Limit = query_limit;

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
                int i = 0;
                //検索成功時の処理
                foreach (NCMBObject obj in objList)
                {
                    string tmpdata = obj[field_name].ToString();
                    all_datalist.Add(tmpdata);
                    string[] rs = tmpdata.Split("\n".ToCharArray()); 
                    for (int l=0;l<rs.Length;)
                    {
                        if (l == 0)
                        {
                            all_namelist.Add(rs[l]);
                        }
                        else if (l == 1)
                        {
                            all_timelist.Add(rs[l]);
                        }
                        l++;
                    }
                    i++;
                }
                for(int h = 0; h < all_timelist.Count;)
                {

                    h++;
                }
            }
        });

    }
}
