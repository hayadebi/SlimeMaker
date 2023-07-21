using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System.IO;
using System;

public class DataBuySystem : MonoBehaviour
{
    public int get_buytype = 0;
    public Text check_text;
    private GameObject jackshop = null;
    private StoreManager storem;
    private float buyprice = 0f;
    private string buyname="";
    public string buyend_save = "";
    public GameObject buyend_targetobj = null;
    public NCMBObject deltarget_data = null;
    public GameObject OffUI = null;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("online-jackshop")) jackshop = GameObject.Find("online-jackshop");
        if (jackshop != null && jackshop.GetComponent<StoreManager>()) storem = jackshop.GetComponent<StoreManager>();
        //文章改変・翻訳
        Invoke(nameof(UpdateText), 0.1f);
    }
    void UpdateText()
    {
        if (get_buytype == 0)
        {
            buyprice = 18;
            if (GManager.instance.isEnglish == 0)
            {
                check_text.text = "18デビコイン消費して【DXコンテンツ】\nを購入しようとしています。\n貴重なデビコインを消費して本当に購入しますか？";
            }
            else
            {
                check_text.text = "I am about to spend 18 devilcoin \nto purchase【DX Contents】.\n Do you really want to spend your precious devilcoins to purchase it?";
            }
        }
        else if (get_buytype == 1)
        {
            buyprice = 6;
            if (GManager.instance.isEnglish == 0)
            {
                check_text.text = "6デビコイン消費して【ギミックセット第1弾】\nを購入しようとしています。\n貴重なデビコインを消費して本当に購入しますか？";
            }
            else
            {
                check_text.text = "I am about to spend 6 devilcoin \nto purchase【Gimmick Set #1】.\n Do you really want to spend your precious devilcoins to purchase it?";
            }
        }
    }
    public void BuyBtn()
    {
        if (storem != null)
        {
            GManager.instance.setrg = 6;
            storem.BuyAddData(-buyprice);
            PlayerPrefs.SetString(buyend_save, "true");
            PlayerPrefs.Save();
            
            if (get_buytype == 0)
            {
                GManager.instance.dx_mode = true;
                PlayerPrefs.SetString("notdxtrg", "TRUE");
                PlayerPrefs.SetInt("daily_year", DateTime.Today.Year);
                PlayerPrefs.SetInt("daily_month", DateTime.Today.Month);
                PlayerPrefs.SetInt("daily_day", DateTime.Today.Day);
                PlayerPrefs.SetInt("daily_hour", DateTime.Now.Hour);
                PlayerPrefs.SetInt("daily_min", DateTime.Now.Minute);
                PlayerPrefs.SetInt("daily_sec", DateTime.Now.Second);
                PlayerPrefs.Save();
            }
            else if (get_buytype == 1)
            {
                var oldonset = PlayerPrefs.GetString("all_onset", "");
                PlayerPrefs.SetString("all_onset", oldonset+"48\n49\n64\n65\n66\n");
                PlayerPrefs.Save();
            }
            if (buyend_targetobj != null)
            {
                buyend_targetobj.SetActive(false);
            }
            OffUI.SetActive(false);
        }
    }
    public void OffThisUI()
    {
        OffUI.SetActive(false);
    }
}
