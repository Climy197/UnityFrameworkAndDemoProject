using System;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class UILabel : UIBase
{
    override public ComponentType Type => ComponentType.standard;
    TextMeshProUGUI m_TMP;
    public string text
    {
        set
        {
            m_TMP.text = value;
        }
        get
        {
            return m_TMP.text;
        }
    }
    protected override void OnInit()
    {
        m_TMP = this.GetComponent<TextMeshProUGUI>();
    }
}