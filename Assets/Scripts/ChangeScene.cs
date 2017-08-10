using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
	public void ChangingScene (int SceneNum){
		SceneManager.LoadScene(SceneNum);
	}

}
