using CommunityToolkit.Mvvm.Input;

using OpenDictionary.DataTransfer.Extensions;
using OpenDictionary.DataTransfer.Services;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels;

namespace OpenDictionary.DataTransfer.ViewModels;

public abstract partial class ExportViewModel<TView, TExport> : BindableObject where TView : class
{
    private readonly IFileExporter exporter;
    private readonly IFileExportService exportService;
    private readonly IToastMessageService toast;
    private readonly INavigationService navigation;

    public bool CanMultipleExport => Collection.SelectedItems.Count > 1;
    public bool AtLeastOneSelected => Collection.SelectedItems.Count > 0;

    public SelectableCollectionViewModel<TView> Collection { get; }

    public ExportViewModel(IFileExportService exportService, IFileExporter exporter, INavigationService navigation, IToastMessageService toast)
    {
        this.exporter = exporter;
        this.exportService = exportService;
        this.toast = toast;
        this.navigation = navigation;

        Collection = new()
        {
            SelectionsChanged = () =>
            {
                OnPropertyChanged(nameof(CanMultipleExport));
                OnPropertyChanged(nameof(AtLeastOneSelected));
            }
        };
    }

    protected abstract ValueTask<TView[]> OnLoad();
    protected abstract ValueTask<FileData<TExport>[]> PrepareExport();

    [RelayCommand]
    private async Task Load()
    {
        var entities = await OnLoad();

        Collection.Clear();

        Collection.AddRange(entities);
    }

    [RelayCommand]
    private Task ExportAsSingle() => TryExport(datas => exportService.AsSingleFile(exporter, datas));

    [RelayCommand]
    private Task ExportAsMultiple() => TryExport(datas => exportService.AsMultipleFiles(exporter, datas));

    private async Task TryExport(Func<FileData<TExport>[], ValueTask> func)
    {
        FileData<TExport>[] datas = await PrepareExport();

        if (datas.Length == 0)
        {
            await toast.Show(message: "Select at least one dictionary");

            return;
        }

        await func.Invoke(datas);

        await toast.ShowSuccess($"Exported: {datas.Length}");

        await navigation.GoBackAsync();
    }
}