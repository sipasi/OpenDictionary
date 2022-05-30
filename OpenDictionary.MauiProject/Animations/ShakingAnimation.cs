
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

namespace OpenDictionary.Animations;

public class ShakingAnimation
{
    private readonly VisualElement element;

    private readonly int times = 5;
    private readonly int rotation = 10;
    private readonly uint length = 150;

    private bool isAnimatin;

    public ShakingAnimation(VisualElement element)
    {
        this.element = element;
    }

    public ShakingAnimation(VisualElement element, int times, int rotation, uint length) : this(element)
    {
        this.times = times;
        this.rotation = rotation;
        this.length = length;
    }

    public async ValueTask Shake()
    {
        if (element is null)
        {
            return;
        }

        if (isAnimatin is true)
        {
            return;
        }

        isAnimatin = true;


        int rotationToRight = rotation;
        int rotationToLeft = -rotation;

        for (int i = 0; i < times; i++)
        {
            await element.RotateTo(rotationToRight, length);
            await element.RotateTo(rotationToLeft, length);
        }

        await element.RotateTo(0, length);

        isAnimatin = false;
    }
}
