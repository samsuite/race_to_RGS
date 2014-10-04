using UnityEngine;
using System.Collections;

public class player_movement : MonoBehaviour {

	private Vector3 vel_vec = new Vector3(0,0,0);
	private float accel = 0f;
	private float brake = 0f;
	private float h_axis = 0f;
	private float v_axis = 0f;
	public float p_vel = 0f;

	private const float max_vel = 1f;
	private float framespeed = 0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		accel = Input.GetAxis("right_trigger");
		brake = Input.GetAxis ("left_trigger");
		h_axis = Input.GetAxis ("horizontal_joystick_1");
		v_axis = Input.GetAxis ("vertical_joystick_1");
		framespeed = Time.deltaTime;

		p_vel += accel/4f;
		p_vel -= brake/2f;
		if (p_vel > max_vel) {
			p_vel = max_vel;
		}
		if (p_vel > 0) {
			p_vel = 0;
		}
		
		vel_vec.Set(0,0,-p_vel);
		transform.Translate(vel_vec*framespeed);
		//transform.Rotate();
	}
}
