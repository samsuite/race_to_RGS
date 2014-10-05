using UnityEngine;
using System.Collections;

public class player_movement : MonoBehaviour {

	private Rigidbody2D rb;

	public int gear = 1;
	public bool reverse = false;

	private Vector3 vel_vec = new Vector3(0,0,0);
	private float accel = 0f;
	private float brake = 0f;
	private float h_axis = 0f;
	private float v_axis = 0f;
	public float speed = 0f;
	private float disp = 0f;

	private float max_vel = 20f;
	private float min_vel = 20f;
	private float rot_vel = 0f;
	private float accel_rate = 5f;
	private const float friction = 0.99f;
	private const float rot_friction = 0.9f;

	private float framespeed = 0f;

	public GameObject cam;
	public GameObject lWheel;
	public GameObject rWheel;


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


		if (gear == 1) {
			max_vel = 5f;
			min_vel = 0f;
			accel_rate = 10f;
		}
		else if (gear == 2) {
			max_vel = 15f;
			min_vel = 5f;
			accel_rate = 20f;
		}
		else if (gear == 3) {
			max_vel = 30f;
			min_vel = 15f;
			accel_rate = 50f;
		}

		if (accel == 0) {
			reverse = true;
			speed -= brake/10f;
		}
		else{
			reverse = false;
		}

		speed += (accel * (1f-brake))/accel_rate;

		if (((accel * (1f-brake))/2f) < 0.5){
			speed *= friction;
		}


		if (speed > max_vel) {
			if (gear < 3){
				gear += 1;
			}
			else{
				speed = max_vel;
			}
		}
		if (speed < min_vel) {
			if (gear > 1){
				gear -= 1;
			}
		}

		if (speed < -3f){
			speed = -3f;
		}
		
		vel_vec.Set(0,speed,0);
		vel_vec = transform.rotation * vel_vec;
		//transform.Translate(vel_vec*framespeed);
		disp = Mathf.Sqrt(Mathf.Abs(rb.velocity.magnitude + speed));

		if (disp * h_axis != 0) {
			if (speed > 0) {
				rb.AddTorque (-disp * h_axis * 15f);
			} else {
				rb.AddTorque (disp * h_axis * 15f);
			}
		}
		rb.AddForce(vel_vec*20f);

		rWheel.transform.localEulerAngles = new Vector3(0f,0f,h_axis * -30);
		lWheel.transform.localEulerAngles = new Vector3(0f,0f,h_axis * -30);

		cam.transform.position = transform.position;
		cam.transform.position += cam_offset;

	}

	void FixedUpdate () {
		cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, transform.rotation, 0.25f);
	}
}
