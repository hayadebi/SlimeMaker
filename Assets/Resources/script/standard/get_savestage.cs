using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class get_savestage : MonoBehaviour
{
    public int get_evid;
    public int get_evselectstage;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Ev" + get_evid.ToString() + "_" + get_evselectstage.ToString(), 0) < 1)
            Destroy(gameObject);
    }

}
