using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrushSizeController : MonoBehaviour
{
    [SerializeField] private DrawingSurface drawingSurface;
    [SerializeField] private Slider brushSizeSlider;
    [SerializeField] private TextMeshProUGUI brushSizeText;

    private void Update()
    {
        UpdateBrushSize(brushSizeSlider.value);
    }

    public void OnBrushSizeChanged(float newSize)
    {
        UpdateBrushSize(newSize);
    }

    private void UpdateBrushSize(float newSize)
    {
        drawingSurface.SetBrushSize((int)newSize);
        brushSizeText.text = $"Brush Size: {newSize:F1}";
    }
}
