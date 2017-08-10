using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadAndChangeModel : MonoBehaviour {
	public GameObject[] model = new GameObject[9]; 
	public Transform parent;
	public static GameObject Now;
	static bool CanChangeCharacter = false;
	//below is for model change use
	public static int index = 4; // for change model use
	Vector3 OrigPosit;
	Vector3 DeltaPosit;
	// above is for model change use

	// Use this for initialization
	void Start () {
		Scene scene = SceneManager.GetActiveScene();
		if (Now == null)
			Now = Instantiate(model[index],parent);
		if (scene.buildIndex == 1) {
			CanChangeCharacter = true;
		} else {
			CanChangeCharacter = false;
		}
		Animator CurrentAni = Now.GetComponent<Animator> ();
		if (CanChangeCharacter) {
			if (CurrentAni.GetLayerWeight (1) == 0.0f) {
				for (int i = 1; i < 7; i++) {
					CurrentAni.SetLayerWeight (i, 0.0f);
				}
				CurrentAni.SetLayerWeight (1, 1.0f);
			}
		} else {
			for (int i = 1; i < 7; i++) {
				CurrentAni.SetLayerWeight (i, 0.0f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Animator CurrentAni = Now.GetComponent<Animator> ();
		if (CanChangeCharacter) {
			int tmpindex = index;
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				// Get movement of the finger since last frame
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;

				DeltaPosit = Input.mousePosition - OrigPosit;

				if (touchDeltaPosition.x > 450) {
					//right
					if (DeltaPosit [0] > 0) {
						if ((index + 1) == 9)
							index = 0;
						else
							index += 1;
						//Debug.Log ("Right");
					}
				}
				if (touchDeltaPosition.x < -450) {
					//left
					if (DeltaPosit [0] < 0) {
						if ((index - 1) == -1)
							index = 8;
						else
							index -= 1;
						//Debug.Log ("Left");
					}
				}
			}
			if (Input.GetMouseButtonDown (0)) {
				OrigPosit = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp (0)) {
				DeltaPosit = Input.mousePosition - OrigPosit;
				if (DeltaPosit [0] > 0) {
					if ((index + 1) == 9)
						index = 0;
					else
						index += 1;
					//Debug.Log ("Right");
				} else if (DeltaPosit [0] < 0) {
					if ((index - 1) == -1)
						index = 8;
					else
						index -= 1;
					//Debug.Log ("Left");
				} else {
				}
			}
			if (tmpindex != index) {
				Destroy (Now);
				Now = Instantiate (model [index], parent);
			}
			//Debug.Log (CurrentAni.GetLayerWeight (1));
			if (CurrentAni.GetLayerWeight (1) == 0.0f) {
				for (int i = 1; i < 7; i++) {
					CurrentAni.SetLayerWeight (i, 0.0f);
				}
				CurrentAni.SetLayerWeight (1, 1.0f);
			}
		}
	}
}
