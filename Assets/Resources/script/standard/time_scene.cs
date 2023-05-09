using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class time_scene : MonoBehaviour
{
    public float scenetime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(NextScene), scenetime);
    }
    void NextScene()
    {
        SceneManager.LoadScene(GManager.instance.loadscene_name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
