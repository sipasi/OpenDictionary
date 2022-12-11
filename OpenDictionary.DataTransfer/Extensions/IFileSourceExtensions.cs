namespace OpenDictionary.DataTransfer.Extensions;

public static class IFileSourceExtensions
{
    public static bool IsNotExists(this IFileSource file) => File.Exists(file.FullPath) is false;
    public static void DeleteIfExists(this IFileSource file)
    {
        if (file.IsNotExists())
        {
            return;
        }

        File.Delete(file.FullPath);
    }
}