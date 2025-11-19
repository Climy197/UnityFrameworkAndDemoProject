using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class UISlider : UIBase
{
    override public ComponentType Type => ComponentType.standard;
    Slider m_slider;
    public float value
    {
        set
        {
            m_slider.value = value;
        }
        get
        {
            return m_slider.value;
        }
    }

    protected override void OnInit()
    {
        base.OnInit();
        m_slider = this.GetComponent<Slider>();
    }
}