using UnityEngine;
using System.Collections;

public class chase_target : MonoBehaviour {

	public GameObject targ;
	private NavMeshAgent navigator;

	void Awake() {
		navigator = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		navigator.SetDestination(targ.transform.position);
	}
}
