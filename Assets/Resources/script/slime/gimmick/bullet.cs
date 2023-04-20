using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject effectobj;
    public float destroy_time = 12;
    private bool destroytrg = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroy_time);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "wall" && !destroytrg)
        {
            GManager.instance.setrg = 10;
            destroytrg = true;
            Instantiate(effectobj, transform.position, transform.rotation);
            Destroy(this.gameObject, 0.1f);
        }
    }
}
