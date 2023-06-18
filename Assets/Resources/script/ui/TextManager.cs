using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
namespace UniLang
{
    public class TextManager : MonoBehaviour
    {
        [Multiline]
        public string EnglishText="";
        int oldis = 0;
        [Multiline]
        string jpText;
        int jpFontSize;
        public int englishFontSize;
        public bool nosetTrg = false;
        public Image textimage = null;
        public Sprite textsprite = null;
        public Font enfont;
        private Font jpfont;
        public float checktime = 0f;
        private bool saymovetrg = true;
        public bool realtimeconvert = false;
        public bool humantrg = false;
        [Multiline]
        public string oldstring = "";
        void Start()
        {
            Text text = GetComponent<Text>();
            jpText = text.text;
            jpFontSize = text.fontSize;
            englishFontSize = jpFontSize / 4 * 3;
            jpfont = text.font;
            if (GManager.instance.isEnglish != 0 && !textimage && !text)
            {
                if (!humantrg)
                {
                    var nolang = jpText;
                    if (EnglishText != "") nolang = EnglishText;
                    string[] tmptext = nolang.ToCharArray().Select(c => new string(c, 1)).ToArray();
                    bool notcheck = false;
                    var kakonot = "";
                    for (int i = 0; i < tmptext.Length;)
                    {
                        if (tmptext[i] == "<") notcheck = true;
                        if (!notcheck) kakonot += tmptext[i];
                        else if (tmptext[i] == ">") notcheck = false;
                        i++;
                    }
                    kakonot =kakonot.Replace("\r", "").Replace("\n", "");
                    var translator = Translator.Create
                    (
                    Language.Auto,
                    GManager.instance.LanguageList[GManager.instance.isEnglish]
                    );
                    translator.Run(kakonot, results =>
                    {
                        foreach (var n in results)
                        {
                            text.text = "" + n.translated;
                        }
                    });
                }
                else
                {
                    text.text = EnglishText;
                }
                //---------------------------
                if (enfont != null)
                {
                    text.font = enfont;
                }
                {
                    text.font = jpfont;
                }
                if (englishFontSize != 0)
                {
                    text.fontSize = englishFontSize;
                }
                if (nosetTrg)
                {
                    this.gameObject.SetActive(false);
                }
            }
            else if (GManager.instance.isEnglish == 1 && textimage)
            {
                textimage.sprite = textsprite;
                if (nosetTrg)
                {
                    this.gameObject.SetActive(false);
                }
            }
            oldis = GManager.instance.isEnglish;
            checktime = 0;
        }
        void Update()
        {
            if (saymovetrg && !humantrg && GManager.instance.isEnglish != 0&& GetComponent<Text>() && oldstring != GetComponent<Text>().text)
                oldstring = GetComponent<Text>().text;
            else if (saymovetrg && !humantrg && GManager.instance.isEnglish != 0)
            {
                checktime += Time.deltaTime;
                if (checktime >= 1f)
                {
                    checktime = 0;
                    if (GManager.instance.isEnglish < 5)
                        oldis = GManager.instance.isEnglish + 1;
                    else
                        oldis = 0;
                }
            }
            if (oldis != GManager.instance.isEnglish && !textimage && !humantrg)
            {
                checktime = 0;
                Text text = GetComponent<Text>();
                if (GManager.instance.isEnglish != 0)
                {
                    var nolang = jpText;
                    if (EnglishText != "") nolang = EnglishText;
                    if (realtimeconvert) nolang = text.text;
                    string[] tmptext = nolang.ToCharArray().Select(c => new string(c, 1)).ToArray();
                    bool notcheck = false;
                    var kakonot = "";
                    for (int i = 0; i < tmptext.Length;)
                    {
                        if (tmptext[i] == "<") notcheck = true;
                        if (!notcheck) kakonot += tmptext[i];
                        else if (tmptext[i] == ">") notcheck = false;
                        i++;
                    }
                    kakonot=kakonot.Replace("\r", "").Replace("\n", "");
                    var translator = Translator.Create
                    (
                    Language.Auto,
                    GManager.instance.LanguageList[GManager.instance.isEnglish]
                    );
                    translator.Run(kakonot, results =>
                    {
                        foreach (var n in results)
                        {
                            if(text!=null) text.text = "" + n.translated;
                        }
                    });

                    if (enfont != null)
                    {
                        text.font = enfont;
                    }
                    else
                    {
                        text.font = jpfont;
                    }
                    if (englishFontSize != 0)
                    {
                        text.fontSize = englishFontSize;
                    }
                    if (nosetTrg)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
                else if (GManager.instance.isEnglish == 0)
                {
                    text.text = jpText;
                    text.font = jpfont;
                    if (englishFontSize != jpFontSize)
                    {
                        text.fontSize = jpFontSize;
                    }
                    if (nosetTrg)
                    {
                        this.gameObject.SetActive(true);
                    }
                }
                oldis = GManager.instance.isEnglish;
            }
        }
    }
}