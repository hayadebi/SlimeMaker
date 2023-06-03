using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Flowchart))]
public class npcsay : MonoBehaviour
{
    public localLnpc llnpc = null;
    public int stage_id =0;
    public float returnTime = 3;
    public bool sayreturn;
    public string PlayerTag = "player";
    bool saytrg = false;
    public string message = "stage";
    bool isTalking = false;
    Flowchart flowChart;
    public int _inputLocal = 0;
    public bool starttrg = true;
    public GameObject uiobj=null;
    public Animator abool_c=null;
    public int ev_checkid = 9999;
    public int ev_checkstageselect = 9999;

    // Start is called before the first frame update
    void Start()
    {
       
        flowChart = this.GetComponent<Flowchart>();
        llnpc = this.GetComponent<localLnpc>();

        if (!saytrg && GManager.instance.storymode && GManager.instance.select_stage == stage_id && GManager.instance.walktrg && !GManager.instance.notsay && starttrg&&GManager.instance.dx_stageid==-1)
        {
            saytrg = true;
            message += stage_id.ToString();
            StartCoroutine(Talk());
        }
        else if (!saytrg && GManager.instance.globalev_stageselect == ev_checkstageselect && GManager.instance.globalev_id == ev_checkid && GManager.instance.walktrg && !GManager.instance.notsay && starttrg )
        {
            saytrg = true;
            message = "ev"+ev_checkid+"_"+ev_checkstageselect.ToString();
            StartCoroutine(Talk());
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (!saytrg && col.tag=="player" && GManager.instance.storymode && GManager.instance.select_stage == stage_id && GManager.instance.walktrg && !GManager.instance.notsay && !starttrg&&GManager.instance.dx_stageid==-1)
        {
            saytrg = true;
            message += stage_id.ToString();
            StartCoroutine(Talk());
        }
    }

    public IEnumerator Talk()
    {
        GManager.instance.notsay = true;
        GManager.instance.walktrg = false;
        if (isTalking)
        {
            yield break;
        }
        isTalking = true;
        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        isTalking = false;
        GManager.instance.walktrg = true;
        if (sayreturn)
        {
            Invoke("SayTrg", returnTime);
        }
        if (uiobj != null)
        {
            GManager.instance.walktrg = true;
            Instantiate(uiobj, transform.position, transform.rotation);
        }
        if (abool_c != null)
            abool_c.SetBool("Abool", true);
    }
    void SayTrg()
    {
        saytrg = false;
    }
}
