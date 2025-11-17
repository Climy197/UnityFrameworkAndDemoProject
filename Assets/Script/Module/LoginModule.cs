using System.Threading.Tasks;
using UnityEngine;

public class LoginModule : IModule
{
    public void Init()
    {

    }
    public async Task<int> Login(Account account)
    {
        var result = await App.Instance.DB.AsyncQuery<Account>("SELECT * FROM Account WHERE AccountID = ? ", account.AccountID);
        if (result.Count == 0)
        {
            Debug.Log("AccountID not found");
            return -1;
        }
        else
        {
            if (result[0].Password == account.Password)
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
        var queryResult = await App.Instance.DB.AsyncQuery<Account>(" AccountID = ? ", account.AccountID);
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
}