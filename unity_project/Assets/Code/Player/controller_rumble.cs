using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class controller_rumble : MonoBehaviour {

	private float rumble = 0f;
	public GameObject cam;
	private screen_shake ss;

	void Awake() {
		ss = cam.GetComponent<screen_shake>();
	}

	void Update() {
		if (rumble > 0.1f) {
			rumble *= 0.75f;
		}
		else{
			rumble = 0f;
		}

		GamePad.SetVibration(0, rumble, rumble);
	}
	
	void OnCollisionEnter2D (Collision2D c){
		rumble = Mathf.Clamp01(0.5f + c.relativeVelocity.magnitude/20f);
		ss.shaketime = Mathf.FloorToInt(c.relativeVelocity.magnitude);
	}

	void OnApplicationQuit() {
		GamePad.SetVibration(0, 0f, 0f);
	}
}
