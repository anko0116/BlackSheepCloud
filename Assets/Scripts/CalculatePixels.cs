using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatePixels : MonoBehaviour
{
    private RawImage fog;
    private Texture2D texture2d;
    private int pixelCount;
    // Start is called before the first frame update
    void Start()
    {
        fog = GetComponent<RawImage>();
        Texture texRef = fog.mainTexture;
        texture2d = TextureToTexture2D(fog.mainTexture);
        pixelCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate() {
        texture2d = TextureToTexture2D(fog.mainTexture);
        Color[] pixels = texture2d.GetPixels();
        int currPixels = 0;
        for (int i = 0; i < pixels.Length; ++i) {
            if (pixels[i].r > 0) {
                currPixels += 1;
            }
        }
        pixelCount = currPixels;
        //print("Uncovered " + pixelCount + " pixels.");
    }

    private Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture2D;
    }
}
