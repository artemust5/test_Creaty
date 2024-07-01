using UnityEngine;
using System.IO;

public class TextureManager : MonoBehaviour
{
    [HideInInspector] public Texture2D savedTexture;
    [SerializeField] private int textureWidth = 512;
    [SerializeField] private int textureHeight = 512;
    [HideInInspector] public Texture2D drawingTexture;
    [HideInInspector] private string filename = "savedTexture.png";

    private void Start()
    {
        if (File.Exists("savedTexture.png"))
        {
            LoadSavedTexture();
        }
        else
        {
            CreateTexture();
        }
    }

    public void SaveTextureToFile()
    {
        if (drawingTexture != null)
        {
            byte[] bytes = drawingTexture.EncodeToPNG();
            File.WriteAllBytes(filename, bytes);
            Debug.Log($"Texture saved as {filename}");
        }
        else
        {
            Debug.LogWarning("No drawing texture available to save.");
        }
    }

    public void CreateTexture()
    {
        drawingTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        drawingTexture.filterMode = FilterMode.Bilinear;
        drawingTexture.wrapMode = TextureWrapMode.Clamp;
        Color fillColor = Color.white;
        Color[] fillColorArray = drawingTexture.GetPixels();
        for (int i = 0; i < fillColorArray.Length; i++)
        {
            fillColorArray[i] = fillColor;
        }
        drawingTexture.SetPixels(fillColorArray);
        drawingTexture.Apply();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = drawingTexture;
    }

    public void LoadSavedTexture()
    {

        byte[] bytes = File.ReadAllBytes("savedTexture.png");
        savedTexture = new Texture2D(2, 2);
        savedTexture.LoadImage(bytes);
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = savedTexture;
        drawingTexture = savedTexture;
    }


    private void OnApplicationQuit()
    {
        SaveTextureToFile();
    }
}
