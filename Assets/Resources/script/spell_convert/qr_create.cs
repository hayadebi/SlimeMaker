using UnityEngine;
using ZXing;
using ZXing.QrCode;
using System.IO;
using Unity.IO.Compression;
using System;
using System.Collections;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.Events;
using NCMB;
//前提知識:参考サイト5

public class qr_create : MonoBehaviour
{
    public static qr_create instance = null;
    //public string content = "";
    public string[] tags;
    [Multiline]
    public byte[] qr_content;
    [Multiline]
    public string tweets = "";
    public InputManager inputspell;
    private void Start()
    {
        //if(content != "")
        //{
        //    StartCoroutine(nameof(CreaterQR));
        //}
    }
    public void outputread()//呼び出すことでQRコードを作成し、ツイートする
    {
        string path = Application.persistentDataPath + "/stage00.txt";
        using (var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            string tmp = fs.ReadToEnd();
            print(tmp);
            qr_content = GManager.instance.ComporessGZIP(tmp);
            print(qr_content.ToString());
        }
        StartCoroutine("CreaterQR");
    }
    public void CreateServer()
    {
        // クラスのNCMBObjectを作成
        NCMBObject obj = new NCMBObject("StageDataClass");
        // オブジェクトに値を設定
        obj["stagedata"] = BitConverter.ToString(qr_content);
        // データストアへの登録
        obj.SaveAsync();
    }
    public IEnumerator CreaterQR()
    {
        // 保存するQRコードの画像ファイル名
        var path = Application.dataPath + "/QRCode.png";

        // QR コードの画像の幅と高さ
        var width = 256;
        var height = 256;

        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Width = width,
                Height = height
            }
        };

        var format = TextureFormat.ARGB32;
        var texture = new Texture2D(width, height, format, false);
        print(BitConverter.ToString(qr_content));
        var colors = writer.Write(BitConverter.ToString(qr_content));

        texture.SetPixels32(colors);
        texture.Apply();

        var bytes = texture.EncodeToPNG();
        var imageBase64 = Convert.ToBase64String(bytes);
        // Form Dataの作成
        var formData = new WWWForm();
        formData.AddField("image", imageBase64);

        //imgurのリクエスト作成
        var request = UnityWebRequest.Post("https://api.imgur.com/3/image", formData);
        request.SetRequestHeader("AUTHORIZATION", "Client-ID " + "dd103802824b5f9");

        // リクエスト実行
        yield return request.SendWebRequest();
        var response = JsonUtility.FromJson<Response>(request.downloadHandler.text);
        string tempurl = response.data.link;
        tempurl = tempurl.Remove(tempurl.Length - 4, 4);
        tweets += tempurl + "%0a";
        StartCoroutine(TweetWithScreenShot.TweetManager.TweetWithScreenShot(tweets));//参考サイト5のを呼び出してツイート
    }

    // アップロードAPIのレスポンスデータ(必要分のみ定義)
    [Serializable]
    private struct Response
    {
        [Serializable]
        public struct Data
        {
            // アップロードされた画像URL
            public string link;
        }

        public Data data;
        public bool success;
        public int status;
    }
}