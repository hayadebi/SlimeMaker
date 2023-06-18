using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jplocal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Application.systemLanguage == SystemLanguage.Japanese && !GManager.instance.slime_titleui)
        {
            GManager.instance.isEnglish = 0;
        }
        else if(!GManager.instance.slime_titleui)
        {
            GManager.instance.isEnglish = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
