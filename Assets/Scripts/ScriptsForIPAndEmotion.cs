using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptsForIPAndEmotion : MonoBehaviour {

	public InputField ip_notag;
	public InputField ip_withtag;
	public Text UserInputIPnotag;
	public Text UserInputIPwithtag;
	public InputField Question_text;
	public Text UserInputQuestion;

	public void change_ip(){
		if (UserInputIPnotag.text != null && UserInputIPwithtag.text != null) {
			if (!UserInputIPnotag.text.StartsWith ("Http://") && !UserInputIPwithtag.text.StartsWith ("Http://")) {
				UserInputIPnotag.text = "Http://" + UserInputIPnotag.text;
				UserInputIPwithtag.text = "Http://" + UserInputIPwithtag.text;
				RecordAndUpload.ChangeIP_Address (UserInputIPnotag.text, UserInputIPwithtag.text);
			}
			else if(!UserInputIPnotag.text.StartsWith ("Http://") && UserInputIPwithtag.text.StartsWith ("Http://")){
				UserInputIPnotag.text = "Http://" + UserInputIPnotag.text;
				RecordAndUpload.ChangeIP_Address (UserInputIPnotag.text, UserInputIPwithtag.text);
			}
			else if (UserInputIPnotag.text.StartsWith ("Http://") && !UserInputIPwithtag.text.StartsWith ("Http://")) {
				UserInputIPwithtag.text = "Http://" + UserInputIPwithtag.text;
				RecordAndUpload.ChangeIP_Address (UserInputIPnotag.text, UserInputIPwithtag.text);
			}
			else
				RecordAndUpload.ChangeIP_Address (UserInputIPnotag.text, UserInputIPwithtag.text);
		}
	}

	public Text Emo_Pos;
	public Text Emo_Net;
	public Text Emo_Neg;

	public void Show_Emo(){
		Emo_Pos.text = RecordAndUpload.speechPositive.ToString() + "%";
		Emo_Net.text = RecordAndUpload.speechNatural.ToString () + "%";
		Emo_Neg.text = RecordAndUpload.speechNegative.ToString () + "%";
		print (RecordAndUpload.speechPositive);
		print (RecordAndUpload.speechNatural);
		print (RecordAndUpload.speechNegative);
	}

	public void change_question(){
		if (UserInputQuestion.text != null) {
			if (int.Parse (UserInputQuestion.text) >= 0 && int.Parse (UserInputQuestion.text) <= 11) {
				if (SpeechSound.playtrack != int.Parse (UserInputQuestion.text))
					SpeechSound.playtrack = int.Parse (UserInputQuestion.text);
			} else {
				SpeechSound.playtrack = 0;
			}
		}
	}

	public void start(){
		ip_notag.text = RecordAndUpload.GetNoTagIP_Address();
		ip_withtag.text = RecordAndUpload.GetWithTagIP_Address();
		Question_text.text = SpeechSound.playtrack.ToString();
	}
}
