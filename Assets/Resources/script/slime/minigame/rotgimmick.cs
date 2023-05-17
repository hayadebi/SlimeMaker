using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotgimmick : MonoBehaviour
{
    public Transform rb;
    public float speed=4;
    private float torque=4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GManager.instance.over && GManager.instance.walktrg)
        {
            torque = speed;
            rb.Rotate(new Vector3(0, torque, 0));
        }
        else if (torque != 0f)
            torque = 0;
    }
}
