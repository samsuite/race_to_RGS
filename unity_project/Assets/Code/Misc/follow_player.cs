using UnityEngine;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class follow_player : MonoBehaviour{

	public GameObject targ;
	private player_movement pm;
	private Rigidbody2D rb;

	public int gear = 1;
	public bool reverse = false;

	private Vector3 vel_vec = new Vector3(0,0,0);
	public float accel = 0f;
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

	private float diff_x = 0f;
	private float diff_y = 0f;
	
	//public GameObject lWheel;
	//public GameObject rWheel;

	void Awake() {
		pm = targ.GetComponent<player_movement>();
		rb = GetComponent<Rigidbody2D>();
	}

	private PolyNavAgent _agent;
	public PolyNavAgent agent{
		get {
			if (!_agent)
				_agent = GetComponent<PolyNavAgent>();
			return _agent;			
		}
	}
	
	void Update() {
		agent.SetDestination(pm.player.transform.position);


		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.up, 4f);


		int num_points = agent.activePath.Count;
		if (num_points > 0){
			diff_x = (transform.position.x - agent.activePath[0].x);
			diff_y = (transform.position.y - agent.activePath[0].y);
		}

		h_axis = ((transform.localEulerAngles.z - (Mathf.Atan2 (diff_y, diff_x) * 180f / Mathf.PI) + 270f) % 360f - 180f) / -90f;
		accel = 1f;
		if (h_axis > 1f){
			h_axis = 1f;
		}
		if (h_axis < -1f){
			h_axis = -1f;
		}
		/*if (Mathf.Abs ((transform.localEulerAngles.z - (Mathf.Atan2 (diff_y, diff_x) * 180f / Mathf.PI) + 270f) % 360f - 180f) < 30f) {
			accel = -1f;
		}*/
		if (hit.collider != null) {
			accel = -1f;
		}


		/*foreach (Vector2 v in agent.activePath){
			print (v.x);
			print (v.y);
			print ("---");
		}
		print ("   ");*/


		framespeed = Time.deltaTime;

		if (gear == 1) {
			max_vel = 1f;
			min_vel = 0f;
			accel_rate = 5f;
		}
		else if (gear == 2) {
			max_vel = 5f;
			min_vel = 1f;
			accel_rate = 50f;
		}
		else if (gear == 3) {
			max_vel = 12f;
			min_vel = 5f;
			accel_rate = 100f;
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
		disp = Mathf.Sqrt(Mathf.Abs(rb.velocity.magnitude + speed));
		
		if (disp * h_axis != 0) {
			if (speed > 0) {
				rb.AddTorque (-disp * h_axis * 15f * framespeed*50f);
			} else {
				rb.AddTorque (disp * h_axis * 15f * framespeed*50f);
			}
		}
		rb.AddForce(vel_vec*20f * framespeed*50f);

	}
}
