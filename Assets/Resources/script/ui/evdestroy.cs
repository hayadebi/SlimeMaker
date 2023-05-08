using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evdestroy : MonoBehaviour
{
    public bool nulldestroytrg = true;
    // Start is called before the first frame update
    void Start()
    {
        if(nulldestroytrg&& (GManager.instance.globalev_id == -1 || GManager.instance.globalev_stageselect == -1)) Destroy(gameObject);
        else if (GManager.instance.globalev_id != -1 && GManager.instance.globalev_stageselect != -1&&!nulldestroytrg ) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
