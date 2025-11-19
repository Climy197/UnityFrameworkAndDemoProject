using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class LoginView : ViewBase
{
    LoginPanel panel;
    LoginModule m_login;
    Account m_account;

    protected override async UniTask<UIBase> OnInit()
    {
        panel = await UIExtension.Create<LoginPanel>();
        this.panel.Register.onClick.AddListener(async () => await this.Register());
        this.panel.Login.onClick.AddListener(async () => await this.Login());
        this.panel.AccountID.Input.onValueChanged.AddListener(this.OnAccountIDChanged);
        this.panel.Password.Input.onValueChanged.AddListener(this.OnPasswordChanged);
        m_login = Game.instance.Login;
        m_account = new Account();
        return panel;
    }

    private void OnPasswordChanged(string code)
    {
        m_account.password = code;
    }

    private void OnAccountIDChanged(string value)
    {
        m_account.account = value;
    }

    private async UniTask Register()
    {
        if (m_account.account.Length == 0)
        {
            Debug.Log("AccountID is empty");
            return;
        }
        if (m_account.password.Length == 0)
        {
            Debug.Log("Password is empty");
            return;
        }
        await m_login.Register(m_account);
    }

    public async UniTask Login()
    {
        if (m_account.account.Length == 0)
        {
            Debug.Log("AccountID is empty");
            return;
        }
        if (m_account.password.Length == 0)
        {
            Debug.Log("Password is empty");
            return;
        }
        var result = await m_login.Login(m_account);
        if (result == 0)
        {
            await App.Instance.UI.HomeView<MainView>();
        }
        else
        {
            Debug.Log("Login failed");
        }
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