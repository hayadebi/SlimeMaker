using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bazooka : MonoBehaviour
{
    public GameObject shoteffect;
    public Transform shotpos;
    public float bulletspeed=8;
    private int shotnumber = 1;
    public GameObject Bullet;
    private GameObject tmp_bullet = null;
    public Animator anim;
    private float count_time = 0f;
    private float shot_time = 3f;
    public bool minigametrg = false;
    // Start is called before the first frame update
    void Start()
    {
        if(minigametrg )
            Invoke(nameof(StartShot), 1f);
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
                if(count_time >= shot_time)
                {
                    count_time = 0;
                    Shot();
                }
            }
        }
    }
    void StartShot()
    {
        Shot();
    }
    public void Shot()
    {
        anim.SetBool("Abool", true);
        Instantiate(shoteffect, shotpos.transform.position, this.transform.rotation);
        GManager.instance.setrg = 9;
        if (Bullet != null)
        {
            var tmp = Instantiate(Bullet, shotpos.transform.position, this.transform.rotation);
            tmp_bullet = tmp;
            if (minigametrg)
            {
                // 向きの生成（Z成分の除去と正規化）
                Vector3 shotForward = Vector3.Scale((shotpos.transform.position - transform.position), new Vector3(1, 1, 0)).normalized;
                tmp.GetComponent<Rigidbody>().velocity = shotForward * bulletspeed;
            }
            else
                tmp.GetComponent<Rigidbody>().velocity = -transform.forward * bulletspeed;
        }
    }
}
