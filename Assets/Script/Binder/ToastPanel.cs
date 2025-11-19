using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToastPanel : UIBase, IBindable
{
    public override ComponentType Type => ComponentType.costume;
    public UIImage m_Bg { get; private set; }
    public UILabel m_content { get; private set; }
    public UIImage m_test { get; private set; }

    void IBindable.BindComponent()
    {
        m_Bg = transform.Find("Bg").GetComponent<UIImage>();
        m_content = transform.Find("content").GetComponent<UILabel>();
        m_test = transform.Find("content/test").GetComponent<UIImage>();
    }
}
