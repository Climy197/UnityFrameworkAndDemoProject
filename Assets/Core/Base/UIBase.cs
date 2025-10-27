using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    protected abstract void OnInit();
    protected abstract void OnShow();
    protected abstract void OnHide();
    protected abstract void OnDestroy();
    void Awake()
    {
        this.OnInit();
    }
    void OnEnable()
    {
        this.OnShow();
    }
    void OnDisable()
    {
        this.OnHide();
    }
    void Dispose()
    {
        this.OnDestroy();
    }
}