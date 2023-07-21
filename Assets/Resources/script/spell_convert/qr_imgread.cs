using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;
public class qr_imgread : MonoBehaviour
{
    string _result = null;
    public WebCamTexture _webCam;//カメラ
    public RawImage raw;//カメラに映った様子を表示するためのRawImage
    private bool check_trg = false;
    private string global_temp1;
    private string global_temp2;
    public Image btimg;
    public Text bttxt;
    private bool is_start = false;
    public Text qrtxt;
    private bool not_lost = false;
    public FileManager fmanager;
    private bool not_stageqrtrg = false;
    public InputManager inputspell;
    public bool servertrg = false;
    public void ClickQR()
    {
        StartCoroutine(nameof(QRStart));
    }
    public IEnumerator QRStart()//カメラ起動
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
       
        btimg.enabled = false;
        bttxt.enabled = false;
        GManager.instance.setmenu = 1;
        if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
        {
            Debug.LogFormat("no camera.");
            yield break;
        }
        Debug.LogFormat("camera ok.");
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices == null || devices.Length == 0)
            yield break;
        _webCam = new WebCamTexture(devices[0].name, Screen.width, Screen.height, 12);
        raw.texture = _webCam;
        _webCam.Play();//このカメラ起動と似たような感じでStop()で止めれる
        is_start = true;
    }
    void Update()
    {
        if (is_start && !check_trg && _webCam != null && !servertrg)
        {
            _result = QRCodeHelper.Read(_webCam);//QRかどうか判断するための
            if (_result != null && QRCodeHelper.Read(_webCam) != null && _result != "error" && !check_trg)//QRだった場合に条件を通させる、エラーの時は通らない
            {
                string tmp = "";
                int stage_qrtrg = 0;
                try
                {
                    print(_result);
                    string[] arrayStr2 = _result.Split('-');
                    byte[] arrayOut = new byte[arrayStr2.Length];
                    for (int i = 0; i < arrayStr2.Length; i++)
                    {
                        // 16進数文字列に変換
                        arrayOut[i] = Convert.ToByte(arrayStr2[i], 16);
                    }
                    print(GManager.instance.DeComporessGZIP(arrayOut));
                    tmp = GManager.instance.DeComporessGZIP(arrayOut);

                    StringReader rs = new StringReader(tmp);
                    string line = null;
                    int check_line = 0;

                    while ((line = rs.ReadLine()) != null)//本作では1行読み込んでステージデータかどうか判別してる
                    {
                        if (check_line == 0 && line == "stage")
                            stage_qrtrg += 1;
                        else if (check_line == 1 && line == Application.version)
                            stage_qrtrg += 1;
                        else if (((check_line == 0 && line != "stage") || (check_line == 1 && (line != Application.version&& line!="-1"))) && !not_stageqrtrg)
                        {
                            not_stageqrtrg = true;
                            GManager.instance.setrg = 1;
                            qrtxt.fontSize = 28;
                            if (GManager.instance.isEnglish == 0)
                                qrtxt.text = "<color=red>同じゲームバージョンのステージ専用</color>QRコードを読み込んでね！";
                            else
                                qrtxt.text = "<color=red>Please read the QR code dedicated to the same game version stage.</color>!";
                        }
                        check_line += 1;
                    }
                    if (stage_qrtrg >= 2)//ステージデータなら
                    {
                        check_trg = true;
                        // 書き込み
                        string path = Application.persistentDataPath + "/stage00.txt";
                        bool isAppend = false; // 上書き or 追記
                        using (var fs = new StreamWriter(path, isAppend, System.Text.Encoding.GetEncoding("UTF-8")))
                        {
                            fs.Write(tmp);
                        }
                        GManager.instance.setrg = 6;
                        Instantiate(GManager.instance.all_ui[0], transform.position, transform.rotation);
                        _webCam.Stop();
                        _webCam = null;
                        Invoke("SceneChange", 1);//書き込み後ステージシーンへ飛ぶ
                    }
                    stage_qrtrg = 0;
                    tmp = "";
                }
                catch (System.Exception)
                {
                    if (tmp == "")
                    {
                        not_stageqrtrg = true;
                        GManager.instance.setrg = 1;
                        qrtxt.fontSize = 28;
                        if (GManager.instance.isEnglish == 0)
                            qrtxt.text = "<color=red>同じゲームバージョンのステージ専用</color>QRコードを読み込んでね！";
                        else
                            qrtxt.text = "<color=red>Please read the QR code dedicated to the same game version stage.</color>!";
                        stage_qrtrg = 0;
                    }
                    else
                    {
                        StringReader rs = new StringReader(tmp);
                        string line = null;
                        int check_line = 0;

                        while ((line = rs.ReadLine()) != null)//本作では1行読み込んでステージデータかどうか判別してる
                        {
                            if (check_line == 0 && line == "stage")
                                stage_qrtrg += 1;
                            else if (check_line == 1 && (line == Application.version || line == "-1"))
                                stage_qrtrg +=1;
                            else if (((check_line == 0 && line != "stage") || (check_line == 1 && (line != Application.version && line != "-1"))) && !not_stageqrtrg)
                            {
                                not_stageqrtrg = true;
                                GManager.instance.setrg = 1;
                                qrtxt.fontSize = 28;
                                if (GManager.instance.isEnglish == 0)
                                    qrtxt.text = "<color=red>同じゲームバージョンのステージ専用</color>QRコードを読み込んでね！";
                                else
                                    qrtxt.text = "<color=red>Please read the QR code dedicated to the same game version stage.</color>!";
                            }
                            check_line += 1;
                        }
                        if (stage_qrtrg >= 2)//ステージデータなら
                        {
                            check_trg = true;
                            // 書き込み
                            string path = Application.persistentDataPath + "/stage00.txt";
                            bool isAppend = false; // 上書き or 追記
                            using (var fs = new StreamWriter(path, isAppend, System.Text.Encoding.GetEncoding("UTF-8")))
                            {
                                fs.Write(tmp);
                            }
                            GManager.instance.setrg = 6;
                            Instantiate(GManager.instance.all_ui[0], transform.position, transform.rotation);
                            _webCam.Stop();
                            _webCam = null;
                            Invoke("SceneChange", 1);//書き込み後ステージシーンへ飛ぶ
                        }
                        stage_qrtrg = 0;
                        tmp = "";
                    }
                }
            }
        }
    }
    public void ServerStageRead(string _result="")
    {
        if (servertrg)
        {
            string tmp = "";
            int stage_qrtrg = 0;
            try
            {
               // print(_result);
                string[] arrayStr2 = _result.Split('-');
                byte[] arrayOut = new byte[arrayStr2.Length];
                for (int i = 0; i < arrayStr2.Length; i++)
                {
                    // 16進数文字列に変換
                    arrayOut[i] = Convert.ToByte(arrayStr2[i], 16);
                }
                //print(GManager.instance.DeComporessGZIP(arrayOut));
                tmp = GManager.instance.DeComporessGZIP(arrayOut);

                StringReader rs = new StringReader(tmp);
                string line = null;
                int check_line = 0;

                while ((line = rs.ReadLine()) != null)//本作では1行読み込んでステージデータかどうか判別してる
                {
                    if (check_line == 0 && line == "stage")
                        stage_qrtrg += 1;
                    else if (check_line == 1 && (line == Application.version || line == "-1"))
                        stage_qrtrg += 1;
                    else if (((check_line == 0 && line != "stage") || (check_line == 1 && (line != Application.version && line != "-1"))) && !not_stageqrtrg)
                    {
                        not_stageqrtrg = true;
                        GManager.instance.setrg = 1;
                        qrtxt.fontSize = 28;
                        if (GManager.instance.isEnglish == 0)
                            qrtxt.text = "<color=red>同じゲームバージョンのステージを選んでね！</color>";
                        else
                            qrtxt.text = "<color=red>Choose the same game version of the stage!</color>!";
                    }
                    check_line += 1;
                }
                if (stage_qrtrg >= 2)//ステージデータなら
                {
                    check_trg = true;
                    // 書き込み
                    string path = Application.persistentDataPath + "/stage00.txt";
                    bool isAppend = false; // 上書き or 追記
                    using (var fs = new StreamWriter(path, isAppend, System.Text.Encoding.GetEncoding("UTF-8")))
                    {
                        fs.Write(tmp);
                    }
                    GManager.instance.setrg = 6;
                    Instantiate(GManager.instance.all_ui[0], transform.position, transform.rotation);
                    Invoke("SceneChange", 1);//書き込み後ステージシーンへ飛ぶ
                }
                stage_qrtrg = 0;
                tmp = "";
            }
            catch (System.Exception)
            {
                if (tmp == "")
                {
                    not_stageqrtrg = true;
                    GManager.instance.setrg = 1;
                    qrtxt.fontSize = 28;
                    if (GManager.instance.isEnglish == 0)
                        qrtxt.text = "<color=red>同じゲームバージョンのステージを選んでね！</color>";
                    else
                        qrtxt.text = "<color=red>Choose the same game version of the stage!</color>!";
                    stage_qrtrg = 0;
                }
                else
                {
                    StringReader rs = new StringReader(tmp);
                    string line = null;
                    int check_line = 0;

                    while ((line = rs.ReadLine()) != null)//本作では1行読み込んでステージデータかどうか判別してる
                    {
                        if (check_line == 0 && line == "stage")
                            stage_qrtrg += 1;
                        else if (check_line == 1 && (line == Application.version || line == "-1"))
                            stage_qrtrg += 1;
                        else if (((check_line == 0 && line != "stage") || (check_line == 1 && (line != Application.version && line != "-1"))) && !not_stageqrtrg)
                        {
                            not_stageqrtrg = true;
                            GManager.instance.setrg = 1;
                            qrtxt.fontSize = 28;
                            if (GManager.instance.isEnglish == 0)
                                qrtxt.text = "<color=red>同じゲームバージョンのステージを選んでね！</color>";
                            else
                                qrtxt.text = "<color=red>Choose the same game version of the stage!</color>!";
                        }
                        check_line += 1;
                    }
                    if (stage_qrtrg >= 2)//ステージデータなら
                    {
                        check_trg = true;
                        // 書き込み
                        string path = Application.persistentDataPath + "/stage00.txt";
                        bool isAppend = false; // 上書き or 追記
                        using (var fs = new StreamWriter(path, isAppend, System.Text.Encoding.GetEncoding("UTF-8")))
                        {
                            fs.Write(tmp);
                        }
                        GManager.instance.setrg = 6;
                        Instantiate(GManager.instance.all_ui[0], transform.position, transform.rotation);
                        _webCam.Stop();
                        _webCam = null;
                        Invoke("SceneChange", 1);//書き込み後ステージシーンへ飛ぶ
                    }
                    stage_qrtrg = 0;
                    tmp = "";
                }
            }
        }
    }
    void SceneChange()
    {
        GManager.instance.setmenu = 0;
        GManager.instance.walktrg = true;
        GManager.instance.storymode = false;
        GManager.instance.debug_trg = false;
        GManager.instance.over = false;
        GManager.instance.goal_num = 0;
        SceneManager.LoadScene("loadstage");
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "qrstage" && !servertrg )
        {
            StartCoroutine(nameof(QRStart));
        }
    }
}