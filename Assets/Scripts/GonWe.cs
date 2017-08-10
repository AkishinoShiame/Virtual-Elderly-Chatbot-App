using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonWe : MonoBehaviour {
	public static bool gonwe_bool = false;
	private static string speak_e_gao = "";

	// Use this for initialization
	void Start () {
		EasyTTSUtil.Initialize (EasyTTSUtil.Taiwan);
	}
	
	// Update is called once per frame
	void Update () {
		if (gonwe_bool) {
			EasyTTSUtil.SpeechAdd (speak_e_gao, 1f, 0.5f, 1.0f);
			gonwe_bool = false;
		}
	}
	void OnApplicationQuit(){
		EasyTTSUtil.Stop ();
	}

	public static void Change_egao(string fuck){
		speak_e_gao = fuck;
	}
}
