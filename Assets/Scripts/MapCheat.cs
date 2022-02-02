using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Attached to MainCamera
public class MapCheat : MonoBehaviour
{
    private float timer;
    private static bool setPixels = false;
    private static Texture prevTexture;
    private static Texture2D prevTexture2d;
    private static Color[] pixels;
    private static RawImage prevFogImg;

    private static RenderTexture rendertexture;

    void Update()
    {
        if (setPixels) {
            // Works
            // RawImage fog = GameObject.Find("Fog").GetComponent<RawImage>();
            // fog.texture = prevTexture2d;

            Graphics.Blit(prevTexture2d, rendertexture);

            // Texture2D texture2d = TextureToTexture2D(fog.texture);
            // texture2d.SetPixels(pixels);
            // texture2d.Apply();
            print("hi");
            setPixels = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Alpha1)) {
            if (Time.time - timer > 1f) {
                setPixels = true;
                SceneManager.LoadScene(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Alpha2)) {
            if (Time.time - timer > 1f) {
                prevFogImg = GameObject.Find("Fog").GetComponent<RawImage>();
                prevTexture = prevFogImg.mainTexture;
                prevTexture2d = TextureToTexture2D(prevTexture);

                rendertexture = Resources.Load<RenderTexture>("RenderTextures/Fog");

                SceneManager.LoadScene(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Alpha3)) {
            if (Time.time - timer > 1f) {
                SceneManager.LoadScene(2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Alpha4)) {
            if (Time.time - timer > 1f) {
                SceneManager.LoadScene(3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Alpha5)) {
            if (Time.time - timer > 1f) {
                SceneManager.LoadScene(4);
            }
        }
    }


    private Texture2D TextureToTexture2D(Texture texture)
    {
        // Convert RenderTexture to Texture2D
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
