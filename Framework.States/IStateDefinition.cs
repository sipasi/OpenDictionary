using System;

namespace Framework.States
{
    public interface IStateDefinition
    {
        Type Type { get; }
        IState GetState();
    }
}