using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Maui;

using Windows.Storage;
using Windows.Storage.Pickers;

namespace OpenDictionary.Services.DataTransfer;

internal sealed class DataShareServiceWindows : IFileExportService
{
    public async Task AsSingleFile(string title, string path)
    {
        var extension = Path.GetExtension(path);

        var picker = CreateFilePicker(name: "Dictionaries", extension);

        if (IsMauiWindow(out var window))
        {
            WinRT.Interop.InitializeWithWindow.Initialize(picker, window.WindowHandle);
        }

        StorageFile? result = await picker.PickSaveFileAsync();

        if (result is null)
        {
            return;
        }

        using Stream file = await result.OpenStreamForWriteAsync();

        file.SetLength(0);

        using Stream cache = OpenFile(path);

        await cache.CopyToAsync(file);
    }
    public async Task AsMultipleFiles(string title, string[] paths)
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

        foreach (var path in paths)
        {
            using Stream cache = OpenFile(path);

            string name = Path.GetFileName(path);

            StorageFile storageFile = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);

            using Stream file = await storageFile.OpenStreamForWriteAsync();

            await cache.CopyToAsync(file);

            await file.FlushAsync();
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
