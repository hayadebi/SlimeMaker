using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dxdestroy : MonoBehaviour
{
    public bool target_trg = true;
    public GameObject target_obj;
    // Start is called before the first frame update
    void Start()
    {
        if (target_trg == GManager.instance.dx_mode) target_obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
