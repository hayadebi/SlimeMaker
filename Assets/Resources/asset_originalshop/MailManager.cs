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
    public string check_name = "2匹はスライム兄弟メーカー";
    private int query_limit = 99;
    public NCMBObject get_ncmbobj = null;
    public int[] getcoin_array = { 5, 10, 20, 40, 80, 120, 160 };
    public int[] getitemid_array = { 0, 1, 2, 3, 10, 20, 19,-1 };
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
        query.OrderByDescending(check_name);
        //検索件数を設定
        query.Limit = query_limit;
        //データストアでの検索を行う
        int i = 0;
        DateTime tmpdays = GManager.instance.GetGameDay();
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
                    if ((bool)obj["isdx"]==false ||((bool)obj["isdx"] == true && GManager.instance.dx_mode))
                    {
                        if (obj.ObjectId == "YpY9012nWJzXaBuS" && PlayerPrefs.GetString(obj.ObjectId, "false") == "false")
                        {
                            ShopManager.instance.tmpchildobj = obj.ObjectId;
                            GameObject tmpobj = Instantiate(setuiobj, transform.position, transform.rotation, transform);
                            ChildMail tmpchild = tmpobj.GetComponent<ChildMail>();
                            long tmpitemnum = (long)obj["additemid"];
                            long tmpcoinnum = (long)obj["addnormalcoin"];
                            if (((int)tmpitemnum != -1 || (int)tmpcoinnum > 0) && PlayerPrefs.GetString(obj.ObjectId.ToString(), "false") == "false")
                            {
                                tmpchild.bonus_trg = true;
                            }
                            else tmpchild.bonus_trg = false;
                            tmpchild.movetrg = true;
                            tmpchild.parenttrg = false;
                        }
                        else
                        {
                            ShopManager.instance.tmpchildobj = obj.ObjectId;
                            GameObject tmpobj = Instantiate(setuiobj, transform.position, transform.rotation, transform);
                            ChildMail tmpchild = tmpobj.GetComponent<ChildMail>();
                            long tmpitemnum = (long)obj["additemid"];
                            long tmpcoinnum = (long)obj["addnormalcoin"];
                            if (((int)tmpitemnum != -1 || (int)tmpcoinnum > 0) && PlayerPrefs.GetString(obj.ObjectId.ToString(), "false") == "false")
                            {
                                tmpchild.bonus_trg = true;
                            }
                            else tmpchild.bonus_trg = false;
                            tmpchild.movetrg = true;
                            tmpchild.parenttrg = false;

                        }
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
