using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {

	// Use this for initialization
	public void Appquit (){
		Application.Quit ();
	}
	public void loadlevel1 (){
		Application.LoadLevel ("Level 1");
	}
	public void loadlevel2 (){
		Application.LoadLevel ("Level 2");
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
