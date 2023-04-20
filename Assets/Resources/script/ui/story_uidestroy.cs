using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_uidestroy : MonoBehaviour
{
    public bool destroy_story = false;
    // Start is called before the first frame update
    void Start()
    {
        if (destroy_story && GManager.instance.storymode)
            Destroy(gameObject, 0.1f);
        else if (!destroy_story && !GManager.instance.storymode)
            Destroy(gameObject, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
