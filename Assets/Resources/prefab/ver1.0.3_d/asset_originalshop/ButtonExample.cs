using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.IO.Compression;
using System;
using UnityEngine.SceneManagement;

public class ButtonExample : MonoBehaviour
{
    [Multiline]
    public string result_stagedata;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StageDecoding()
    {
        string[] arrayStr2 = result_stagedata.Split('-');
        byte[] arrayOut = new byte[arrayStr2.Length];
        for (int i = 0; i < arrayStr2.Length; i++)
        {
            // 16進数文字列に変換
            arrayOut[i] = Convert.ToByte(arrayStr2[i], 16);
        }
        //print(GManager.instance.DeComporessGZIP(arrayOut));
        var tmp = DeComporessGZIP(arrayOut);
        print(tmp);
        result_stagedata = tmp;
    }
    public void StageEncoding()
    {
        var tmp = ComporessGZIP(result_stagedata);
        print(BitConverter.ToString(tmp));
        result_stagedata = BitConverter.ToString(tmp);
    }

    public byte[] ComporessGZIP(string _str)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var inflateStream = new GZipStream(memoryStream, CompressionMode.Compress))
            {
                using (var writer = new StreamWriter(inflateStream))
                {
                    writer.Write(_str);
                }
            }
            return memoryStream.ToArray();
        }
    }
    /// <summary>
    /// GZIP圧縮されたバッファを解凍します。
    /// </summary>
    public string DeComporessGZIP(byte[] _bytes)
    {
        using (var memoryStream = new MemoryStream(_bytes))
        {
            using (var deflateStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                using (var reader = new StreamReader(deflateStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
