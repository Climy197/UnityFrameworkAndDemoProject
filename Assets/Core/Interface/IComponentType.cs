public interface IComponentType
{
    ComponentType Type { get; }
}
public enum ComponentType
{
    None = 0,
    standard = 1,
    costume = 2,
}