using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll_add : MonoBehaviour
{
    public GameObject addcopy_target;
    public int check_maxobjnum = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(check_maxobjnum <= 0)
        {
            check_maxobjnum = GManager.instance.gimmick_length;
            for(int i=1; i < GManager.instance.gimmick_length;)
            {
                if (GManager.instance.stageobj_onset[i]==1)
                {
                    GameObject tmp = Instantiate(addcopy_target, transform.position, transform.rotation, transform);
                    clickbutton tmpui = tmp.GetComponent<clickbutton>();
                    tmpui.FixedUITime(i);
                }
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
