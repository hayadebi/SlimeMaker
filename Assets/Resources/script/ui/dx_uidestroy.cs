using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dx_uidestroy : MonoBehaviour
{
    public bool nodxdestroy = true;
    // Start is called before the first frame update
    void Start()
    {
        if (nodxdestroy && GManager.instance.dx_stageid != -1)
            Destroy(gameObject, 0.1f);
        else if(!nodxdestroy && GManager.instance.dx_stageid == -1)
            Destroy(gameObject, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
