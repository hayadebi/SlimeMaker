using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bp_slider : MonoBehaviour
{
    public Slider sl;
    public PhysicMaterial pm;
    public Transform parentscale;
    private float tmpx;
    private float tmpy;
    // Start is called before the first frame update
    void Start()
    {
        sl.value = pm.bounciness;
        tmpx = parentscale.localScale.x;
        tmpy = parentscale.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (sl.value != pm.bounciness)
        {
            if(sl.value >= pm.bounciness)
                iTween.ShakePosition(this.gameObject, iTween.Hash("x", 5f, "y", 5f, "time", 0.24f));
            else
            {
                iTween.ScaleTo(parentscale.gameObject, iTween.Hash("x", tmpx * 1.5f, "y", tmpy * 1.5f, "time", 0.15f));
                iTween.ScaleTo(parentscale.gameObject, iTween.Hash("x", tmpx, "y", tmpy, "time", 0.2f, "delay", 0.151f));
            }
            sl.value = pm.bounciness;
        }
    }
}
