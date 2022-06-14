using System.Threading.Tasks;

using MvvmHelpers.Commands;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;

namespace OpenDictionary.ViewModels;

public sealed class WordGroupInfoList : WordGroupInfoListBase
{
    private readonly INavigationService navigation;

    public AsyncCommand RedirectToCreateCommand { get; }

    public WordGroupInfoList(IStorage<WordGroup> wordGroupStorage, INavigationService navigation)
        : base(wordGroupStorage)
    {
        this.navigation = navigation;

        RedirectToCreateCommand = new AsyncCommand(OnCreateItemReditected);
    }


    private Task OnCreateItemReditected()
    {
        return navigation.GoToAsync(AppRoutes.WordGroup.Create);
    }

    protected override Task OnTapped(WordGroupInfo item)
    {
        if (item == null) return Task.CompletedTask;

        return navigation.GoToAsync(AppRoutes.WordGroup.Detail, parameter: nameof(WordGroup.Id), value: item.Id.ToString());
    }
}