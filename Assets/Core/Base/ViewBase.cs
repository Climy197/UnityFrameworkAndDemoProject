using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ViewBase
{
    internal GameObject m_root;
    private bool m_Active = false;
    private object m_Data;
    internal void BindData(object data)
    {
        this.m_Data = data;
    }
    internal void Show()
    {
        if (this.m_Active)
        {
            return;
        }
        this.m_Active = true;
        this.m_root.transform.SetAsLastSibling();
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
        GameObject.Destroy(this.m_root);
        this.OnDestroy();
    }

    internal async UniTask<UIBase> Init(Transform parent)
    {
        var content = await this.OnInit();
        this.m_root = content.gameObject;
        content.transform.SetParent(parent);
        content.transform.localPosition = Vector3.zero;
        content.transform.localRotation = Quaternion.identity;
        content.transform.localScale = Vector3.one;
        var rect = content.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

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