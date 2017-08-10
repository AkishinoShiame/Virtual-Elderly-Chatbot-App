using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechSound : MonoBehaviour {

	public AudioClip boy1;
	public AudioClip boy2;
	public AudioClip boy3;
	public AudioClip boy4;
	public AudioClip boy5;
	public AudioClip boy6;
	public AudioClip boy7;
	public AudioClip boy8;
	public AudioClip boy9;
	public AudioClip boy10;
	public AudioClip boy11;
	public AudioClip boy12;
	public AudioClip girl1;
	public AudioClip girl2;
	public AudioClip girl3;
	public AudioClip girl4;
	public AudioClip girl5;
	public AudioClip girl6;
	public AudioClip girl7;
	public AudioClip girl8;
	public AudioClip girl9;
	public AudioClip girl10;
	public AudioClip girl11;
	public AudioClip girl12;
	AudioSource sound_speech;
	public static int playtrack = 0;

	public static bool state = false;

	bool sexual;

	public void play_talking_in_loop(){
		switch (LoadAndChangeModel.index) {
		case 0:
			sexual = false;
			break;
		case 1:
			sexual = false;
			break;
		case 2:
			sexual = false;
			break;
		case 3:
			sexual = false;
			break;
		case 4:
			sexual = true;
			break;
		case 5:
			sexual = true;
			break;
		case 6:
			sexual = true;
			break;
		case 7:
			sexual = true;
			break;
		case 8:
			sexual = true;
			break;
		}

		//sound_speech.loop = false;
		//print ("will play song");
		if (sexual) {
			//true is girl
			//print("test in sexual"); // test
			//print(playtrack);
			switch(playtrack){
			case 0:
				sound_speech.clip = girl1;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 1:
				sound_speech.clip = girl2;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 2:
				sound_speech.clip = girl3;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 3:
				sound_speech.clip = girl4;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 4:
				sound_speech.clip = girl5;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 5:
				sound_speech.clip = girl6;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 6:
				sound_speech.clip = girl7;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 7:
				sound_speech.clip = girl8;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 8:
				sound_speech.clip = girl9;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 9:
				sound_speech.clip = girl10;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 10:
				sound_speech.clip = girl11;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 11:
				sound_speech.clip = girl12;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			}
		} else {
			//false is boy
			switch(playtrack){
			case 0:
				sound_speech.clip = boy1;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 1:
				sound_speech.clip = boy2;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 2:
				sound_speech.clip = boy3;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 3:
				sound_speech.clip = boy4;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 4:
				sound_speech.clip = boy5;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 5:
				sound_speech.clip = boy6;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 6:
				sound_speech.clip = boy7;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 7:
				sound_speech.clip = boy8;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 8:
				sound_speech.clip = boy9;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 9:
				sound_speech.clip = boy10;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 10:
				sound_speech.clip = boy11;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			case 11:
				sound_speech.clip = boy12;
				sound_speech.volume = (float)1;
				sound_speech.Play();
				break;
			}
		}
		//playtrack = playtrack + 1;
		/*if(playtrack == 5)
			playtrack = 0;*/
	}

	// Use this for initialization
	void Start () {
		sound_speech = GetComponent<AudioSource>();
		play_talking_in_loop ();
	}
	
	// Update is called once per frame
	void Update () {
		if (state) {
			play_talking_in_loop ();
			state = false;
		}
	}
}
