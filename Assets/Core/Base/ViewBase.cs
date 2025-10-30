using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ViewBase
{
    internal GameObject m_root;
    private bool m_Active = false;
    internal void Show()
    {
        if (this.m_Active)
        {
            return;
        }
        this.m_Active = true;
        this.m_root.SetActive(true);
        this.OnShow();
    }
    internal void Hide()
    {
        if (!this.m_Active)
        {
            return;
        }
        this.m_root.SetActive(false);
        this.m_Active = false;
        this.OnHide();
    }
    internal void Dispose()
    {
        this.OnDestroy();
    }
    public void BindRoot(GameObject root)
    {
        m_root = root;
    }
    internal async UniTask<UIBase> Init()
    {
        var content = await this.OnInit();
        content.transform.SetParent(m_root.transform);
        content.transform.localPosition = Vector3.zero;
        content.transform.localRotation = Quaternion.identity;
        content.transform.localScale = Vector3.one;
        return content;
    }
    protected abstract UniTask<UIBase> OnInit();

    protected virtual void OnShow()
    {

    }
    protected virtual void OnHide()
    {

    }
    protected virtual void OnDestroy()
    {

    }

}