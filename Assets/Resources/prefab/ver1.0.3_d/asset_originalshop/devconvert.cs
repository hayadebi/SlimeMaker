using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System.IO;
using System;
public class devconvert : MonoBehaviour
{
    private AudioSource audioS;
    public AudioClip getse;
    public int inputshopID;
    private float cooltime;
    public GameObject BuyCheckUI;
    public Text getdevcoin_text;
    public Transform parentobj = null;
    public GameObject buyend_targetobj;
    public string buyend_text = "dxcontents_buy";
    public bool dxcheck = true;
    public float set_price=18;
    public GameObject NotUI;
    // Start is called before the first frame update
    void Start()
    {
        audioS = this.GetComponent<AudioSource>();
        if (parentobj == null) parentobj = this.transform;
        if ((buyend_text!="" && PlayerPrefs.GetString(buyend_text,"false")=="true")||(dxcheck && GManager.instance.dx_mode)) buyend_targetobj.SetActive(false);
        //SetUIConvert();
    }
    void SetUIConvert()
    {
        getdevcoin_text.text = ShopManager.instance.get_devcoin.ToString() + "DC×";
    }
    // Update is called once per frame
    void Update()
    {
        if (cooltime >= 0)
        {
            cooltime -= Time.deltaTime;
        }
    }
    public void ShopPlay()
    {
        if (cooltime <= 0 && set_price <= ShopManager.instance.get_devcoin && ShopManager.instance.mpurseuser_on)
        {
            cooltime = 2f;
            audioS.PlayOneShot(getse);
            GManager.instance.setmenu += 1;
            //処理
            GameObject tmpobj = Instantiate(BuyCheckUI, parentobj.transform.position, parentobj.transform.rotation, parentobj.transform);
            ShopManager.instance.select_buyid = inputshopID;
            var tmpbuy = tmpobj.GetComponent<DataBuySystem>();
            tmpbuy.get_buytype = inputshopID;
            tmpbuy.buyend_save = buyend_text;
            tmpbuy.buyend_targetobj= buyend_targetobj;
        }
        else
        {
            GManager.instance.setrg = 27;
            if (!ShopManager.instance.mpurseuser_on) NotUI.SetActive(true);
        }
    }
}
