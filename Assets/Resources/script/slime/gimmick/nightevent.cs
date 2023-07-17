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
    private bool oldtrg = false;
    private float default_light = 1f;
    // Start is called before the first frame update
    void Start()
    {
        dlightobj = GameObject.Find("DirectionalLight");
        DLight = dlightobj.GetComponent<Light>();
        GManager.instance.nighttrg = true;
        oldtrg = GManager.instance.nighttrg;
        if (eventtrg)
        {
            DLight.intensity = 0;
            RenderSettings.ambientIntensity = 0.01f;
        }
        else if (!eventtrg && (GameObject.FindGameObjectsWithTag("nightevent") == null|| GameObject.FindGameObjectsWithTag("nightevent").Length ==0))
        {
            if (vlightobj != null) vlightobj.SetActive(false);
            if (plightobj != null) plightobj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (eventtrg && !oldtrg && GManager.instance.nighttrg)
        {
            oldtrg = GManager.instance.nighttrg;
            DLight.intensity = 0;
            if (RenderSettings.ambientIntensity > 0.01f) RenderSettings.ambientIntensity = 0.01f;
        }
        else if (eventtrg && oldtrg && !GManager.instance.nighttrg)
        {
            oldtrg = GManager.instance.nighttrg;
            DLight.intensity = default_light;
            if(RenderSettings.ambientIntensity<1f) RenderSettings.ambientIntensity = 1f;
        }
        if (!eventtrg && !oldtrg && GManager.instance.nighttrg)
        {
            oldtrg = GManager.instance.nighttrg;
            if (vlightobj != null) vlightobj.SetActive(true);
            if (plightobj != null) plightobj.SetActive(true);
        }
        else if (!eventtrg && oldtrg && !GManager.instance.nighttrg)
        {
            oldtrg = GManager.instance.nighttrg;
            if (vlightobj != null) vlightobj.SetActive(false);
            if (plightobj != null) plightobj.SetActive(false);
        }
    }
}
