public partial class LoginPanel : UIBase, IBindable
{
    public override ComponentType Type => ComponentType.costume;
    private UIButton m_Register;
    private UIButton m_Login;
    public UIButton Register => m_Register;
    public UIButton Login => m_Login;

    void IBindable.BindComponent()
    {
        m_Register = this.transform.Find("Register").GetComponent<UIButton>();
        m_Login = this.transform.Find("Login").GetComponent<UIButton>();
    }
}