using UnityEngine;
using System.Collections;

public class radioactivity : MonoBehaviour {
	private float rdlevel;
	private float deltatime;
	private float percentage;

	private float W;
	private float H;

	public float difficulty;

	public void Reset () {
		rdlevel = 0f;
	}
	public float returnRD () {
		return rdlevel;
	}
	void Awake () {
		rdlevel = 0f;
		percentage = 0f;
		difficulty = .6f;
		W = Screen.width;
		H = Screen.height;
	}
	void Update () {
		deltatime = Time.deltaTime;
		rdlevel += difficulty * deltatime;
		percentage = rdlevel / 100;
	}
	void OnGUI(){
		GUI.BeginGroup (new Rect(W / 4, H /12, W / 2, H / 12));
		GUI.Box (new Rect (0f, 0f, W / 2 - 1, H / 12 ), " "); // minus one so that the edge border fits within the group
		GUI.BeginGroup (new Rect (3f, 3f, W / 2 - 3f, H / 12 - 3));
		GUI.Box (new Rect (0f, 0f, (W / 2 - 3) * percentage, H / 12 - 6), " ");
		GUI.EndGroup ();
		GUI.EndGroup ();
	}
}

