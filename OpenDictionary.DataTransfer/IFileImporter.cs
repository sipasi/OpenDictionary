namespace OpenDictionary.DataTransfer;

public interface IFileImporter
{
    ValueTask<IReadOnlyList<IFileSource>> Import();
}