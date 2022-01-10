using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatePixels : MonoBehaviour
{
    private RawImage fog;
    private Texture2D texture2d;

    private int prevPixelCount;
    private int pixelCount;
    private float prevTime;
    private float time;

    private Text pixelText;
    private Text timerText;

    private Transform playerTransf;
    private PlayerMovement playerBody;

    void Start()
    {
        fog = GetComponent<RawImage>();
        Texture texRef = fog.mainTexture;
        texture2d = TextureToTexture2D(fog.mainTexture);

        prevPixelCount = 0;
        pixelCount = 0;
        prevTime = 5.0f;
        time = prevTime;

        pixelText = GameObject.Find("PixelText").GetComponent<Text>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();

        playerTransf = GameObject.Find("player2").GetComponent<Transform>();
        playerBody = GameObject.Find("player2").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate() {
        // Count uncovered pixels in the map
        texture2d = TextureToTexture2D(fog.mainTexture);
        Color[] pixels = texture2d.GetPixels();
        int currPixels = 0;
        for (int i = 0; i < pixels.Length; ++i) {
            if (pixels[i].r > 0) {
                currPixels += 1;
            }
        }
        pixelCount = currPixels;
        pixelText.text = pixelCount.ToString();

        // Countdown time
        time -= Time.deltaTime;
        timerText.text = time.ToString("0");
        // Reset timer when it hits 0
        if (time <= 0f) {
            time = prevTime + (((pixelCount - prevPixelCount) / 100f) - 0f);
            prevPixelCount = pixelCount;
            // Reset character to origin of the map
            playerTransf.position = new Vector3(-1.0f, 3.35f, 0f);
            StartCoroutine(TimeDelay());
        }
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

    IEnumerator TimeDelay() {
        playerBody.enabled = false;
        yield return new WaitForSeconds(1f);
        playerBody.enabled = true;
    }
}

// 1. Map zoom out (gets cut off at the bottom)
// 2. Fog not centered on character (gets shifted)
// 3. blaccck background
// 4. timer (time in seconds = m * pixels + constant)