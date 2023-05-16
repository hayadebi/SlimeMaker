using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinsmash : MonoBehaviour
{
    public Animator anim=null;
    public Rigidbody rb=null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(anim != null)
        {
            if (!anim.GetBool("pintrg") && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
                anim.SetBool("pintrg", true);
            else if (anim.GetBool("pintrg") && (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2)))
                anim.SetBool("pintrg", false);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (rb!=null && col.tag == "pinsmash")
        {
            // 向きの生成（Z成分の除去と正規化）
            Vector3 shotForward = Vector3.Scale((col.ClosestPointOnBounds(this.transform.position) - transform.position), new Vector3(1, 1, 0)).normalized;
            rb.velocity = shotForward * -5f;
        }
    }
}
