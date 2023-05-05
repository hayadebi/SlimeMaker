using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class nightevent : MonoBehaviour
{
    public GameObject vlightobj;
    public GameObject plightobj;
    private GameObject dlightobj;
    private Light DLight;
    public bool eventtrg = false;
    // Start is called before the first frame update
    void Start()
    {
        if (eventtrg)
        {
            dlightobj = GameObject.Find("DirectionalLight");
            DLight = dlightobj.GetComponent<Light>();
            DLight.intensity = 0;
            RenderSettings.ambientIntensity = 0.01f;
        }
        else if (!eventtrg && (GameObject.FindGameObjectsWithTag("nightevent") == null|| GameObject.FindGameObjectsWithTag("nightevent").Length ==0))
        {
            vlightobj.SetActive(false);
            plightobj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
