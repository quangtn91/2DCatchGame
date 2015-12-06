using UnityEngine;
using System.Collections;

public class HatController : MonoBehaviour {

	public Camera cam;

	private float maxWidth;
	private bool camControl;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		camControl = false;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float hatWidth = GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - hatWidth;
	}
	
	// Update is called once per frame
	void Update () {
		if (camControl) {
			Vector3 rawPosition = cam.ScreenToWorldPoint (Input.mousePosition);
			//Vector3 targetPosition = new Vector3 (rawPosition.x, -2.5f, 0.0f);
			float targetWidth = Mathf.Clamp (rawPosition.x, -maxWidth, maxWidth);
			Vector3 targetPosition = new Vector3 (targetWidth, -2.5f, 0.0f);
			GetComponent<Rigidbody2D>().MovePosition (targetPosition);
		}
	}

	public void ToggleCamControl (bool toggle){
		camControl = toggle;
	}
}
