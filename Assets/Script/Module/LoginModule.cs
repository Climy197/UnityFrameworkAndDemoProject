using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LoginModule : IModule
{
    public async UniTask Init()
    {
        await App.Instance.DB.AsyncCreateTable<Account>();
    }
    public async Task<int> Login(Account account)
    {
        var result = await App.Instance.DB.AsyncQuery<Account>($"{nameof(Account.account)}  = ? ", account.account);
        if (result.Count == 0)
        {
            Debug.Log("AccountID not found");
            return -1;
        }
        else
        {
            if (result[0].password == account.password)
            {

                Debug.Log("Login Success");
                return 0;
            }
            else
            {
                Debug.Log("Password not match");
                return 1;
            }
        }
    }
    public async Task<int> Register(Account account)
    {
        var queryResult = await App.Instance.DB.AsyncQuery<Account>($"{nameof(Account.account)}  = ? ", account.account);
        if (queryResult.Count != 0)
        {
            Debug.Log("AccountID already exist");
            return -1;
        }
        var result = await App.Instance.DB.AsyncInsert(account);
        if (result == 0)
        {
            Debug.Log("Register Failed");
            return -1;
        }
        else
        {
            Debug.Log("Register Success");
            return 0;
        }
    }

    public void Save()
    {

    }
}