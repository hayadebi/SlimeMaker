using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniscoreobj : MonoBehaviour
{
    public int min_getscore = 500;
    public int max_getscore = 501;//同値にしたい場合はmaxに+1
    public float destroytime = 0.31f;
    public int set_setrg = 6;
    public int tweenanim_set = 0;//-1でiTweenアニメーション設定無効化
    public GameObject effect = null;//nullでエフェクト生成無し
    public int hp_count = 1;//0でデストロイへ移行、9999はHP設定無限化
    private bool dstrg = false;
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "player" && !dstrg && hp_count > 0)
        {
            if(hp_count < 9999)
                hp_count -= 1;
            if (hp_count < 1)
                dstrg = true;
            if (set_setrg > -1)
                GManager.instance.setrg = set_setrg;
            if (effect != null)
                Instantiate(effect, transform.position, transform.rotation);
            GManager.instance.minigame_score += Random.Range(min_getscore, max_getscore);
            if (dstrg)
            {
                if (destroytime == 0f || destroytime == -1)
                    Destroy(gameObject);
                else
                {
                    Destroy(gameObject, destroytime);
                    if(tweenanim_set !=-1)
                        Invoke("TweenAnim_"+tweenanim_set.ToString(),0.001f);
                }
            }
        }
    }
    void TweenAnim_0()
    {
        iTween.ScaleBy(gameObject, iTween.Hash("x", 2f, "z", 2f, "time",0.15f));
        iTween.ScaleBy(gameObject, iTween.Hash("x", 0.01f, "z", 0.01f, "time", 0.2f, "delay", 0.151f));
    }
}
