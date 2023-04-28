using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    [Multiline]
    public string savetext = "";
    public qr_create qrcreate;
    public bool startqr=false;
    public GameObject loadui;
    public Text load_offtext;
    public GameObject server_onui;
    public GameObject server_offui;
    public void Start()
    {
        if(startqr)
            OutputRead();
    }
    public void OutputRead()
    {
        loadui.SetActive(true);
        string path = Application.persistentDataPath + "/stage00.txt";
        using (var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            string tmp = fs.ReadToEnd();
            string tmp2 = tmp;
            qrcreate.qr_content = GManager.instance.ComporessGZIP(tmp2);
        }
        //qrcreate.StartCoroutine("CreaterQR");
        try
        {
            qrcreate.CreateServer();
            load_offtext.enabled = false;
            server_onui.SetActive(true);
        }
        catch (System.Exception)
        {
            GManager.instance.setrg = 1;
            load_offtext.enabled = false;
            server_offui.SetActive(true);
        }
        
    }
    public void InputRead()
    {
        // 書き込み
        string path = Application.persistentDataPath + "/stage00.txt";
        bool isAppend = true; // 上書き or 追記
        using (var fs = new StreamWriter(path, isAppend, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            fs.Write(savetext);
        }
        //OutputRead();
    }
}
