using CommunityToolkit.Maui.Views;

namespace OpenDictionary.Views.Popups;

public partial class DialogPopup : Popup
{
    public DialogPopup(string title, string message, string accept, string cancel)
    {
        InitializeComponent();

        this.title.Text = title;
        this.message.Text = message;
        this.accept.Text = accept;
        this.cancel.Text = cancel;
    }

    private void ClickedCancel(object sender, System.EventArgs e) => Close(Result.cancel);
    private void ClickedAccept(object sender, System.EventArgs e) => Close(Result.accept);


    new public class Result
    {
        internal static readonly Accept accept = new();
        internal static readonly Cancel cancel = new();

        private Result() { }

        public bool IsAccept() => this is Accept;
        public bool IsCancel() => this is Cancel;

        public sealed class Accept : Result { }
        public sealed class Cancel : Result { }
    }
}