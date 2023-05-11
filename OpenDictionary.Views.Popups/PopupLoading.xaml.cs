using System;
using System.Threading.Tasks;

using CommunityToolkit.Maui.Views;

using Microsoft.Maui.Controls;

namespace OpenDictionary.Views.Popups;

public partial class PopupLoading : Popup
{
    private readonly Func<Task> task;

    public PopupLoading(string title, string message, Func<Task> task)
    {
        InitializeComponent();

        SetLabel(this.title, title);
        SetLabel(this.message, message);

        this.task = task;

        Opened += PopupLoading_Opened;
    }

    private async void PopupLoading_Opened(object? sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
    {
        Opened -= PopupLoading_Opened;

        await task.Invoke();

        Close();
    }

    private void SetLabel(Label label, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            label.IsVisible = false;

            return;
        }

        label.Text = value;
    }
}