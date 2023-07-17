using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallbutton : MonoBehaviour
{
    private GameObject[] wallrocks = null;
    private Animator[] wallanims = null;
    public Animator buttonanim;
    private bool uptrg = true;
    public AudioSource audioSource;
    public AudioClip se;
    public bool staytrg = false;
    public GameObject[] connect_obj = null;
    public wallbutton[] connect_button = null;
    public string btn_tagname = "btn";
    public string wallcolor_tagname = "wall_red";
    private void Start()
    {
        Invoke(nameof(SetBTN), 0.31f);
    }
    void SetBTN()
    {
        wallrocks = GameObject.FindGameObjectsWithTag(wallcolor_tagname);
        if (wallrocks.Length > 0)
        {
            wallanims = new Animator[wallrocks.Length];
            for (int i = 0; i < wallrocks.Length;)
            {
                if (wallrocks[i].GetComponent<Animator>()) wallanims[i] = wallrocks[i].GetComponent<Animator>();
                i++;
            }

        }
        connect_obj = GameObject.FindGameObjectsWithTag(btn_tagname);
        if (connect_obj.Length > 0)
        {
            connect_button = new wallbutton[connect_obj.Length];
            for (int i = 0; i < connect_obj.Length;)
            {
                if (connect_obj[i].GetComponent<wallbutton>()) connect_button[i] = connect_obj[i].GetComponent<wallbutton>();
                i++;
            }
        }
    }
    void ButtonAnimSet(int setn = 1)
    {
        buttonanim.SetInteger("Anumber", setn);
        if (setn != 0)
        {
            var tmp = 0;
            for (int l = 0; l < connect_button.Length;)
            {
                if (connect_button[l] != null && connect_button[l].staytrg && staytrg)
                    tmp += 1;
                l++;
            }
            if (connect_button == null || connect_button.Length < 1 || (connect_button.Length > 0 && tmp >= connect_button.Length && staytrg))
            {
                if (wallanims != null && wallanims.Length > 0)
                {
                    for (int i = 0; i < wallanims.Length;)
                    {
                        wallanims[i].SetInteger("Anumber", setn);
                        i++;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "player" || col.tag == "wall")
        {
            staytrg = true;
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(se);
            if (uptrg)
            {
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
