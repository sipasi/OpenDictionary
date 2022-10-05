using System.Collections.Generic;

using CommunityToolkit.Mvvm.ComponentModel;

using OpenDictionary.Collections.Extensions;
using OpenDictionary.Models;

namespace OpenDictionary.Games.WordConformities;

[INotifyPropertyChanged]
public sealed partial class GamePropertiesObservable : IProperties
{
    private readonly IList<Word> words;

    [ObservableProperty]
    private string groupName = string.Empty;

    [ObservableProperty]
    private string question = string.Empty;

    [ObservableProperty]
    private int total;

    [ObservableProperty]
    private int answered;

    [ObservableProperty]
    private int correct;
    [ObservableProperty]
    private int uncorrect;

    public GamePropertiesObservable(IList<Word> words)
    {
        this.words = words;
    }

    public void Reset()
    {
        Question = string.Empty;

        Answered = Correct = Uncorrect = 0;

        Total = words.Count;

        words.Randomize();
    }
}