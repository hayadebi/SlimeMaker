using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class npcwalk : MonoBehaviour
{
    public GameObject[] slimes=new GameObject[2];
    private float agentSP = 2.1f;
    private NavMeshAgent agent; //自動で動くオブジェクト
    private float walktime = 0;
    private Rigidbody rb;
    private bool eyetrg = false;
    private bool starttrg = false;
    public Animator anim;
    private AudioSource _audio;
    public AudioClip se;
    public GameObject effect;
    private NavMeshSurface nms=null;
    private float nmstime = 0f;
    
    void Start()
    {
        // アタッチされているオブジェクトのNaveMeshAgentを取得
        agent = GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        agent.destination = this.transform.position;
        agent.speed = 0f;
        _audio = GetComponent<AudioSource>();
        Invoke(nameof(StartAgentSet), 0.2f);
    }
    void StartAgentSet()
    {
        if (nms == null)
            nms = GameObject.Find("navsurface").GetComponent<NavMeshSurface>();
        slimes[0] = GameObject.Find("blue_slime");
        slimes[1] = GameObject.Find("red_slime");
        //最初のターゲット設定
        agent.destination = SetTarget().transform.position;
        agent.speed = 2.1f;
        starttrg = true;
    }
    private void FixedUpdate()
    {
        if (!GManager.instance.over && GManager.instance.walktrg&& starttrg && !eyetrg && (slimes[0]!=null || slimes[1]!=null))
        {
            walktime += Time.deltaTime;
            nmstime += Time.deltaTime;
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
            if (nmstime >= 15f)
            {
                nmstime = 0f;
                nms.BuildNavMesh();
            }
        }
        else if ((GManager.instance.over || !GManager.instance.walktrg || (slimes[0] == null && slimes[1] == null)|| eyetrg) && agent.speed != 0)
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
        anim.SetInteger("Anumber", 0);
        return tmpobj;
    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "eye")
        {
            anim.SetInteger("Anumber", 1);
            _audio.PlayOneShot(se);
            Instantiate(effect, transform.position, transform.rotation);
            eyetrg = true;
        }
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "eye")
        {
            anim.SetInteger("Anumber", 0);
            eyetrg = false;
        }
    }
}