using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class localLnpc : MonoBehaviour
{
    public bool bgmplay = false;
    public bool endTrg = false;
    public string local = "local";
    public string badend = "badend";
    public bool setP = false;
    public npcsay input_say = null;
    Flowchart flowChart;
    private npcsay ns = null;
    public GameObject BGM = null;
    public int bgm_index = 1;
    public float _time = 0;
    public int fademode = 0;
    public AudioSource bgmA;
    public bool move_trg = false;
    private int ontrg = 0;
    private Vector3 vec;
    private float vtime = 0;
    // Start is called before the first frame update
    void Start()
    {
        flowChart = this.GetComponent<Flowchart>();
        if (GManager.instance.isEnglish == 1)
        {
            flowChart.SetBooleanVariable(local, true);
        }
        if (bgmplay && this.GetComponent<npcsay>() )
        {
            ns = this.GetComponent<npcsay>();
            
        }
        BGM = GameObject.Find("BGM");
        bgmA = BGM.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(flowChart.GetBooleanVariable(local) == true && GManager.instance.isEnglish == 0)
        {
            flowChart.SetBooleanVariable(local, false);
        }
        else if (flowChart.GetBooleanVariable(local) == false && GManager.instance.isEnglish == 1)
        {
            flowChart.SetBooleanVariable(local, true);
        }
        if (input_say && flowChart.GetIntegerVariable("input") != 0)
        {
            input_say._inputLocal = flowChart.GetIntegerVariable("input");
            flowChart.SetIntegerVariable("input", 0);
        }

    }
    
}
