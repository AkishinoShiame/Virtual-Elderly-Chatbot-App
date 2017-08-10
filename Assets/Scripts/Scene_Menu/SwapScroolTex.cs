using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapScroolTex : MonoBehaviour {

	public float speed = 0.1F;
	public Vector3 OrigPosit;
	public Vector3 DeltaPosit;
	public GameObject TheObject;
	public int index = 5;
	public Sprite[] pictures = new Sprite[9];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

			DeltaPosit = Input.mousePosition - OrigPosit;

			if(touchDeltaPosition.x >400) {
				//right
				if (DeltaPosit [0] > 0) {
					if ((index + 1) == 10)
						index = 1;
					else
						index += 1;
					//Debug.Log ("Right");
				}
			}
			if(touchDeltaPosition.x <-400) {
				//left
				if (DeltaPosit [0] < 0) {
					if ((index - 1) == 0)
						index = 9;
					else
						index -= 1;
					//Debug.Log ("Left");
				}
			}
		}
		if (Input.GetMouseButtonDown(0)) {
			OrigPosit = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0)) {
			DeltaPosit = Input.mousePosition - OrigPosit;
			if (DeltaPosit [0] > 0) {
				if ((index + 1) == 10)
					index = 1;
				else
					index += 1;
				//Debug.Log ("Right");
			} else if (DeltaPosit [0] < 0) {
				if ((index - 1) == 0)
					index = 9;
				else
					index -= 1;
				//Debug.Log ("Left");
			} else {
			}
		}
		TheObject.GetComponent<Image>().sprite = pictures[index-1];
	}
}
