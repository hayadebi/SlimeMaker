using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class player : MonoBehaviour
{
    //【今後修正しながら使っていくプレイヤースクリプトの原型】

    [Header("停止気にするな")] private bool stoptrg = false;//優先度高めにプレイヤーを停止させるトリガー
    public float playerspeed = 2;
    private float oldp_y = 0;
    public float gravity = 32;//重力値
    public Transform character;//プレイヤーに対応するトランスフォーム
    public Transform body;//プレイヤーのモデルに対応するトランスフォーム
    private bool movetrg = false;//移動時にアニメーションさせるかどうか
    public string numbername;//アニメーターの変数名
    //移動で加算させるxyzそれぞれの値
    private float xSpeed = 0;
    private float ySpeed = 0;
    private float zSpeed = 0;
    //プレイヤーのサウンド関係
    public AudioClip groundse;
    public AudioSource audioSource;

    public Animator anim;//プレイヤーのアニメーションセット
    public Rigidbody rb;//プレイヤーの物理挙動をセット
    //回転に使う値
    private float X_Rotation = 0;
    private float Y_Rotation = 0;
    private Vector3 mXAxiz;
    private Vector3 cmAxiz;

    public int move_num = 1;
    public int stand_anim = 0;
    public int walk_anim = 1;
    private GameObject cm;
    private Vector3 iceoldvec;
    private bool icese = false;
    private GameObject[] goals=null;
    private takegoal[] takegs=null;
    private GameObject adcmpos = null;
    // Start is called before the first frame update

    void Start()
    {
        GManager.instance.cleartime = 0;
        GManager.instance.cleartrg = false;
        GManager.instance.goal_num = 0;
        audioSource = this.GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        mXAxiz = body.transform.localEulerAngles;
        latestPos = character.transform.position;  //前回のPositionの更新
        cm = GameObject.Find("Main Camera");
        adcmpos = GameObject.Find("adcmpos");
        Invoke(nameof(StartSet), 0.2f);
    }
    void StartSet()
    {
        goals = GameObject.FindGameObjectsWithTag("takegoal");
        if (goals != null && goals.Length >0)
        {
            takegs = new takegoal[goals.Length];
            for (int i = 0; i < goals.Length;)
            {
                takegs[i] = goals[i].GetComponent<takegoal>();
                i++;
            }
        }
    }
    //視点回転に関する
    private  float maxYAngle = 135f;
    private float minYAngle = 45f;
    private Vector3 latestPos;  //前回のPosition
    private bool goaltrg = false;

    public float kando = 15;
    private bool icetrg = false;
    private float icewall_time = 0;
    void FixedUpdate()
    {
        if (GManager.instance.walktrg && !GManager.instance.over && !stoptrg)
        {
            if (rb.useGravity)
                rb.useGravity = false;
            X_Rotation = 0;
            Y_Rotation = 0;
            xSpeed = 0;
            ySpeed = -gravity;
            //----ここからは移動----
            if (!movetrg &&!icetrg&& (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            {
                //この部分では歩きの効果音、アニメーションを操作
                movetrg = true;
                audioSource.clip = groundse;
                audioSource.loop = true;
                audioSource.Play();
                anim.SetInteger(numbername, walk_anim);
            }
            //移動メイン部分
            var inputX = Input.GetAxisRaw("Horizontal");
            var inputZ = Input.GetAxisRaw("Vertical");

            if (!icetrg || (transform.position - iceoldvec).magnitude <= 0.00001f)
            {
                var tempVc = new Vector3(move_num * inputX, 0, inputZ);
                if (tempVc.magnitude > 1) tempVc = tempVc.normalized;
                var vec = tempVc;
                var movevec = vec * playerspeed + Vector3.up * ySpeed;
                rb.velocity = movevec;
            }
            else if (icetrg)
            {
                inputX = 0;
                inputZ = 0;
                var tempVc = character.transform.forward *2f;
                if (tempVc.magnitude > 1) tempVc = tempVc.normalized;
                var vec = tempVc;
                var movevec = vec * (playerspeed*2) + Vector3.up *ySpeed;
                rb.velocity = movevec;
                iceoldvec = transform.position;
            }
            
            Vector3 targetPositon = latestPos;
            // 高さがずれていると体ごと上下を向いてしまうので便宜的に高さを統一
            if (character.transform.position.y != latestPos.y)
                targetPositon = new Vector3(latestPos.x, character.transform.position.y, latestPos.z);
            Vector3 diff = character.transform.position - targetPositon;
            if (diff != Vector3.zero && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                Quaternion targetRotation = Quaternion.LookRotation(diff);
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, Time.deltaTime * kando);
            }
            latestPos = character.transform.position;  //前回のPositionの更新

            if ((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))||icetrg)
            {
                //移動してない場合、またはジャンプ中の時はアニメーションや音を止める
                if (movetrg)
                    movetrg = false;
                anim.SetInteger(numbername, stand_anim);
                audioSource.loop = false;
                audioSource.Stop();
            }
        }
        else if ((!GManager.instance.walktrg|| GManager.instance.over) && anim.GetInteger(numbername)!= stand_anim)
        {
            audioSource.Stop();
            anim.SetInteger(numbername, stand_anim);
            ySpeed = 0;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
        
    }
    private void OnTriggerStay(Collider col)
    {
        if (!GManager.instance.over && GManager.instance.walktrg)
        {
            if (col.tag == "icewall" && !goaltrg && icetrg)
            {
                icewall_time += Time.deltaTime;
                if (icewall_time >= 3f)
                {
                    icetrg = false;
                    icese = false;
                    icewall_time = 0;
                }

            }
            if (!GManager.instance.over&&(col.tag == "red" || col.tag == "bullet" || col.tag == "icebullet"))
            {
                GManager.instance.setrg = 4;
                if (col.tag == "bullet")
                    Destroy(col.gameObject);
                iTween.ShakePosition(cm.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f));
                GManager.instance.over = true;
                Instantiate(GManager.instance.all_ui[2], transform.position, transform.rotation);
                Instantiate(GManager.instance.all_ui[7], transform.position, transform.rotation);
                Destroy(gameObject, 0.1f);
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (!GManager.instance.over && GManager.instance.walktrg && (col.tag == "ice"|| col.tag == "icewall") && !icese && !goaltrg)
        {
            icese = true;
            GManager.instance.setrg = 12;
        }
        else if (!GManager.instance.over && GManager.instance.walktrg && (col.tag != "ice" && col.tag != "icewall") && icese)
        {
            icese = false;
        }
        if (!GManager.instance.over && GManager.instance.walktrg && (col.tag == "ice" || col.tag == "icewall") && !icetrg)
        {
            icewall_time = 0;
            icetrg = true;
        }
        if (!GManager.instance.over && GManager.instance.walktrg && (col.tag != "ice" && col.tag != "icewall") && icetrg)
        {
            icetrg = false;
            icese = false;
        }
        if (!GManager.instance.over && GManager.instance.walktrg)
        {
            if (col.tag == "red" || col.tag == "bullet"||col.tag=="icebullet")
            {
                GManager.instance.setrg = 4;
                if (col.tag == "bullet")
                    Destroy(col.gameObject);
                iTween.ShakePosition(cm.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f));
                GManager.instance.over = true;
                Instantiate(GManager.instance.all_ui[2], transform.position, transform.rotation);
                Instantiate(GManager.instance.all_ui[7], transform.position, transform.rotation);
                Destroy(gameObject, 0.1f);
            }
            else if (col.tag == "goal" && !goaltrg)
            {
                goaltrg = true;
                GManager.instance.goal_num += 1;
                GManager.instance.setrg = 7;
                Instantiate(GManager.instance.all_ui[4], transform.position, transform.rotation);
                //if (adcmpos != null)
                //    Instantiate(GManager.instance.all_ui[8], adcmpos.transform.position, adcmpos.transform.rotation);
                if (GManager.instance.goal_num < 2)
                {
                    GManager.instance.setrg = 5;
                }
                else
                {
                    GManager.instance.setrg = 6;
                    GManager.instance.cleartrg = true;
                    if (goals != null && goals.Length > 0)
                    {
                        for (int i = 0; i < takegs.Length;)
                        {
                            takegs[i].goal_tanzaku.SetActive (true);
                            i++;
                        }
                    }
                    if (GManager.instance.debug_trg)
                        Instantiate(GManager.instance.all_ui[5], transform.position, transform.rotation);
                    else
                        Instantiate(GManager.instance.all_ui[6], transform.position, transform.rotation);
                    if (GManager.instance.globalev_id != -1 && GManager.instance.globalev_stageselect != -1 && PlayerPrefs.GetInt("Ev" + GManager.instance.globalev_id.ToString() + "_" + GManager.instance.globalev_stageselect.ToString(), 0)<1)
                        PlayerPrefs.SetInt("Ev" + GManager.instance.globalev_id.ToString() + "_" + GManager.instance.globalev_stageselect.ToString(), 1);
                }
                iTween.ShakePosition(cm.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f));
                Destroy(gameObject, 0.1f);
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        
    }

}
