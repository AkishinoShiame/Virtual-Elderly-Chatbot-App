using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RecordAndUpload : MonoBehaviour {

	public Image mic_img;
	bool mic_rec = false;
	AudioClip myAudioClip; //audio wav
	//public static int playtrack = 0;

	Animator CurrentAni;
	//below for the audio emotion
	public static float speechPositive;
	public static float speechNegative;
	public static float speechNatural;
	//above for the speech emotion
	public Text speech_text;
	private static string ip_addr_notag = "http://120.125.83.120:6060";
	private static string ip_addr_withtag = "http://120.125.83.120:4040";

	private string mixedtext;
	private string jsonString;
	private string speaking = "";
	bool finish_upload = false;
	bool finish_emotion = false;
	int speechcount = 0;
	//bool gospeaking = false;

	//for sound play use
	/*
	public AudioClip boy1;
	public AudioClip boy2;
	public AudioClip boy3;
	public AudioClip boy4;
	public AudioClip boy5;
	public AudioClip girl1;
	public AudioClip girl2;
	public AudioClip girl3;
	public AudioClip girl4;
	public AudioClip girl5;
	AudioSource sound_speech;
	*/
	//for sound plau use

	public void PrintTheGameObj(){
		CurrentAni = LoadAndChangeModel.Now.GetComponent<Animator> ();
		if (mic_rec)
			mic_rec = false;
		else
			mic_rec = true;
		if (mic_rec) {
			mic_img.GetComponent<Image> ().color = Color.red;
			myAudioClip = Microphone.Start (null, false, 5, 44100);
		} else {
			if (Application.internetReachability != NetworkReachability.NotReachable)
				Debug.Log ("Have internet");
			Network.connectionTesterIP = "120.125.83.120";
			Network.connectionTesterPort = 80;
			Network.TestConnection ();
			Network.TestConnectionNAT ();
			mic_img.GetComponent<Image> ().color = Color.white;
			//Microphone.End (null);
			SavWav.Save("Clip_to_Upload", myAudioClip);
			//Upload ();
			if (speechcount==0)
				StartCoroutine (Upload_withtag());
			else
				StartCoroutine (Upload_notag());
		}
		//StartCoroutine (Upload());
		//Debug.Log (LoadAndChangeModel.Now); << call variables from different script
	}
	// http://120.125.83.120:6060
	// "file", "Clip_to_Upload.wav"
	IEnumerator Upload_notag() {
		//var filepath = Path.Combine(Application.dataPath, "Clip_to_Upload.wav");
		var filepath = Path.Combine(Application.persistentDataPath, "Clip_to_Upload.wav");
		byte[] bytes = File.ReadAllBytes (filepath);

		WWWForm form = new WWWForm ();
		form.AddBinaryData ("file", bytes , "Clip_to_Upload.wav", "audio/x-wav");
		WWW www = new WWW(ip_addr_notag,form);

		yield return www;
		mixedtext = www.text;
		string[] tmp = mixedtext.Split('|');
		jsonString = tmp[0] ;
		speaking = tmp[1] ;
		Debug.Log ("finished Upload");
		finish_upload = true;

		/*
		WWWForm form = new WWWForm();
		var filepath = Path.Combine(Application.dataPath, "Clip_to_Upload.wav");
		byte[] bytes = File.ReadAllBytes (filepath);
		form.AddBinaryData ("file", bytes , "Clip_to_Upload.wav", "audio/x-wav");

		UnityWebRequest www = UnityWebRequest.Post("http://120.125.83.120:5050", form);
		yield return www.Send();


		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			//www.downloadHandler
			//Debug.Log("Code: "+ www.responseCode + " Form upload complete! " + www.);
			//Debug.Log("Form upload complete!");
		}
		*/
	}


	// http://120.125.83.120:8080
	// "file", "Clip_to_Upload.wav"
	IEnumerator Upload_withtag() {
		//var filepath = Path.Combine(Application.dataPath, "Clip_to_Upload.wav");
		var filepath = Path.Combine(Application.persistentDataPath, "Clip_to_Upload.wav");
		byte[] bytes = File.ReadAllBytes (filepath);

		WWWForm form = new WWWForm ();
		form.AddBinaryData ("file", bytes , "Clip_to_Upload.wav", "audio/x-wav");
		WWW www = new WWW(ip_addr_withtag,form);

		yield return www;
		mixedtext = www.text;
		string[] tmp = mixedtext.Split('|');
		jsonString = tmp[0] ;
		speaking = tmp[1] ;
		Debug.Log ("finished Upload");
		finish_upload = true;

		/*
		WWWForm form = new WWWForm();
		var filepath = Path.Combine(Application.dataPath, "Clip_to_Upload.wav");
		byte[] bytes = File.ReadAllBytes (filepath);
		form.AddBinaryData ("file", bytes , "Clip_to_Upload.wav", "audio/x-wav");

		UnityWebRequest www = UnityWebRequest.Post("http://120.125.83.120:5050", form);
		yield return www.Send();


		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			//www.downloadHandler
			//Debug.Log("Code: "+ www.responseCode + " Form upload complete! " + www.);
			//Debug.Log("Form upload complete!");
		}
		*/
	}


	void try_to_print_json(){
		//print (jsonString);
		jsonString = jsonString.Replace ("\"predictions\":" , "");
		jsonString = jsonString.Replace ("[" , "");
		jsonString = jsonString.Replace (" " , "");
		jsonString = jsonString.Replace ("\n" , "");
		jsonString = jsonString.Replace ("]" , "");
		jsonString = jsonString.Replace ("\"," , "\":");
		predict prediction = JsonUtility.FromJson<predict> (jsonString);
		speechPositive = prediction.Positive;
		speechNatural = prediction.Natural;
		speechNegative = prediction.Negative;
		//print ("finished assign!");
		finish_emotion = true;
		//print ("Positive : " + prediction.Positive);
		//print ("Natural : " + prediction.Natural);
		//print ("Negative : " + prediction.Negative);
		//Prediction prediction = JsonUtility.FromJson<Prediction> (jsonString);
		//print (prediction);
		jsonString = null;
		finish_upload = false;
	}

	[System.Serializable]
	public class predict{
		public float Positive;
		public float Natural;
		public float Negative;
	}
		
	void emotion_react(){
		int feeling = 0;
		float max = Mathf.Max (speechPositive, speechNatural, speechNegative);
		if (Mathf.Approximately (max, speechPositive))
			feeling = 1;
		if (Mathf.Approximately (max, speechNatural))
			feeling = 2;
		if (Mathf.Approximately (max, speechNegative))
			feeling = 3;
		//print (max + " Nat " + speechNatural + " Pos " + speechPositive + " Neg " + speechNegative);
		//print (feeling);

		int rand = UnityEngine.Random.Range (0, 2);
		switch (feeling) {
		case 1:
			if (rand == 0) {
				for (int i = 1; i < 7; i++) {
					CurrentAni.SetLayerWeight (i, 0.0f);
				}
				CurrentAni.SetLayerWeight (2, 1.0f);
			} else {
				for (int i = 1; i < 7; i++) {
					CurrentAni.SetLayerWeight (i, 0.0f);
				}
				CurrentAni.SetLayerWeight (4, 1.0f);
			}
			StartCoroutine(wait ());
			feeling = 0;
			break;
		case 2:
			for (int i = 1; i < 7; i++) {
				CurrentAni.SetLayerWeight (i, 0.0f);
			}
			CurrentAni.SetLayerWeight (3, 1.0f);
			StartCoroutine(wait ());
			feeling = 0;
			break;
		case 3:
			if (rand == 0) {
				for (int i = 1; i < 7; i++) {
					CurrentAni.SetLayerWeight (i, 0.0f);
				}
				CurrentAni.SetLayerWeight (5, 1.0f);
			} else {
				for (int i = 1; i < 7; i++) {
					CurrentAni.SetLayerWeight (i, 0.0f);
				}
				CurrentAni.SetLayerWeight (6, 1.0f);
			}
			StartCoroutine(wait ());
			feeling = 0;
			break;
		default:
			feeling = 0;
			break;
		}
		/*

			if (CurrentAni.GetLayerWeight (1) == 0.0f) {
				for (int i = 1; i < 7; i++) {
					CurrentAni.SetLayerWeight (i, 0.0f);
				}
				CurrentAni.SetLayerWeight (1, 1.0f);
			}
		*/
	}

	IEnumerator wait() {
		//print(Time.time);
		yield return new WaitForSeconds(5);
		//print(Time.time);
		for (int i = 1; i < 7; i++) {
			CurrentAni.SetLayerWeight (i, 0.0f);
		}
	}

	void start() {
		//play_talking_in_loop ();
		//EasyTTSUtil.Initialize (EasyTTSUtil.Taiwan);
		SpeechSound.state = true;
	}
	/*
	void OnGUI (){
		if (gospeaking) {
			speaking = speaking.Replace (" ", "");
			if(speaking == "次")
				speaking = "讓我們換個話題吧！";
			EasyTTSUtil.SpeechAdd (speaking);
			gospeaking = false;
		}
	}

	public void gonwea(){
		print (speaking);
		if(speaking == "次")
			speaking = "讓我們換個話題吧！";
		speech_text.text = speaking;
		//speaking = "讓我們換個話題吧";
		EasyTTSUtil.SpeechAdd (speaking);

	}*/
	void Update () {
		if (finish_upload) {
			//gonwea ();
			if(speaking == "次")
				speaking = "讓我們換個話題吧！";
			speech_text.text = speaking;
			GonWe.Change_egao(speech_text.text);
			GonWe.gonwe_bool = true;
			try_to_print_json ();
			//File.Delete (Path.Combine (Application.dataPath, "Clip_to_Upload.wav"));
			File.Delete (Path.Combine (Application.persistentDataPath, "Clip_to_Upload.wav"));
		}
		if (finish_emotion) {
			emotion_react ();
			//gospeaking = true;
			speechcount += 1;
			finish_emotion = false;
			//play_talking_in_loop ();
		}
		
	}
	/*
	void OnApplicationQuit(){
		EasyTTSUtil.Stop ();
	}
	*/
	public static void ChangeIP_Address(string IPAddress_notag, string IPAddress_withtag){
		if (ip_addr_notag != IPAddress_notag)
			ip_addr_notag = IPAddress_notag;
		if (ip_addr_withtag != IPAddress_withtag)
			ip_addr_withtag = IPAddress_withtag;
	}

	public static string GetNoTagIP_Address(){
		return ip_addr_notag;
	}

	public static string GetWithTagIP_Address(){
		return ip_addr_withtag;
	}
}
