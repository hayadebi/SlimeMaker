using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutter : MonoBehaviour
{
    public cutter colmode = null;
    public float move_num = 1;
    public float move_y = 0;
    public float move_x = 1;
    public float move_speed = 1.5f;
    public Rigidbody rb;
    public float auto_switchtime = 10f;
    public float count_time = 0;
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.walktrg && !GManager.instance.over && colmode == null && rb != null)
        {
            count_time += Time.deltaTime;
            if(count_time >= auto_switchtime)
            {
                count_time = 0;
                move_num *= -1;
            }

            var tempVc = new Vector3(move_num * move_x, 0, move_num * move_y);
            if (tempVc.magnitude >= 1) tempVc = tempVc.normalized;
            var vec = tempVc;
            var movevec = vec * move_speed;
            rb.velocity = movevec;
        }
        else if (rb != null && rb.velocity != Vector3.zero)
            rb.velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (colmode != null && (col.tag == "wall"|| col.tag == "icewall"))
        {
            colmode.move_num *= -1;
            colmode.count_time = 0;
           if(rb != null) rb.velocity = Vector3.zero;
        }
        else if (col.tag != "ground" && rb != null && rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
