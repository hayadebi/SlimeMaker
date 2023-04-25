using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject effectobj;
    public float destroy_time = 12;
    private bool destroytrg = false;
    private bool mirror_trg = false;
    public bool icetrg = false;
    public GameObject colid_obj0;
    public GameObject colid_obj1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroy_time);
    }

    private void OnTriggerEnter(Collider col)
    {
        if((col.tag == "wall" && !destroytrg)|| (col.tag == "icewall" && !destroytrg && mirror_trg && !icetrg)|| (col.tag == "icewall" && !destroytrg && icetrg))
        {
            GManager.instance.setrg = 10;
            destroytrg = true;
            if(icetrg && col.GetComponent<obj_id>() && col.GetComponent<obj_id>().colcheck_id == 0)
            {
                Instantiate(colid_obj0, col.transform.position, col.transform.rotation);
                Destroy(col.gameObject);
            }
            else if (icetrg && col.GetComponent<obj_id>() && col.GetComponent<obj_id>().colcheck_id == 1)
            {
                Instantiate(colid_obj1, col.transform.position, col.transform.rotation);
                Destroy(col.gameObject);
            }
            Instantiate(effectobj, transform.position, transform.rotation);
            Destroy(this.gameObject, 0.1f);
        }
        else if (col.tag == "icewall" && !destroytrg && !mirror_trg &&!icetrg )
        {
            mirror_trg = true;
            GManager.instance.setrg = 13;
            this.gameObject.GetComponent<Rigidbody>().velocity = this.transform.forward * 16;
        }
    }
}
