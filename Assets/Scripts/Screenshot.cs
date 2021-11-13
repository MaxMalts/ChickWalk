using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;



public class Screenshot : MonoBehaviour {

	new Camera camera;


	void Awake() {
		camera = GetComponent<Camera>();
		Assert.IsNotNull(camera, "No Camera component attached to GameObject.");
	}


	void Start() {
		string filename = string.Format("Assets/Screenshots/capture_{0}.png",
			DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff"));

		if (!Directory.Exists("Assets/Screenshots")) {
			Directory.CreateDirectory("Assets/Screenshots");
		}
		TakeTransparentScreenshot(Screen.width, Screen.height, filename);
	}


	void TakeTransparentScreenshot(int width, int height, string savePath) {
		// Depending on your render pipeline, this may not work.
		var bakCamTargetTexture = camera.targetTexture;
		var bakCamClearFlags = camera.clearFlags;
		var bakRenderTextureActive = RenderTexture.active;

		var texTransparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
		// Must use 24-bit depth buffer to be able to fill background.
		var renderTexture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
		var grabArea = new Rect(0, 0, width, height);

		RenderTexture.active = renderTexture;
		camera.targetTexture = renderTexture;
		camera.clearFlags = CameraClearFlags.SolidColor;

		// Simple: use a clear background
		camera.backgroundColor = Color.clear;
		camera.Render();
		texTransparent.ReadPixels(grabArea, 0, 0);
		texTransparent.Apply();

		// Encode the resulting output texture to a byte array then write to the file
		byte[] pngShot = ImageConversion.EncodeToPNG(texTransparent);
		File.WriteAllBytes(savePath, pngShot);

		camera.clearFlags = bakCamClearFlags;
		camera.targetTexture = bakCamTargetTexture;
		RenderTexture.active = bakRenderTextureActive;
		RenderTexture.ReleaseTemporary(renderTexture);
		Texture2D.Destroy(texTransparent);
	}

}