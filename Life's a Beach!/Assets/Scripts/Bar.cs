using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider mainSlider;

    protected bool initialised;

    protected virtual void Awake()
    {
        if (!mainSlider)
        {
            mainSlider = GetComponent<Slider>();
        }
    }

    protected virtual void Start() { }

    public virtual void Initialise(float value)
    {
        SetMaxValue(value);
        SetValue(value);
        initialised = true;
    }

    public virtual void SetMaxValue(float value)
    {
        mainSlider.maxValue = value;
    }

    public virtual void SetValue(float value)
    {
        mainSlider.value = value;
    }
}
