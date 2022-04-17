
using Xamarin.Forms;

namespace OpenDictionary.Views.Controls.Helpers
{
    public static class BindableHelper
    {
        public static BindableProperty Create<TReturnType, TDeclaringType>(string name, BindableProperty.BindingPropertyChangedDelegate propertyChanged = null)
        {
            return BindableProperty.Create(name, typeof(TReturnType), typeof(TDeclaringType), default(TReturnType), propertyChanged: propertyChanged);
        }
        public static BindableProperty CreateString<TDeclaringType>(string name, BindableProperty.BindingPropertyChangedDelegate propertyChanged = null)
        {
            return Create<string, TDeclaringType>(name, propertyChanged);
        }
    }
}
