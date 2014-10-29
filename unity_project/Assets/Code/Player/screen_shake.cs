using UnityEngine;
using System.Collections;

public class screen_shake : MonoBehaviour {

	public GameObject targ;
	public int shaketime = 0;
	Vector3 cam_offset = new Vector3(0,0,-10f);

	void Update () {

		transform.position = targ.transform.position;
		transform.position += cam_offset;

		if (shaketime > 0){
			shaketime -= 1;
			if(shaketime%2==0){
				transform.Translate(Random.insideUnitCircle/100f*shaketime);
			}
		}
	}

	public void SwitchCar(GameObject new_car) {
		targ = new_car;
	}
}
