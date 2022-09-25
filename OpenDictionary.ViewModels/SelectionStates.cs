#nullable enable

using System.Diagnostics;

namespace OpenDictionary.ViewModels;

public interface ISelectionState { }

public static class SelectionStates
{
    public static ISelectionState None { get; } = new NoneState();
    public static ISelectionState Indeterminate { get; } = new IndeterminateState();
    public static ISelectionState All { get; } = new AllState();

    [DebuggerDisplay(nameof(NoneState))] private sealed class NoneState : ISelectionState { }
    [DebuggerDisplay(nameof(IndeterminateState))] private sealed class IndeterminateState : ISelectionState { }
    [DebuggerDisplay(nameof(AllState))] private sealed class AllState : ISelectionState { }
}
