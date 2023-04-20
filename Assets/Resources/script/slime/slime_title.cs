using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_title : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if (!GManager.instance.slime_titleanim)
        {
            GManager.instance.slime_titleanim = true;
            anim.SetInteger("Anumber", 1);
        }
        else
            anim.SetInteger("Anumber", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
