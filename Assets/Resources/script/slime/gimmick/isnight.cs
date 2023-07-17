using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isnight : MonoBehaviour
{
    public bool destroytrg = true;
    public Renderer[] _sprite;
    public Collider[] _col;
    public Rigidbody rb;
    public GameObject psystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((_sprite[0].enabled && GManager.instance.nighttrg && destroytrg)|| (_sprite[0].enabled && !GManager.instance.nighttrg && !destroytrg))
        {
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
            if (psystem != null) psystem.SetActive(false);
            foreach (Renderer sp in _sprite)
            {
                sp.enabled = false;
            }
            foreach (Collider tmp in _col)
            {
                tmp.enabled = false;
            }
        }
        if ((!_sprite[0].enabled && !GManager.instance.nighttrg && destroytrg) || (!_sprite[0].enabled && GManager.instance.nighttrg && !destroytrg))
        {
            foreach (Renderer sp in _sprite)
            {
                sp.enabled = true;
            }
            foreach (Collider tmp in _col)
            {
                tmp.enabled = true;
            }
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            if (psystem != null) psystem.SetActive(true);
        }
    }
}
