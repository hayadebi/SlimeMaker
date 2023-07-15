using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniscoreobj : MonoBehaviour
{
    public int min_getscore = 500;
    public int max_getscore = 501;//同値にしたい場合はmaxに+1
    public int destroy_getscore = 0;
    public float destroytime = 0.31f;
    public int set_setrg = 6;
    public int tweenanim_set = 0;//-1でiTweenアニメーション設定無効化
    public GameObject effect = null;//nullでエフェクト生成無し
    public int hp_count = 1;//0でデストロイへ移行、9999はHP設定無限化
    private bool dstrg = false;
    public Sprite damage_sprite=null;
    public SpriteRenderer _sprite=null;
    private Sprite old_sprite=null;
    public PhysicMaterial pm=null;
    public float remove_pm = 0f;
    public bool curetrg = false;
    public bool startanimtrg = false;
    public float not_startentertime = -1f;
    private float tmp_x = 0;
    private float tmp_z = 0;
    public Transform set_parentpos = null;
    public resummon rs = null;
    public widthmove wm = null;
    public bool bosstrg = false;
    // Start is called before the first frame update
    void Start()
    {
        if (rs != null)
            rs.tmpobj = this.gameObject;
        tmp_x = transform.localScale.x;
        tmp_z = transform.localScale.z;
        if (_sprite != null)
            old_sprite = _sprite.sprite;
        if (wm != null) wm.movetrg = true;
        if (startanimtrg)
            Invoke("TweenAnim_" + tweenanim_set.ToString(), 0.001f);

    }

    // Update is called once per frame
    void Update()
    {
        if (not_startentertime >= 0f)
            not_startentertime -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "player" && !dstrg && hp_count > 0 && not_startentertime <= 0f)
        {
            if(hp_count < 9999)
                hp_count -= 1;
            if (pm != null )
                pm.bounciness += remove_pm;
            if (hp_count < 1)
                dstrg = true;
            if (bosstrg && hp_count != GManager.instance.tmp_bosscount)
                GManager.instance.tmp_bosscount = hp_count;
            if (set_setrg > -1)
                GManager.instance.setrg = set_setrg;
            
            if (old_sprite != null && damage_sprite != null && _sprite != null)
                _sprite.sprite = damage_sprite;
            GManager.instance.minigame_score += Random.Range(min_getscore, max_getscore);
            if (tweenanim_set != -1 && !dstrg)
                Invoke("TweenAnim_" + tweenanim_set.ToString(), 0.001f);
            else
                Invoke("TweenAnim_" + 0.ToString(), 0.001f);
            if (dstrg)
            {
                if (wm != null) wm.movetrg = false;
                if (effect != null && !set_parentpos)
                {
                    GameObject tobj=Instantiate(effect, transform.position, transform.rotation);
                    tobj.SetActive(true);
                }
                if (curetrg && pm != null)
                    pm.bounciness = 0.99f;
                else if (effect != null && set_parentpos)
                {
                    GameObject tobj=Instantiate(effect, set_parentpos.position, set_parentpos.rotation,set_parentpos.parent);
                    tobj.transform.position = set_parentpos.position;
                    tobj.transform.localScale = set_parentpos.localScale;
                    tobj.SetActive(true);
                }
                GManager.instance.minigame_score += destroy_getscore;
                if (destroytime == 0f || destroytime == -1)
                    Destroy(this.gameObject.gameObject);
                else
                    Destroy(this.gameObject.gameObject, destroytime);
            }
        }
    }
    void TweenAnim_0()//normal or ds
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", tmp_x*2f, "z", tmp_z*2f, "time",0.15f));
        iTween.ScaleTo(gameObject, iTween.Hash("x", tmp_x*0.01f, "z", tmp_z*0.01f, "time", 0.2f, "delay", 0.151f));
        if (old_sprite != null && damage_sprite != null && _sprite != null)
            Invoke(nameof(SpriteReset), 0.35f);
    }
    void TweenAnim_1()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", tmp_x * 2f, "z", tmp_z * 2f, "time", 0.15f));
        iTween.RotateAdd(gameObject, iTween.Hash("y", 360f,"time", 0.35f));
        iTween.ScaleTo(gameObject, iTween.Hash("x", tmp_x, "z", tmp_z, "time", 0.2f, "delay", 0.151f));
        if (old_sprite != null && damage_sprite != null && _sprite != null)
            Invoke(nameof(SpriteReset), 0.35f);
    }
    void TweenAnim_2()
    {
        iTween.ShakePosition(this.gameObject, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 0.2f));
    }

    void SpriteReset()
    {
        if (old_sprite != null && damage_sprite != null && _sprite != null)
            _sprite.sprite = old_sprite;
    }
}
