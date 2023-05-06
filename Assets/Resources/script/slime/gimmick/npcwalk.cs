using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMeshAgentを使うために必要

public class npcwalk : MonoBehaviour
{
    public GameObject[] slimes=new GameObject[2];
    public GameObject target;
    private float agentSP = 1.5f;
    private NavMeshAgent agent; //自動で動くオブジェクト
    private float walktime = 0;
    private Rigidbody rb;
    private bool eyetrg = false;
    private bool starttrg = false;
    void Start()
    {
        // アタッチされているオブジェクトのNaveMeshAgentを取得
        agent = GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        target = null;
        agent.destination = this.transform.position;
        agent.speed = 0f;
        Invoke(nameof(StartAgentSet), 0.2f);
    }
    void StartAgentSet()
    {
        slimes[0] = GameObject.Find("blue_slime");
        slimes[1] = GameObject.Find("red_slime");
        //最初のターゲット設定
        agent.destination = SetTarget().transform.position;
        agent.speed = 1.5f;
        starttrg = true;
    }
    private void FixedUpdate()
    {
        if (!GManager.instance.over && starttrg && !eyetrg && slimes[0]!=null && slimes[1]!=null)
        {
            walktime += Time.deltaTime;
            if (walktime >= 2f)
            {
                walktime = 0;
                agent.destination = SetTarget().transform.position;
            }
            else if (agent.speed != agentSP)
            {
                agent.destination = SetTarget().transform.position;
                agent.speed = agentSP;
            }
        }
        else if(slimes[0] == null || slimes[1] == null)
        {
            slimes[0] = GameObject.Find("blue_slime");
            slimes[1] = GameObject.Find("red_slime");
        }
        else if ((GManager.instance.over || !GManager.instance.walktrg || target == null || slimes[0] == null || slimes[1] == null) && agent.speed != 0 && eyetrg)
        {
            agent.speed = 0;
            rb.velocity = Vector3.zero;
        }
    }
    public GameObject SetTarget()
    {
        GameObject tmpobj = this.gameObject;
        if (slimes[0] != null && slimes[1] != null)
        {
            float blue = Mathf.Abs((slimes[0].transform.position - this.transform.position).magnitude);
            float red = Mathf.Abs((slimes[1].transform.position - this.transform.position).magnitude);
            if (blue < red)
                tmpobj = slimes[0];
            else if (blue >= red)
                tmpobj = slimes[1];
        }
        else if (slimes[0] == null)
        {
            tmpobj = slimes[1];
        }
        else if (slimes[1] == null)
        {
            tmpobj = slimes[0];
        }
        return tmpobj;
    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "eye")
            eyetrg = true;
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "eye")
            eyetrg = false;
    }
}