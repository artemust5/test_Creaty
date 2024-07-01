using UnityEngine;

public class DrawingSurface : MonoBehaviour
{
    [HideInInspector] public Color brushColor = Color.red;
    [HideInInspector] private int brushSize = 10;
    [SerializeField] private TextureManager textureManager;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            DrawOnSphere();
    }

    private void DrawOnSphere()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("DrawingSurface"))
            {
                Vector2 sphereUV = GetSphereUV(hit.point);
                for (int i = -brushSize; i <= brushSize; i++)
                {
                    for (int j = -brushSize; j <= brushSize; j++)
                    {
                        int x = Mathf.Clamp((int)(sphereUV.x * textureManager.drawingTexture.width) + i, 0, textureManager.drawingTexture.width - 1);
                        int y = Mathf.Clamp((int)(sphereUV.y * textureManager.drawingTexture.height) + j, 0, textureManager.drawingTexture.height - 1);
                        textureManager.drawingTexture.SetPixel(x, y, brushColor);
                    }
                }

                textureManager.drawingTexture.Apply();
            }
            if (hit.collider.CompareTag("QuadDrawingSurface"))
            {
                Vector2 pixelUV = hit.textureCoord;
                pixelUV.x *= textureManager.drawingTexture.width;
                pixelUV.y *= textureManager.drawingTexture.height;

                for (int i = -brushSize; i <= brushSize; i++)
                {
                    for (int j = -brushSize; j <= brushSize; j++)
                    {
                        int x = Mathf.Clamp((int)pixelUV.x + i, 0, textureManager.drawingTexture.width - 1);
                        int y = Mathf.Clamp((int)pixelUV.y + j, 0, textureManager.drawingTexture.height - 1);
                        textureManager.drawingTexture.SetPixel(x, y, brushColor);
                    }
                }

                textureManager.drawingTexture.Apply();
            }
        }
    }
    private Vector2 GetSphereUV(Vector3 hitPoint)
    {
        float theta = Mathf.Atan2(hitPoint.z, hitPoint.x);
        float phi = Mathf.Acos(hitPoint.y / hitPoint.magnitude);
        float u = theta / (2 * Mathf.PI) + 0.5f;
        float v = 1.0f - (phi / Mathf.PI); 

        return new Vector2(u, v);
    }
    public void SetBrushSize(int newSize)
    {
        brushSize = newSize;
    }
}
