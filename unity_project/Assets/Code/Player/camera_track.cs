using UnityEngine;
using System.Collections;

public class camera_track : MonoBehaviour {

	public GameObject targ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (Mathf.Lerp (transform.position.x, targ.transform.position.x, 100f*Time.deltaTime), Mathf.Lerp (transform.position.y, targ.transform.position.y, 100f*Time.deltaTime), -10f);
		//transform.rotation = targ.transform.rotation;//= Quaternion.Lerp(transform.rotation, targ.transform.rotation, 0.1f);
	}
}
