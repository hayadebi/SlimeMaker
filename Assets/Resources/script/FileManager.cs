using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    [Multiline]
    public string savetext = "";
    public qr_create qrcreate;
    public bool startqr=false;
    public void Start()
    {
        if(startqr)
            OutputRead();
    }
    public void OutputRead()
    {
        string path = Application.persistentDataPath + "/stage00.txt";
        using (var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            string tmp = fs.ReadToEnd();
            print(tmp);
            string tmp2 = tmp;
            print(tmp2);
            qrcreate.qr_content = tmp2;
        }
        qrcreate.StartCoroutine("CreaterQR");
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
