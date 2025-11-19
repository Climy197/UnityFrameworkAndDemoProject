using UnityEngine;

[RequireComponent(typeof(Transform))]
public class UITransform : UIBase
{
    private Transform m_transform;
    public Transform Transform => m_transform;
    protected override void OnInit()
    {
        base.OnInit();
        m_transform = this.GetComponent<Transform>();
    }
}