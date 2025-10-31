using UnityEngine;
using UnityEngine.UI;
public class UIImage : UIBase
{
    override public ComponentType Type => ComponentType.standard;
    Image m_image;
    public Sprite sprite
    {
        set
        {
            m_image.sprite = value;
        }
        get
        {
            return m_image.sprite;
        }
    }


}