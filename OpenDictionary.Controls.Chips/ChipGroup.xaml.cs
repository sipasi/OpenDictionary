using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Maui.Controls;

namespace OpenDictionary.Controls.Chips;

public partial class ChipGroup : ContentView
{
    private readonly List<Chip> chips = new();

    private Chip? selectedChip;

    public static readonly BindableProperty ItemSourceProperty = BindableBuilder.Create<ChipGroup, IEnumerable>()
        .WithName(nameof(ItemSource))
        .WithPropertyChanged(OnItemSource);

    public static readonly BindableProperty SelectedProperty = BindableBuilder.Create<ChipGroup, object>()
        .WithName(nameof(Selected))
        .WithDefaultBinding(BindingMode.TwoWay)
        .WithPropertyChanged(OnSelected);

    public static readonly BindableProperty SpacingProperty = BindableBuilder.Create<ChipGroup, int>()
        .WithName(nameof(Spacing))
        .WithPropertyChanged(static (view, old, current) => view.layout.Spacing = current);

    public IEnumerable ItemSource
    {
        get => (IEnumerable<object>)GetValue(ItemSourceProperty);
        set => SetValue(ItemSourceProperty, value);
    }

    public object? Selected
    {
        get { return GetValue(SelectedProperty); }
        set { SetValue(SelectedProperty, value); }
    }

    public int Spacing
    {
        get { return (int)GetValue(SpacingProperty); }
        set { SetValue(SpacingProperty, value); }
    }

    public ChipGroup()
    {
        InitializeComponent();

        layout.ChildAdded += OnLayoutChildAdded;
        layout.ChildRemoved -= OnLayoutChildRemoved;
    }

    private void Add(Chip chip)
    {
        chip.Enable();

        chips.Add(chip);
        layout.Add(chip);
    }
    private void Remove(Chip chip)
    {
        chip.Disable();
        chips.Remove(chip);
        layout.Remove(chip);
    }
    private void Clear()
    {
        foreach (var chip in chips)
        {
            chip.Disable();
        }

        chips.Clear();
        layout.Clear();
    }

    private void OnLayoutChildAdded(object? sender, ElementEventArgs e)
    {
        if (e.Element is Chip chip && chips.Contains(chip))
        {
            chip.PropertyChanged += OnChipPropertyChanged;

            UpdateChildrenLayout();
        }
    }
    private void OnLayoutChildRemoved(object? sender, ElementEventArgs e)
    {
        if (e.Element is Chip chip && chips.Contains(chip))
        {
            chips.Remove(chip);

            chip.PropertyChanged -= OnChipPropertyChanged;

            chip.Disable();

            UpdateChildrenLayout();
        }
    }

    private void OnChipPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not nameof(Chip.IsSelected) || sender is not Chip chip)
        {
            return;
        }

        if (chip.IsSelected && Selected != chip)
        {
            if (selectedChip is not null)
            {
                selectedChip!.IsSelected = false;
            }

            selectedChip = chip;

            Selected = chip.Value;
        }
        else if (!chip.IsSelected && Selected == chip)
        {
            Selected = null;
            selectedChip = null;
        }
    }

    private static void OnItemSource(ChipGroup group, IEnumerable? old, IEnumerable? current)
    {
        bool any = current?.OfType<object>().Any() ?? false;

        group.Clear();

        if (any is not true)
        {
            return;
        }

        foreach (var item in current!)
        {
            Chip chip = new()
            {
                Text = item.ToString() ?? "Empty",
                Value = item,
            };

            group.Add(chip);
        }
    }
    private static void OnSelected(ChipGroup owner, object? old, object? current)
    {
        if (current is null)
        {
            return;
        }

        Chip? chip = owner.chips.Find(chip => chip.Value?.Equals(current) ?? false);

        if (chip is null || chip.IsSelected)
        {
            return;
        }

        chip.IsSelected = true;
    }
}