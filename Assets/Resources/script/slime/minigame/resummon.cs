using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resummon : MonoBehaviour
{
    public GameObject summonobj;
    public GameObject tmpobj=null;
    private float tmp_time = 0;
    public float retime = 10f;
    public widthmove wm = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tmpobj == null)
        {
            tmp_time += Time.deltaTime;
            if(tmp_time >= retime)
            {
                tmp_time = 0f;
                tmpobj = Instantiate(summonobj, transform.position, transform.rotation,summonobj.transform.parent);
                tmpobj.transform.position = summonobj.transform.position;
                tmpobj.transform.localScale = summonobj.transform.localScale;
                tmpobj.SetActive(true);
            }
        }
    }
}
