using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.IO;
using System;
using UnityEngine.UI;
public class ChildMail : MonoBehaviour
{
    public bool bonus_trg = false;
    public Text objtitletext;
    public Image ischeck_icon;
    public bool movetrg = false;
    private bool oldmove = false;
    private NCMBObject child_obj;
    public bool parenttrg = true;
    private AudioSource audioS;
    public AudioClip onse;
    [Header("以下メッセージ内容について")]
    public GameObject checkUI;
    public Text messagetitletext;
    public Text messagedoctext;
    public transmailopen transbtn;
    public string tmpchildobj;
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!parenttrg && movetrg && movetrg != oldmove && tmpchildobj != "")
        {
            oldmove = movetrg;
            child_obj = new NCMBObject("GameNews");
            child_obj.ObjectId = tmpchildobj;
            child_obj.FetchAsync();
            Invoke(nameof(CoolMail), 0.31f);
        }
    }
    public void CoolMail()
    {
        oldmove = movetrg;
        objtitletext.text = child_obj["messagetitle"].ToString();
        ischeck_icon.enabled = true;
        if (PlayerPrefs.GetString(child_obj.ObjectId.ToString(), "false") == "false")
        {
            ;
        }
        else
        {
            ischeck_icon.enabled = false;
        }
        if ("YpY9012nWJzXaBuS" == tmpchildobj && PlayerPrefs.GetString(tmpchildobj, "false") == "true") Destroy(gameObject);
    }
    public void MailOpen()
    {
        audioS.PlayOneShot(onse);
        //処理
        checkUI.SetActive(true);
        if(messagetitletext!=null) messagetitletext.text = child_obj["messagetitle"].ToString();
        if(messagedoctext!=null) messagedoctext.text = child_obj["messagedoc"].ToString();
        if(transbtn!=null) transbtn.targetchild = this.GetComponent<ChildMail>();
    }
    public void MailBonus()
    {
        if (bonus_trg)
        {
            bonus_trg = false;
            GManager.instance.setrg = 27;
        }
        else
        {
            GManager.instance.setrg = 27;
        }
    }
}
