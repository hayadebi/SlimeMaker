using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreobj_bar : MonoBehaviour
{
    public Slider sl;
    public miniscoreobj mobj;
    // Start is called before the first frame update
    void Start()
    {
        sl.maxValue = mobj.hp_count;
        sl.value = mobj.hp_count;
    }

    // Update is called once per frame
    void Update()
    {
        if (sl.value != mobj.hp_count)
            sl.value = mobj.hp_count;
    }
}
