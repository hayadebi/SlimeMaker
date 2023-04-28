using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class child_data : MonoBehaviour
{
    public int child_id = 0;
    public datatest parent_data=null;
    public string stage_name;
    public string stage_version;
    public string stage_alldata;
    public Text nameText;
    public Text versionText;
    private qr_imgread serverread=null;
    public string readscript_name;
    // Start is called before the first frame update
    void Start()
    {
        serverread = GameObject.Find(readscript_name).GetComponent<qr_imgread >();
        parent_data = GameObject.Find("Content").GetComponent<datatest>();
        if(parent_data != null)
        {
            for(int i=0; i< parent_data.all_btn.Length;)
            {
                if(parent_data.all_btn[i] == this.gameObject)
                {
                    child_id = i;
                    stage_name = parent_data.all_namelist[i];
                    stage_version = parent_data.all_versionlist[i];
                    stage_alldata = parent_data.all_datalist[i];
                    ChildDataSet();
                }
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChildDataSet()
    {
        if (GManager.instance.isEnglish == 0)
        {
            nameText.text = "ステージ名：" + stage_name;
            versionText.text = "バージョン：" + stage_version;
        }
        else
        {
            nameText.text = "Stage name：" + stage_name;
            if(stage_version!="共通")
                versionText.text = "Version：" + stage_version;
            else if (stage_version == "共通")
                versionText.text = "Version：common";
        }
    }
    public void ClickStageServer()
    {
        GManager.instance.setrg = 0;
        try
        {
            serverread.ServerStageRead(stage_alldata);
        }
        catch(System.Exception )
        {
            GManager.instance.setrg = 1;
        }
    }
}
