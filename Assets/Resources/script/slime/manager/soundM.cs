﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class soundM : MonoBehaviour
{
    public AudioClip[] se;
    AudioSource audioS;
    
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        GManager.instance.setmenu = 0;
        GManager.instance.over = false;
        GManager.instance.walktrg = true;
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown (KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        else if( GManager.instance.setrg != -1 && GManager.instance.setrg != 99)
        {
            if(SceneManager.GetActiveScene().name== "minigame") audioS.Stop();
            audioS.PlayOneShot(se[GManager.instance.setrg]);
            GManager.instance.setrg = -1;
        }
    }

}
