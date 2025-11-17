public partial class LoginPanel : UIBase, IBindable
{
    public override ComponentType Type => ComponentType.costume;
    private UIButton m_Register;
    private UIButton m_Login;
    private UIInput m_AccountID;
    private UIInput m_Password;
    public UIInput AccountID => m_AccountID;
    public UIInput Password => m_Password;
    public UIButton Register => m_Register;
    public UIButton Login => m_Login;

    void IBindable.BindComponent()
    {
        m_Register = this.transform.Find("Register").GetComponent<UIButton>();
        m_Login = this.transform.Find("Login").GetComponent<UIButton>();
        m_AccountID = this.transform.Find("Account").GetComponent<UIInput>();
        m_Password = this.transform.Find("PassWord").GetComponent<UIInput>();
    }
}