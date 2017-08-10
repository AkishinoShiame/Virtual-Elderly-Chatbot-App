using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamBG : MonoBehaviour {

	public RawImage imgCam;
	public AspectRatioFitter fit;
	private WebCamTexture CameraTexture;

	void Start () { 
		//GUITexture BackgroundTexture = gameObject.AddComponent<GUITexture>();
		//BackgroundTexture.pixelInset = new Rect(0,0,Screen.width,Screen.height);
		WebCamDevice[] devices = WebCamTexture.devices;
		string backCamName = devices[0].name;
		CameraTexture = new WebCamTexture(backCamName,Screen.width,Screen.height,30);
		CameraTexture.Play();
		//BackgroundTexture.texture = CameraTexture;
		imgCam.texture = CameraTexture;


	}

	void Update(){
		/*
		float ratio;
		if (CameraTexture != null) {
			ratio = (float)CameraTexture.width / (float)CameraTexture.height;
			fit.aspectRatio = ratio;
		}
		float scaleY = CameraTexture.videoVerticallyMirrored ? -1 : 1f;
		imgCam.rectTransform.localScale = new Vector3 (1f, scaleY, 1f);
		*/

		int orient = -CameraTexture.videoRotationAngle;
		imgCam.rectTransform.localEulerAngles = new Vector3 (0, 0, orient);
	}

	void OnApplicationQuit()
	{
		CameraTexture.Stop ();
	}
}
