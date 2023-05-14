using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tanzaku : MonoBehaviour
{
    public GameObject[] goals;
    public takegoal[] takegs;
    public int set_setrg = 0;
    public GameObject effect;
    private bool entertrg = false;
    // Start is called before the first frame update
    void Start()
    {
        
        Invoke(nameof(StartSet), 0.2f);
    }
    void StartSet()
    {
        goals = GameObject.FindGameObjectsWithTag("takegoal");
        takegs = new takegoal[goals.Length];
        for (int i = 0; i < goals.Length;)
        {
            takegs[i] = goals[i].GetComponent<takegoal>();
            i++;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "player" && !entertrg )
        {
            entertrg = true;
            for (int i = 0; i < takegs.Length;)
            {
                takegs[i].SetGoal(-1);
                i++;
            }
            GManager.instance.setrg = set_setrg;
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
