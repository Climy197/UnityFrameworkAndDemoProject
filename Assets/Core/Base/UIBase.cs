using UnityEngine;

public abstract class UIBase : MonoBehaviour, IBindable
{
    protected virtual void OnInit()
    {

    }
    protected virtual void OnShow()
    {

    }
    protected virtual void OnHide()
    {

    }
    protected virtual void OnDestroy()
    {

    }

    private void Awake()
    {
        this.BindComponent();
        this.OnInit();
    }
    private void OnEnable()
    {
        this.OnShow();
    }
    private void OnDisable()
    {
        this.OnHide();
    }
    private void Dispose()
    {
        this.OnDestroy();
    }

    public abstract void BindComponent();
}