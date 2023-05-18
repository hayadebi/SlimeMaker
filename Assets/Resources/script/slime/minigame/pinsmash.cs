using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinsmash : MonoBehaviour
{
    public Rigidbody rb=null;
    public Transform connect_body;
    public bool se_ok = false;
    //回転
    public bool use_r = true;
    public bool jumpertrg = false;
    private float torque;
    private float addangle = 0f;
    public AudioSource _audio;
    public AudioClip[] se;
    public float addpower = 4;
    private bool dstrg = false;
    public bool bluetrg = true;
    public GameObject effect;
    public GameObject GameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jumpertrg && !GManager.instance.over && GManager.instance.walktrg )
        {
            if (se_ok&&(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
                GManager.instance.setrg = 15;
            torque = 0;
            if (use_r)
            {
                if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && addangle > -60)
                {
                    addangle -= 15f;
                    torque = -15;
                }
                else if( addangle < 30)
                {
                    addangle += 15f;
                    torque = 15;
                }
                connect_body.Rotate(new Vector3(0, 0, torque));
            }
            else if (!use_r)
            {
                if ((Input.GetMouseButton(0) || Input.GetMouseButton(1) ) && addangle < 60)
                {
                    addangle += 15f;
                    torque = 15;
                }
                else if (addangle > -30)
                {
                    addangle -= 15f;
                    torque = -15;
                }
                connect_body.Rotate(new Vector3(0, 0, torque));
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (rb != null && col.tag != "pinsmash")
        {
            _audio.Stop();
            _audio.PlayOneShot(se[1]);
        }
        
    }
    private void OnTriggerStay(Collider col)
    {
        if (rb != null && col.tag == "addpower")
        {
            // 向きの生成（Z成分の除去と正規化）
            Vector3 shotForward = Vector3.Scale((col.ClosestPointOnBounds(this.transform.position) - transform.position), new Vector3(1, 1, 0)).normalized;
            rb.velocity = shotForward * -Random.Range(addpower - 0.4f, addpower + 0.4f);
        }
        if (rb != null && col.tag == "pinsmash" && col.ClosestPointOnBounds(this.transform.position).y <= this.transform.position.y && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
        {
            _audio.Stop();
            // 向きの生成（Z成分の除去と正規化）
            Vector3 shotForward = Vector3.Scale((col.ClosestPointOnBounds(this.transform.position) - transform.position), new Vector3(1, 1, 0)).normalized;
            rb.velocity = shotForward * -Random.Range(4 - 0.3f, 4 + 0.3f);
            _audio.PlayOneShot(se[0]);
        }
        if(rb!=null &&col.tag=="red" && !dstrg)
        {
            dstrg = true;
            GManager.instance.minigame_score -= 500;
            if (bluetrg)
                GManager.instance.minislime_blue = false;
            else if (!bluetrg)
                GManager.instance.minislime_red = false;
            if(!GManager.instance.minislime_blue && !GManager.instance.minislime_red)
            {
                GManager.instance.over = true;
                GManager.instance.walktrg = false;
                if (GameOverUI != null)
                    Instantiate(GameOverUI, transform.position, transform.rotation);
                else
                    print("ミニゲームオーバー");
            }
            GManager.instance.mini_loadtime = 10f;
            GManager.instance.setrg = 4;
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
