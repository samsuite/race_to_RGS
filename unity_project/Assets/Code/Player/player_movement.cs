using UnityEngine;
using System.Collections;

public class player_movement : MonoBehaviour {

	private Rigidbody2D rb;

	private Vector3 vel_vec = new Vector3(0,0,0);
	private float accel = 0f;
	private float brake = 0f;
	private float h_axis = 0f;
	private float v_axis = 0f;
	public float speed = 0f;
	private float disp = 0f;

	private const float max_vel = 20f;
	private const float friction = 0.9f;
	private const float rot_friction = 0.9f;

	private float framespeed = 0f;
	private float rot_vel = 0f;

	public GameObject cam;
	Vector3 cam_offset = new Vector3(0,0,-10f);

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		accel = Input.GetAxis("right_trigger");
		brake = Input.GetAxis ("left_trigger");
		h_axis = Input.GetAxis ("horizontal_joystick_1");
		v_axis = Input.GetAxis ("vertical_joystick_1");
		framespeed = Time.deltaTime;

		speed += (accel - brake)/2f;
		if (accel < 0.5){
			speed *= friction;
		}

		if (speed > 20) {
			speed = 20;
		}
		if (speed < -5) {
			speed = -5;
		}
		
		vel_vec.Set(0,speed,0);
		vel_vec = transform.rotation * vel_vec;
		//transform.Translate(vel_vec*framespeed);
		disp = Mathf.Sqrt(rb.velocity.magnitude + speed);

		rb.AddTorque(-disp*h_axis*15f);
		rb.AddForce(vel_vec*20f);

		//cam.transform.position = transform.position;//Vector3.Lerp(cam.transform.position, transform.position, 20f*framespeed);
		//cam.transform.position += cam_offset;
		//cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, transform.rotation, 0.5f);

	}
}
