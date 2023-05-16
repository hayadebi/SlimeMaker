using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class togetrap : MonoBehaviour
{
    public SpriteRenderer _sprite;
    public Sprite[] on_or_off;
    public int offtrg = 0;
    public float check_time = 3f;
    private float count_time;
    public float shake_power = 0.24f;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "red";
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.walktrg && !GManager.instance.over)
        {
            count_time += Time.deltaTime;
            if(count_time >= check_time)
            {
                count_time = 0;
                GManager.instance.setrg = 3;
                iTween.ShakePosition(this.gameObject, iTween.Hash("x", shake_power, "y", shake_power, "time", 0.24f));
                if (offtrg == 0)
                {
                    this.gameObject.tag = "ground";
                    offtrg = 1;
                }
                else if (offtrg == 1)
                {
                    this.gameObject.tag = "red";
                    offtrg = 0;
                }
                _sprite.sprite = on_or_off[offtrg];
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (SceneManager.GetActiveScene().name == "minigame" && col.tag == "player" && this.gameObject.tag == "ground" && !GManager.instance.over && col.GetComponent<pinsmash>() && ((col.GetComponent<pinsmash>().bluetrg && GManager.instance.minislime_blue) || (!col.GetComponent<pinsmash>().bluetrg && GManager.instance.minislime_red)))
            GManager.instance.minigame_score += 200;
    }
}
