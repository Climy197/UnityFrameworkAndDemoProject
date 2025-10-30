using System.Threading.Tasks;
using Cysharp.Threading.Tasks;


public partial class LoginView : ViewBase
{
    public void Login()
    {

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

    protected override async UniTask<UIBase> OnInit()
    {
        var panel = await UIExtension.Create<LoginPanel>();
        return panel;
    }
}