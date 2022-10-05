namespace OpenDictionary.Games.WordConformities;

public interface IProperties
{
    string GroupName { get; set; }
    string Question { get; set; }

    int Total { get; set; }

    int Answered { get; set; }
    int Correct { get; set; }
    int Uncorrect { get; set; }

    void Clear();
}