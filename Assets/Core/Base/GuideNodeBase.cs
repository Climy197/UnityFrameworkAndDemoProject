using Cysharp.Threading.Tasks;

public abstract class GuideNodeBase
{
    protected abstract bool m_Skip { get; }
    public abstract bool CheckCondition();
    public abstract bool CheckDone();
    public abstract UniTask Execute();
}