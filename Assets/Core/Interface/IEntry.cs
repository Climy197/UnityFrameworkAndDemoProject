using System;
using Cysharp.Threading.Tasks;

public interface IEntry
{
    UniTask Init();
}
