using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class night_bgm : MonoBehaviour
{
    public AudioClip set_bgm;
    private GameObject bgmobj=null;
    private AudioSource audioSource=null;
    // Start is called before the first frame update
    void Start()
    {
        bgmobj = GameObject.Find("BGM");
        if(bgmobj!=null)
            audioSource = bgmobj.GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = set_bgm;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
