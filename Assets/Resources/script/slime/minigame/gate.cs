using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
    public Transform target_tppos;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "player")
        {
            GManager.instance.minigame_score += 100;
            GManager.instance.setrg = 18;
            Vector3 tmp = col.transform.position;
            tmp.x = target_tppos.position.x;
            tmp.y = target_tppos.position.y;
            col.transform.position = tmp;
            Instantiate(effect, transform.position, transform.rotation);
            Instantiate(effect, target_tppos.position, target_tppos.rotation );
        }
    }
}
