using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bazooka : MonoBehaviour
{
    public GameObject shoteffect;
    public Transform shotpos;
    public float bulletspeed = 8;
    private int shotnumber = 1;
    public GameObject Bullet;
    public GameObject[] Slimes;
    private GameObject tmp_bullet = null;
    public Animator anim;
    private float count_time = 0f;
    private float shot_time = 3f;
    public bool minigametrg = false;
    public PhysicMaterial pm = null;
    // Start is called before the first frame update
    void Start()
    {
        if (minigametrg)
        {
            GManager.instance.bz = this.GetComponent<bazooka>();
            GManager.instance.minislime_blue = false;
            GManager.instance.minislime_red = false;
            if(!GManager.instance.adstrg )
                GManager.instance.minigame_score = 0;
            
            if (pm != null)
                pm.bounciness = 0.99f;
            Invoke(nameof(StartShot), 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.walktrg && !GManager.instance.over && tmp_bullet == null && !minigametrg)
        {
            if (anim.GetBool("Abool"))
                anim.SetBool("Abool", false);
            else if (!anim.GetBool("Abool"))
            {
                count_time += Time.deltaTime;
                if (count_time >= shot_time)
                {
                    count_time = 0;
                    Shot();
                }
            }
        }
    }
    void StartShot()
    {
        GManager.instance.mini_loadtime = 6f;
        Shot();
    }
    public void Shot()
    {
        if (!GManager.instance.over)
        {
            int tmpnum = 0;
            anim.SetBool("Abool", true);
            Instantiate(shoteffect, shotpos.transform.position, this.transform.rotation);
            GManager.instance.setrg = 9;
            GameObject tmp = null;
            if (minigametrg)
            {
                if (!GManager.instance.minislime_blue) { tmpnum = 0; GManager.instance.minislime_blue = true; }
                else if (!GManager.instance.minislime_red) { tmpnum = 1; GManager.instance.minislime_red = true; }
                Instantiate(Slimes[tmpnum], shotpos.transform.position, this.transform.rotation);
            }
            else
                tmp = Instantiate(Bullet, shotpos.transform.position, this.transform.rotation);
            tmp_bullet = tmp;
            if (minigametrg && tmp != null)
            {
                // 向きの生成（Z成分の除去と正規化）
                Vector3 shotForward = Vector3.Scale((shotpos.transform.position - transform.position), new Vector3(1, 1, 0)).normalized;
                tmp.GetComponent<Rigidbody>().velocity = shotForward * Random.Range(bulletspeed - 0.3f, bulletspeed + 0.3f);
            }
            else if (tmp != null)
                tmp.GetComponent<Rigidbody>().velocity = -transform.forward * bulletspeed;
        }
    }
}
