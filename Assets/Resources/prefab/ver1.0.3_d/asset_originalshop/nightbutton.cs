using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightbutton : MonoBehaviour
{
    public Animator buttonanim;
    private bool uptrg = true;
    public AudioSource audioSource;
    public AudioClip se;
    public bool staytrg = false;
    private void Start()
    {
        ;
    }
    void ButtonAnimSet(int setn = 1)
    {
        buttonanim.SetInteger("Anumber", setn);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "player" || col.tag == "wall")
        {
            staytrg = true;
            
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(se);
            if (uptrg)
            {
                GManager.instance.nighttrg = !GManager.instance.nighttrg;
                uptrg = false;
                ButtonAnimSet(1);
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "player" || col.tag == "wall")
        {
            staytrg = false;
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(se);
            ButtonAnimSet(0);
            uptrg = true;
        }
    }
}
