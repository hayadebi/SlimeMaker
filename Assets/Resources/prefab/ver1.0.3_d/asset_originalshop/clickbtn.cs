using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class clickbtn : MonoBehaviour
{
    public string settingtype = "";
    public float addsettingfloat;
    public int addsettingint;
    public bool menutrg = false;
    public bool addstage = false;
    public bool stagetrg = false;
    public bool resettrg = false;
    public float maxUI = 0;
    public string nextscene;
    public GameObject settingUI;
    public GameObject fadeinUI;
    public AudioClip clickse;
    AudioSource audioSource;
    public bool subTrg = false;
    private int oldrandom = 0;
    public Animator anim_;
    public string a_name;
    public int a_setnumber;
    [Header("課金用")]
    public bool mpurseuser_trg = false;
    public clickbutton usercheck=null;
    public int set_buyid = -1;
    public int limit_onviewmax = -1;
    [Header("0=アイテム,1=クラフトレシピ")]
    public int set_buytype = -1;

    public string targeturl = "";

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void animClick()
    {
        if(anim_)
        {
            GManager.instance.setrg = 6;
            anim_.SetInteger(a_name, a_setnumber);
        }
    }
    public void CheckUser()
    {
        string tmp = targeturl;
        Application.OpenURL(tmp);
        this.gameObject.SetActive(false);
    }
    public void settingClick()
    {
        if (!mpurseuser_trg || (mpurseuser_trg && ShopManager.instance.mpurseuser_on))
        {
            GameObject tmpobj=null;
            if (GManager.instance.setmenu < maxUI && GManager.instance.walktrg == true)
            {
                GManager.instance.setmenu += 1;
                GManager.instance.walktrg = false;
                audioSource.PlayOneShot(clickse);
                    tmpobj=Instantiate(settingUI, transform.position, transform.rotation);
            }
            else if (GManager.instance.setmenu < maxUI && GManager.instance.setmenu > 0)
            {
                GManager.instance.setmenu += 1;
                GManager.instance.walktrg = false;
                audioSource.PlayOneShot(clickse);
                    tmpobj = Instantiate(settingUI, transform.position, transform.rotation);
            }
            else if (GManager.instance.setmenu < maxUI && GManager.instance.walktrg == false)
            {
                GManager.instance.setmenu += 1;
                GManager.instance.walktrg = false;
                audioSource.PlayOneShot(clickse);
                    tmpobj = Instantiate(settingUI, transform.position, transform.rotation);
            }
            if (set_buyid >= 0) ShopManager.instance.select_buyid = set_buyid;
            if (set_buytype >= 0 && tmpobj) tmpobj.GetComponent<DataBuySystem>().get_buytype = set_buytype;
        }
        else
        {
            if (usercheck != null) usercheck.gameObject.SetActive(true);
            GManager.instance.setrg = 27;
        }
    }
    public void NoSetThis()
    {
        GManager.instance.ESCtrg = true;
    }
    public void CheckOnView()
    {
        if (!mpurseuser_trg || (mpurseuser_trg && ShopManager.instance.mpurseuser_on))
        {
            if (limit_onviewmax == -1 || (limit_onviewmax > 0 && PlayerPrefs.GetInt("DayAds", 0) < 5))
            {
                var tmp = PlayerPrefs.GetInt("DayAds", 0) + 1;
                PlayerPrefs.SetInt("DayAds", tmp);
                PlayerPrefs.Save();
                settingUI.SetActive(true);
            }
            else
            {
                GManager.instance.setrg = 27;
                if (this.GetComponent<Image>())
                {
                    Image tmpimg = this.GetComponent<Image>();
                    Color tmpcl = tmpimg.color;
                    tmpcl.a /= 2;
                    tmpimg.color = tmpcl;
                }

            }
        }
        else
        {
            if (usercheck != null) usercheck.gameObject.SetActive(true);
            GManager.instance.setrg = 27;
        }
    }
    public void ChildOnView()
    {
        if (!mpurseuser_trg || (mpurseuser_trg && ShopManager.instance.mpurseuser_on))
        {
            if (limit_onviewmax == -1 || (limit_onviewmax > 0 && PlayerPrefs.GetInt("DayAds", 0) < 5))
            {
                var tmp = PlayerPrefs.GetInt("DayAds", 0) + 1;
                PlayerPrefs.SetInt("DayAds", tmp);
                PlayerPrefs.Save();
                Instantiate(settingUI, fadeinUI.transform.position, transform.rotation, fadeinUI.transform);
            }
            else
            {
                GManager.instance.setrg = 27;
                if (this.GetComponent<Image>())
                {
                    Image tmpimg = this.GetComponent<Image>();
                    Color tmpcl = tmpimg.color;
                    tmpcl.a /= 2;
                    tmpimg.color = tmpcl;
                }

            }
        }
        else
        {
            if (usercheck != null) usercheck.gameObject.SetActive(true);
            GManager.instance.setrg = 27;
        }
    }
    public void CheckNoView()
    {
        this.gameObject.SetActive(false);
    }

    public void quitClick()
    {
        Application.Quit();
    }
    public void DestroyClick()
    {
        GManager.instance.walktrg = true;
        Destroy(gameObject);
    }
    
}
