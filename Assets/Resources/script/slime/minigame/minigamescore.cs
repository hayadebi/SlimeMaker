using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class minigamescore : MonoBehaviour
{
    public Text viewtext;
    private float viewx;
    private float viewy;
    public Text addtext;
    private int oldscore = 0;
    private float get_tmpa = 0f;
    private string tmpplus = "+";
    // Start is called before the first frame update
    void Start()
    {
        var tmp = addtext.color;
        tmp.a = 0f;
        addtext.color = tmp;
        get_tmpa = 0;
        viewx = transform.parent.gameObject.transform.localScale.x;
        viewy = transform.parent.gameObject.transform.localScale.y;
        viewtext.text = "SCORE:" + oldscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(GManager.instance.minigame_score!= oldscore)
        {
            tmpplus = "+";
            if (GManager.instance.minigame_score - oldscore < 0)
                tmpplus = "";
            addtext.text = tmpplus+ (GManager.instance.minigame_score - oldscore).ToString();
            get_tmpa = 1.3f;
            var tmp = addtext.color;
            tmp.a = get_tmpa;
            addtext.color = tmp;
            if (GManager.instance.minigame_score > 999999999)
                GManager.instance.minigame_score = 999999999;
            oldscore = GManager.instance.minigame_score;
            viewtext.text = "スコア:" + oldscore.ToString();
            iTween.ScaleTo(transform.parent.gameObject, iTween.Hash("x", viewx * 2f, "y", viewy * 2f, "time", 0.15f));
            iTween.ScaleTo(transform.parent.gameObject, iTween.Hash("x", viewx, "y", viewy, "time", 0.2f, "delay", 0.151f));
        }
        else if(addtext.color.a > 0)
        {
            get_tmpa -= Time.deltaTime;
            var tmp = addtext.color;
            tmp.a = get_tmpa;
            addtext.color = tmp;
        }
    }
}
