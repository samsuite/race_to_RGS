using UnityEngine;
using System.Collections;

public class chase_target : MonoBehaviour {
	public GameObject targ;
	private NavMeshAgent navigator;
	public float distance;
	private player_movement pm;

	void Awake() {
		navigator = GetComponent<NavMeshAgent>();
		pm = GetComponent<player_movement> ();
	}
	
	void Update () {
		distance = Mathf.Sqrt (Mathf.Pow(targ.transform.position.x - pm.player.transform.position.x,2f) + Mathf.Pow (targ.transform.position.y - pm.player.transform.position.y,2f));
		if (distance < 10f) {
			navigator.SetDestination(targ.transform.position);
		}
	}
}
