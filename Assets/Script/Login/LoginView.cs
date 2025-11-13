using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;


public partial class LoginView : ViewBase
{
    LoginPanel panel;
    LoginModule m_login;
    Account m_account;

    protected override async UniTask<UIBase> OnInit()
    {
        panel = await UIExtension.Create<LoginPanel>();
        this.panel.Register.onClick.AddListener(this.Login);
        this.panel.Login.onClick.AddListener(this.Register);
        m_login = Game.instance.Login;
        m_account = new Account();
        return panel;
    }

    private void Register()
    {

    }

    public void Login()
    {
        m_login.Login(m_account);
    }

    protected override void OnShow()
    {

    }
    protected override void OnHide()
    {

    }

    protected override void OnDestroy()
    {

    }

}