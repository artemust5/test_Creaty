using UnityEngine;
using UnityEngine.UI;

public class BrushColorChanger : MonoBehaviour
{
    [SerializeField] private DrawingSurface drawingSurface;
    [SerializeField] private Color newBrushColor;

    public void ChangeBrushColor()
    {
        drawingSurface.brushColor = newBrushColor;
    }
}
