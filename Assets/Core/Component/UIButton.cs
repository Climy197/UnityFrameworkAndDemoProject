using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Button;
[RequireComponent(typeof(Button))]
public class UIButton : UIBase
{
    override public ComponentType Type => ComponentType.standard;
    Button m_button;
    public ButtonClickedEvent onClick
    {
        get
        {
            return m_button.onClick;
        }
    }
    protected override void OnInit()
    {
        m_button = GetComponent<Button>();
    }

}