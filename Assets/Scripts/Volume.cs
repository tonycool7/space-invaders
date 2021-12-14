using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider slider; 
    
    void Awake()
    {
        slider.onValueChanged.AddListener((float value) => this.SetVolume(value));
    }

    void SetVolume(float value)
    {
        AudioListener.volume = value * 10;
    }

    float ConvertToDecimal(float _value)
    {
        return Mathf.Log10(_value) * 20f;
    }
}
