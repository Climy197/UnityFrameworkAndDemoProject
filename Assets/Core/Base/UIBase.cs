using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    protected abstract void BindComponent();
    protected abstract void Init();
    protected abstract void Show();
    protected abstract void Hide();
    protected virtual void OnInit()
    {
        BindComponent();
        Init();
    }

    protected virtual void OnHide()
    {
        Hide();
    }
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }
}