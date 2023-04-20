using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる
using UnityEngine.SceneManagement;

public class slider : MonoBehaviour
{
    public string sliderType = "audio";
    public float get_num = 0;
    Slider _slider;
    void Start()
    {
        // スライダーを取得する
        _slider = this.GetComponent<Slider>();
        if (sliderType == "audio")
        {
            _slider.value = GManager.instance.audioMax;
        }
        else if (sliderType == "se")
        {
            _slider.value = GManager.instance.seMax;
        }
    }

    void Update()
    {
        if (sliderType == "audio" && GManager.instance.audioMax != _slider.value)
        {
            GManager.instance.audioMax = _slider.value;
        }
        else if (sliderType == "se" && GManager.instance.seMax != _slider.value)
        {
            GManager.instance.seMax = _slider.value;
        }
    }
}