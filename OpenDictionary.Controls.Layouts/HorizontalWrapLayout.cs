using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace OpenDictionary.Controls.Layouts;

public class HorizontalWrapLayout : HorizontalStackLayout
{
    protected override ILayoutManager CreateLayoutManager()
    {
        return new HorizontalWrapLayoutManager(this);
    }
}
