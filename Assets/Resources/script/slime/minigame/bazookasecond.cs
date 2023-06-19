using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bazookasecond : MonoBehaviour
{
    public RawImage blue;
    public RawImage red;
    public Text secondText;
    public Image pin;
    private bool bazookaon = false;
    [Multiline]
    public string[] secondtemplate;
    public string[] secondisEnglish;
    private int old_second;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.mini_loadtime > 0 && GManager.instance.walktrg && !GManager.instance.over )
        {
            if (!bazookaon)
            {
                bazookaon = true;
                secondText.enabled = true;
                pin.enabled = true;
                old_second = (int)(GManager.instance.mini_loadtime- GManager.instance.mini_tmptime);
                secondText.text = secondtemplate[0] + "\n" + old_second.ToString() + secondisEnglish[0];
            }
            if (!GManager.instance.minislime_blue && !blue.enabled)
                blue.enabled = true;
            else if (!GManager.instance.minislime_red && !red.enabled && GManager.instance.minislime_blue)
                red.enabled = true;
            if (GManager.instance.minislime_blue && blue.enabled)
                blue.enabled = false;
            else if (GManager.instance.minislime_red && red.enabled)
                red.enabled = false;
            if (old_second != (int)(GManager.instance.mini_loadtime - GManager.instance.mini_tmptime))
            {
                old_second = (int)(GManager.instance.mini_loadtime - GManager.instance.mini_tmptime);
                secondText.text = secondtemplate[0] + "\n" + old_second.ToString() + secondisEnglish[0];
            }
        }
        else if(bazookaon)
        {
            bazookaon = false;
            blue.enabled = false;
            red.enabled = false;
            secondText.enabled = false;
            pin.enabled = false;
        }
        
    }
}
