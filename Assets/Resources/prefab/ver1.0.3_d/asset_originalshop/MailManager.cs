using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.IO;
using System;
using UnityEngine.UI;

public class MailManager : MonoBehaviour
{
    public bool destroytrg = true;
    public string class_name = "GameNews";
    private string check_name = "2匹はスライム兄弟メーカー";
    private int query_limit = 99;
    public GameObject setuiobj;
    // Start is called before the first frame update
    void Start()
    {
        FetchStage();
    }
    public void FetchStage()
    {
        //ユーザーチェック
        NCMBQuery<NCMBObject> query = null;
        query = new NCMBQuery<NCMBObject>(class_name);
        query.OrderByDescending("gametype");
        //検索件数を設定
        query.Limit = query_limit;
        //データストアでの検索を行う
        int i = 0;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Destroy(gameObject);
            }
            else
            {
                //検索成功時の処理
                foreach (NCMBObject obj in objList)
                {
                    if ((obj["gametype"].ToString() == check_name || obj["gametype"].ToString() == "共通")) //&& ((bool)obj["isdx"] == false || ((bool)obj["isdx"] == true && GManager.instance.dx_mode)))
                    {
                        GameObject tmpobj = Instantiate(setuiobj, transform.position, transform.rotation, transform);
                        ChildMail tmpchild = tmpobj.GetComponent<ChildMail>();
                        tmpchild.tmpchildobj = obj.ObjectId.ToString();
                        tmpchild.bonus_trg = false;
                        tmpchild.movetrg = true;
                        tmpchild.parenttrg = false;

                    }
                    i++;
                }
                setuiobj.SetActive(false);
            }
        });
    }
    private void Update()
    {
        ;
    }
}
