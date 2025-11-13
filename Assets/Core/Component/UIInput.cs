using TMPro;

public class UIInput : UIBase
{
    override public ComponentType Type => ComponentType.standard;
    private TMP_InputField m_InputField;
    public string text
    {
        set
        {
            m_InputField.text = value;
        }
        get
        {
            return m_InputField.text;
        }
    }
    protected override void OnInit()
    {
        m_InputField = this.GetComponent<TMP_InputField>();
    }
}