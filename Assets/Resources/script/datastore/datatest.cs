using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.IO;

public class datatest : MonoBehaviour
{
    public string class_name = "StageDataClass";
    public string field_name = "stagedata";
    public bool descending = true;
    public int query_limit = 99;
    public string[] all_namelist =null;
    public string[] all_versionlist = null;
    public List<string> all_datalist = null;
    [System.Serializable]
    public struct StageData
    {
        public string stage_version;
        public string stage_name;
        public string stage_data;
    }
    public StageData[] AllStage=null;
    public GameObject[] all_btn;
    public GameObject summon_btn;
    private datatest selfdata;
    // Start is called before the first frame update
    void Start()
    {
        selfdata = this.GetComponent<datatest>();
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
        //ボタン配置初期化
        if(all_btn!=null && all_btn.Length > 0)
        {
            for(int b = 0; b < all_btn.Length;)
            {
                Destroy(all_btn[b].gameObject);
                b++;
            }
        }
        all_btn = null;
        //取得から配置
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(class_name);

        //Scoreフィールドの降順でデータを取得
        if(descending)
            query.OrderByDescending(field_name);
        else if (!descending)
            query.OrderByAscending(field_name);

        //検索件数を5件に設定
        query.Limit = query_limit;

        //データストアでの検索を行う
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
            if (e != null)
            {
                GManager.instance.setrg = 1;
                //検索失敗時の処理
            }
            else
            {
                all_btn = new GameObject[objList.Count];
                all_namelist = new string[objList.Count];
                all_versionlist = new string[objList.Count];
                //
                int count_id = 0;
                //検索成功時の処理
                // 取得したレコードをHighScoreクラスとして保存
                foreach (NCMBObject obj in objList)
                {
                    string tmpdata = System.Convert.ToString(obj["stagedata"]);
                    all_datalist.Add(tmpdata);
                    string line = null;
                    int check_line = 0;
                    StringReader strReader = new StringReader(tmpdata);
                    while ((line = strReader.ReadLine()) != null)
                    {
                        all_btn[count_id] = Instantiate(summon_btn, transform.position, transform.rotation, transform);
                        child_data tmp_cdata = all_btn[count_id].GetComponent<child_data>();
                        tmp_cdata.stage_alldata = tmpdata;
                        if (check_line == 1 && (line == Application.version || line == "-1"))
                        {
                            if(line == Application.version) all_versionlist[count_id ]=line;
                            else if (line == "-1") all_versionlist[count_id] = "共通";
                            if (line == Application.version) tmp_cdata.stage_version=line;
                            else if (line == "-1") tmp_cdata.stage_version = "共通";
                        }
                        else if(check_line == 2)
                        {
                            all_namelist[count_id] = line;
                            tmp_cdata.stage_name = line;
                        }
                        tmp_cdata.child_id = count_id;
                        tmp_cdata.parent_data = selfdata;
                        tmp_cdata.ChildDataSet();
                        check_line += 1;
                    }
                    count_id++;
                }
            }
        });
    }
}
