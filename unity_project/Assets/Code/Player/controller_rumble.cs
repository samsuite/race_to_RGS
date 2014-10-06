using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class controller_rumble : MonoBehaviour {
	
	void OnCollisionEnter (Collision c){
		GamePad.SetVibration(0, 1f, 1f);
		print ("ouch!");
	}

	void OnApplicationQuit() {
		GamePad.SetVibration(0, 0f, 0f);
	}
}
