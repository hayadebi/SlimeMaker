using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomBool : MonoBehaviour
{
    public string[] name_true ;
    public string[] name_false ;
    public Text _text;
    // Start is called before the first frame update
    void Start()
    {
        SetBoolText();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetBoolText()
    {
        if (GManager.instance.sorttrg) _text.text = name_true[0];
        else if (!GManager.instance.sorttrg) _text.text = name_false[0];
    }
    public void SetClickBool()
    {
        GManager.instance.setrg = 8;
        GManager.instance.sorttrg = !GManager.instance.sorttrg;
        SetBoolText();
    }
}
