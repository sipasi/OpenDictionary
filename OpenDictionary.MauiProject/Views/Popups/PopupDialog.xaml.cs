using CommunityToolkit.Maui.Views;

namespace OpenDictionary.Views.Popups;

public partial class PopupDialog : Popup
{
    public PopupDialog(string title, string message, string accept, string cancel)
    {
        InitializeComponent();

        this.title.Text = title;
        this.message.Text = message;
        this.accept.Text = accept;
        this.cancel.Text = cancel;
    }

    private void ClickedCancel(object sender, System.EventArgs e)
    {
        Close(Result.Cancel);
    }
    private void ClickedAccept(object sender, System.EventArgs e)
    {
        Close(Result.Ok);
    }

    new public sealed class Result
    {
        internal static Result Ok { get; } = new();
        internal static Result Cancel { get; } = new();

        public bool IsOk() => this == Ok;
        public bool IsCancel() => this == Cancel;
    }
}