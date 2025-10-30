using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GuideManager : IManager
{
    bool m_OpenGuide = false;
    bool m_pause;
    private Stack<GuideNodeBase> m_GuideRoots = new Stack<GuideNodeBase>();
    async UniTask IManager.Init()
    {

    }

    void IManager.Dispose()
    {

    }
    public void Push(GuideNodeBase guideNode)
    {
        m_GuideRoots.Push(guideNode);
    }
    public void ExecuteGuide()
    {
        
    }
}