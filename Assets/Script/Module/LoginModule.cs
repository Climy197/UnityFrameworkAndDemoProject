using UnityEngine;

public class LoginModule : IModule
{
    public void Init()
    {
       
    }
    public void Login(Account account)
    {
        Debug.Log($"Login AccountID: {account.AccountID}, Password: {account.Password}");
    }
}