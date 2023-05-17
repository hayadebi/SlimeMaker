using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class widthmove : MonoBehaviour
{
    private float start_x;
    public float max_widthmove = 3f;
    public float movetime = 2f;
    private float tmp_time = 0;
    public bool movetrg = true;
    // Start is called before the first frame update
    void Start()
    {
        start_x = transform.position.x;
        var tmp = transform.position;
        tmp.x -= max_widthmove;
        transform.position = tmp;
        tmp_time = 0;
        MoveTween();
    }

    // Update is called once per frame
    void Update()
    {
        if(GManager.instance.walktrg && !GManager.instance.over && movetrg)
        {
            tmp_time += Time.deltaTime;
            if (tmp_time >= movetime*2)
            {
                tmp_time = 0f;
                MoveTween();
            }
        }
    }
    void MoveTween()
    {
        if (GManager.instance.walktrg && !GManager.instance.over && movetrg)
        {
            iTween.MoveTo(gameObject, iTween.Hash("x", start_x, "time", movetime / 2));
            iTween.MoveTo(gameObject, iTween.Hash("x", start_x +max_widthmove, "time", movetime/2, "delay", movetime / 2));
            iTween.MoveTo(gameObject, iTween.Hash("x", start_x, "time", movetime / 2, "delay", movetime));
            iTween.MoveTo(gameObject, iTween.Hash("x", start_x - max_widthmove, "time", movetime/2, "delay", movetime+movetime/2));
        }
    }
}
