using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class datatest : MonoBehaviour
{
    public string class_name = "StageDataClass";
    public string field_name = "stagedata";
    public int query_limit = 200;
    public List<string> all_namelist = null;
    public List<string> all_versionlist = null;
    public List<string> all_datalist = null;
    public GameObject[] all_btn;
    public GameObject summon_btn;
    [Multiline]
    private string result_tmp;

    public CustomBool cbool;
    public Text onsearch_word;
    public Text notsearch_word;
    public List<NCMBObject> tmp_objlist = null;
    void Start()
    {
        //// クラスのNCMBObjectを作成
        //NCMBObject testClass = new NCMBObject("TestClass");

        //// オブジェクトに値を設定

        //testClass["message"] = "Hello, NCMB!";
        //// データストアへの登録
        //testClass.SaveAsync();
        FetchStage();
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }
    public void FetchStage()
    {
        GManager.instance.setrg = 0;
        //ボタン配置初期化
        if (all_btn != null && all_btn.Length  > 0)
        {
            for (int b = 0; b < all_btn.Length;)
            {
                Destroy(all_btn[b].gameObject);
                b++;
            }
        }
        all_btn = null;
        //取得から配置
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(class_name);

        //Scoreフィールドの降順でデータを取得
        if (GManager.instance.sorttrg )
        {
            query.OrderByDescending("stagedata");
        }
        else if (!GManager.instance.sorttrg)
        {
            query.OrderByAscending("stagedata");
        }


        //検索件数を5件に設定
        query.Limit = query_limit;

        //データストアでの検索を行う
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                GManager.instance.setrg = 1;
                //検索失敗時の処理
            }
            else
            {
                all_btn = new GameObject[objList.Count];
                int i = 0;
                //検索成功時の処理
                // 取得したレコードをHighScoreクラスとして保存
                foreach (NCMBObject obj in objList)
                {
                    
                    string tmpdata = System.Convert.ToString(obj["stagedata"]);
                    all_datalist.Add(tmpdata);
                    TempRead(tmpdata);
                    all_btn[i] = Instantiate(summon_btn, transform.position, transform.rotation, transform);
                    //---
                    StringReader rs = new StringReader(result_tmp);
                    string line = null;
                    int check_line = 0;

                    while ((line = rs.ReadLine()) != null)//本作では1行読み込んでステージデータかどうか判別してる
                    {
                        if (check_line == 1)
                        {
                            if (line.Contains("-1")) all_versionlist.Add("共通");
                            else all_versionlist.Add(line);
                        }
                        else if (check_line == 2)
                        {
                            all_namelist.Add(line);
                        }
                        check_line += 1;
                    }
                    result_tmp = "";
                    i ++;
                }
                for(int ch = 0; ch < all_btn.Length;)
                {
                    all_btn[ch].SetActive(true);
                    ch++;
                }
                //検索調整
                if (all_btn != null && all_btn.Length > 0)
                {
                    for (int b = 0; b < all_btn.Length;)
                    {
                        bool on_name = all_namelist[b].Contains(GManager.instance.check_onword);
                        bool on_ver = all_versionlist[b].Contains(GManager.instance.check_onword);
                        bool off_name = all_namelist [b].Contains(GManager.instance.check_notword);
                        bool off_ver = all_versionlist[b].Contains(GManager.instance.check_notword);
                        if ( GManager.instance.check_onword != "" && !on_name && !on_ver)
                            Destroy(all_btn[b].gameObject);
                        else if( GManager.instance.check_notword != "" &&(off_name  || off_ver))
                            Destroy(all_btn[b].gameObject);
                        b++;
                    }
                }
            }
        });
        
    }
    public void FetchReload()
    {
        GManager.instance.setrg = 0;
        GManager.instance.check_onword = onsearch_word.text;
        GManager.instance.check_notword = notsearch_word.text;
        Instantiate(GManager.instance.all_ui[0], transform.position, transform.rotation);
        Invoke("SceneChange", 1f);
    }
    void SceneChange()
    {
        SceneManager.LoadScene("qrstage");
    }
    public void TempRead(string _result = "")
    {
        string tmp = "";
        //print(_result);
        string[] arrayStr2 = _result.Split('-');
        byte[] arrayOut = new byte[arrayStr2.Length];
        for (int i = 0; i < arrayStr2.Length;)
        {
            // 16進数文字列に変換
            arrayOut[i] = Convert.ToByte(arrayStr2[i], 16);
            i++;
        }
        //print(GManager.instance.DeComporessGZIP(arrayOut));
        tmp = GManager.instance.DeComporessGZIP(arrayOut);
        result_tmp = tmp;
    }
}
