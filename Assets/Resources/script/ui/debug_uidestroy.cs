using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug_uidestroy : MonoBehaviour
{
    public bool destroy_debug = false;
    // Start is called before the first frame update
    void Start()
    {
        if(destroy_debug && GManager.instance.debug_trg )
            Destroy(gameObject, 0.1f);
        else if (!destroy_debug && !GManager.instance.debug_trg)
            Destroy(gameObject, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
