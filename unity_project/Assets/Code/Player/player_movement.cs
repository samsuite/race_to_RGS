using UnityEngine;
using System.Collections;

public class player_movement : MonoBehaviour {

	private Rigidbody2D rb;
	private screen_shake cam_shake;

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
	//private float rot_vel = 0f;
	private float accel_rate = 5f;
	private const float friction = 0.99f;
	private const float rot_friction = 0.9f;

	private float framespeed = 0f;

	public GameObject player;
	public GameObject cam;
	//public GameObject lWheel;
	//public GameObject rWheel;

	//Turning speed variables
	private float turn = 17f;

	//private Component[] trailrenderer;
	void Awake() {
		rb = player.GetComponent<Rigidbody2D>();
		cam_shake = cam.GetComponent<screen_shake>();
		cam_shake.SwitchCar(player);
	}



	void Update () {

		accel = Mathf.Clamp01(Input.GetAxis("right_trigger") + Input.GetAxis("forward"));
		brake = Mathf.Clamp01(Input.GetAxis("left_trigger") + Input.GetAxis("backward"));

		h_axis = Input.GetAxis ("Horizontal");
		v_axis = Input.GetAxis ("Vertical");
		if(h_axis > 0){
			h_axis = Mathf.Pow(h_axis, 2f);
		}
		else{
			h_axis = -Mathf.Pow(h_axis, 2f);
		}

		framespeed = Time.deltaTime;


		if (gear == 1) {
			max_vel = 2f;
			min_vel = 0f;
			accel_rate = 2f;
		}
		else if (gear == 2) {
			max_vel = 10f;
			min_vel = 2f;
			accel_rate = 30f;
		}
		else if (gear == 3) {
			max_vel = 24f;
			min_vel = 10f;
			accel_rate = 80f;
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

		if(brake > 0 && transform.InverseTransformDirection(rb.velocity).x > -0.1f && speed > 0){
			speed = 0f;
		}
		
		vel_vec.Set(0,speed,0);
		vel_vec = player.transform.rotation * vel_vec;
		//transform.Translate(vel_vec*framespeed);
		disp = Mathf.Sqrt(Mathf.Abs(rb.velocity.magnitude + speed));
		//As the gear (or speed) goes up, the car will be able to turn less quickly
		if (disp * h_axis != 0) {
			if (speed > 0) {
				rb.AddTorque (-disp * h_axis * (turn - gear) * framespeed*50f);
			} else {
				rb.AddTorque (disp * h_axis * (turn - gear) * framespeed*50f);
			}
		}
		rb.AddForce(vel_vec*20f * framespeed*50f);

		//rWheel.transform.localEulerAngles = new Vector3(0f,0f,h_axis * -30);
		//lWheel.transform.localEulerAngles = new Vector3(0f,0f,h_axis * -30);

	}

	void FixedUpdate () {
		cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, player.transform.rotation, 0.25f);
	}

	public void SwitchCar(GameObject new_car) {
		player = new_car;
		rb = player.GetComponent<Rigidbody2D>();
		cam_shake.SwitchCar(new_car);
	}
}