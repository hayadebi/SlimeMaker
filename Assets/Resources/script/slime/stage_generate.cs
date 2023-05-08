using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class stage_generate : MonoBehaviour
{
    [System.Serializable]
    public struct stage_board
    {
        public GameObject[] stage_x;
    }
    public stage_board[] stage_y;

    private int select_y = -3;
    private int select_x = 0;
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        if (GManager.instance.select_stage == 6)
        {
            BGM.Stop();
            BGM.clip = null;
        }
        //その壁上下
        for (int i = 0; i < 26;)
        {
            Vector3 vec = transform.position;
            vec.x = (26 / 2 * -1) + i;
            vec.z = -10;
            vec.y = 0;
            Instantiate(GManager.instance.stageobj_id[1], vec, transform.rotation, transform);
            vec.z = 9;
            Instantiate(GManager.instance.stageobj_id[1], vec, transform.rotation, transform);
            i++;
        }
        for (int i = 0; i < 20;)
        {
            Vector3 vec = transform.position;
            vec.x = -14;
            vec.z = (20 / 2 * -1) + i;
            vec.y = 0;
            Instantiate(GManager.instance.stageobj_id[1], vec, transform.rotation, transform);
            vec.x = 13;
            Instantiate(GManager.instance.stageobj_id[1], vec, transform.rotation, transform);
            vec.x = 14;
            Instantiate(GManager.instance.stageobj_id[1], vec, transform.rotation, transform);
            vec.x = -15;
            Instantiate(GManager.instance.stageobj_id[1], vec, transform.rotation, transform);
            vec.x = 15;
            Instantiate(GManager.instance.stageobj_id[1], vec, transform.rotation, transform);
            vec.x = -16;
            Instantiate(GManager.instance.stageobj_id[1], vec, transform.rotation, transform);
            vec.x = 16;
            Instantiate(GManager.instance.stageobj_id[1], vec, transform.rotation, transform);
            vec.x = -17;
            Instantiate(GManager.instance.stageobj_id[1], vec, transform.rotation, transform);
            i++;
        }
        if (GManager.instance.dx_stageid != -1)
        {
            StringReader fs = new StringReader(GManager.instance.dx_filename[GManager.instance.dx_stageid].text);//csv
            string line = null;
            while ((line = fs.ReadLine()) != null)
            {
                if (select_y > -1)
                {
                    string[] arr = line.Split(',');
                    //Array.Reverse(arr);
                    for (int i = 0; i < arr.Length;)
                    {
                        if (arr[i] != ",")
                        {
                            Vector3 vec = transform.position;
                            vec.x = ((stage_y[0].stage_x.Length / 2) * -1) + select_x;
                            vec.z = (stage_y.Length / 2 - 1f) - select_y;
                            vec.y = 0;
                            stage_y[select_y].stage_x[select_x] = Instantiate(GManager.instance.stageobj_id[int.Parse(arr[i])], vec, transform.rotation, transform);
                            select_x += 1;
                        }
                        i++;
                    }
                    select_x = 0;
                }
                select_y += 1;
            }
        }
        else if (GManager.instance.storymode)
        {
            StringReader fs = null;
            if (GManager.instance.dx_stageid == -1)
                fs = new StringReader(GManager.instance.story_stagefile[GManager.instance.select_stage].text);//csv
            else if (GManager.instance.dx_stageid != -1)
                fs = new StringReader(GManager.instance.dx_filename[GManager.instance.dx_stageid].text);//csv
            string line = null;
            while ((line = fs.ReadLine()) != null)
            {
                if (select_y > -1)
                {
                    string[] arr = line.Split(',');
                    //Array.Reverse(arr);
                    for (int i = 0; i < arr.Length;)
                    {
                        if (arr[i] != ",")
                        {
                            Vector3 vec = transform.position;
                            vec.x = ((stage_y[0].stage_x.Length / 2) * -1) + select_x;
                            vec.z = (stage_y.Length / 2-1f) - select_y;
                            vec.y = 0;
                            stage_y[select_y].stage_x[select_x]=Instantiate(GManager.instance.stageobj_id[int.Parse(arr[i])], vec, transform.rotation,transform);
                            select_x += 1;
                        }
                        i++;
                    }
                    select_x = 0;
                }
                select_y += 1;
            }
        }
        else if (!GManager.instance.storymode)
        {
            string path = Application.persistentDataPath + "/stage00.txt";
            using (var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))
            {
                string line = null;
                string allline = "";
                while ((line = fs.ReadLine()) != null)
                {
                    allline += line + "\n";
                    if (select_y > -1)
                    {
                        string[] arr = line.Split(',');
                        //Array.Reverse(arr);
                        for (int i = 0; i < arr.Length;)
                        {
                            if (arr[i] != ",")
                            {
                                Vector3 vec = transform.position;
                                vec.x = ((stage_y[0].stage_x.Length / 2) * -1) + select_x;
                                vec.z = (stage_y.Length / 2-1f) - select_y;
                                vec.y = 0;
                                GManager.instance.test_y[select_y].test_x[select_x] = int.Parse(arr[i]);
                                stage_y[select_y].stage_x[select_x] = Instantiate(GManager.instance.stageobj_id[int.Parse(arr[i])], vec, transform.rotation,transform);
                                select_x += 1;
                            }
                            i++;
                        }
                        select_x = 0;
                    }
                    select_y += 1;
                }
                print(allline);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GManager.instance.over && GManager.instance.walktrg && !GManager.instance.debug_trg && !GManager.instance.cleartrg)
        {
            GManager.instance.cleartime += Time.deltaTime;
        }
    }
}
