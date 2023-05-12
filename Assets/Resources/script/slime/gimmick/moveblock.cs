using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveblock : MonoBehaviour
{
    public GameObject effectobj;
    private bool destroytrg = false;
    private int destroy_count = 0;
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "bullet" && !destroytrg)
        {
            GManager.instance.setrg = 10;
            Instantiate(effectobj, col.transform.position, transform.rotation);
            destroy_count +=1;
            Destroy(col.gameObject);
            if(destroy_count >= 2)
            {
                destroytrg = true;
                GManager.instance.setrg = 11;
                Instantiate(effectobj, transform.position, transform.rotation);
                Destroy(this.gameObject, 0.1f);
            }
        }
    }
}
