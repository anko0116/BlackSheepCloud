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
    private PlayerMovement playerMoveScript;
    private Rigidbody2D playerBody;

    public float multiplier = 25f;
    public float constant = 0f;

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

        GameObject player = GameObject.Find("player2"); 
        playerTransf = player.GetComponent<Transform>();
        playerMoveScript = player.GetComponent<PlayerMovement>();
        playerBody = player.GetComponent<Rigidbody2D>();
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
        pixelText.text = pixelCount.ToString() + " Pixels";
        UnityEngine.Object.Destroy(texture2d);

        // Countdown time
        time -= Time.deltaTime;
        timerText.text = time.ToString("0") + " Seconds";
        // Reset timer when it hits 0
        if (time <= -0.49f) {
            time = prevTime + (((pixelCount - prevPixelCount) / multiplier) - constant);
            prevTime = time;
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
        playerMoveScript.enabled = false;
        playerBody.velocity = Vector3.zero;
        yield return new WaitForSeconds(1f);
        playerMoveScript.enabled = true;
    }
}