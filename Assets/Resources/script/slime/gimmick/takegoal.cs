using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takegoal : MonoBehaviour
{
    private GameObject[] tanzaku;
    public int tanzaku_num=0;
    public GameObject[] lv_goalsprite;
    [System.Serializable]
    public struct LvGoal
    {
        public bool[] set_active;
    }
    public LvGoal[] lv_goal;
    public GameObject effect;
    private AudioSource _audio;
    public AudioClip se;
    // Start is called before the first frame update
    void Start()
    {
       
        Invoke(nameof(StartSet), 0.2f);
    }
    void StartSet()
    {
        tanzaku = GameObject.FindGameObjectsWithTag("bamboo");
        tanzaku_num = tanzaku.Length;
        SetGoal(0);
    }
    public void SetGoal(int addnum = -1)
    {
        tanzaku_num += addnum;
        _audio.PlayOneShot(se);
        Instantiate(effect, transform.position, transform.rotation);
        if (tanzaku_num == tanzaku.Length)
        {
            for (int i = 0; i < lv_goalsprite.Length;)
            {
                lv_goalsprite[i].SetActive(lv_goal[0].set_active[i]);
            }
        }
        else if (tanzaku_num <= 0)
        {
            for (int i = 0; i < lv_goalsprite.Length;)
            {
                lv_goalsprite[i].SetActive(lv_goal[2].set_active[i]);
            }
            this.gameObject.tag = "goal";
        }
        else if (tanzaku_num <= tanzaku.Length/2)
        {
            for (int i = 0; i < lv_goalsprite.Length;)
            {
                lv_goalsprite[i].SetActive(lv_goal[1].set_active[i]);
            }
        }
    }
}
