using System.Diagnostics.CodeAnalysis;

using OpenDictionary.DataTransfer;
using OpenDictionary.DataTransfer.Services;

using Windows.Storage;
using Windows.Storage.Pickers;

namespace OpenDictionary.Services.DataTransfer;

internal sealed class FileExportServiceWindows : IFileExportService
{
    public async ValueTask AsSingleFile(IFileSource file)
    {
        var extension = Path.GetExtension(file.FullPath);

        var picker = CreateFilePicker(file.Name, extension);

        if (IsMauiWindow(out var window))
        {
            WinRT.Interop.InitializeWithWindow.Initialize(picker, window.WindowHandle);
        }

        StorageFile? result = await picker.PickSaveFileAsync();

        if (result is null)
        {
            return;
        }

        using Stream stream = await result.OpenStreamForWriteAsync();

        stream.SetLength(0);

        using Stream cache = OpenFile(file.FullPath);

        await cache.CopyToAsync(stream);
    }
    public async ValueTask AsMultipleFiles(IReadOnlyList<IFileSource> files)
    {
        var picker = CreateFolderPicker();

        if (IsMauiWindow(out var window))
        {
            WinRT.Interop.InitializeWithWindow.Initialize(picker, window.WindowHandle);
        }

        StorageFolder? folder = await picker.PickSingleFolderAsync();

        if (folder is null)
        {
            return;
        }

        foreach (var file in files)
        {
            using Stream cache = OpenFile(file.FullPath);

            string name = Path.GetFileName(file.Name);

            StorageFile storageFile = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);

            using Stream stream = await storageFile.OpenStreamForWriteAsync();

            await cache.CopyToAsync(stream);

            await stream.FlushAsync();
        }
    }

    private bool IsMauiWindow([NotNullWhen(returnValue: true)] out MauiWinUIWindow? window)
    {
        window = MauiWinUIApplication.Current.Application.Windows[0].Handler?.PlatformView as MauiWinUIWindow;

        return window is not null;
    }

    private static FileSavePicker CreateFilePicker(string name, string extension)
    {
        var picker = new FileSavePicker
        {
            SuggestedFileName = name,
        };

        picker.FileTypeChoices.Add(extension, new List<string> { extension });

        return picker;
    }
    private static FolderPicker CreateFolderPicker()
    {
        var picker = new FolderPicker
        {
            CommitButtonText = "Save",
            SuggestedStartLocation = PickerLocationId.Desktop,
            ViewMode = PickerViewMode.List,
        };

        picker.FileTypeFilter.Add("*");

        return picker;
    }

    private static Stream OpenFile(string path) => new FileStream(path, FileMode.Open, FileAccess.Read);
}
