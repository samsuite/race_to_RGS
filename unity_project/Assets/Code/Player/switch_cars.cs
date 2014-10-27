using UnityEngine;
using System.Collections;

public class switch_cars : MonoBehaviour {

	GameObject[] available_cars;
	GameObject target_car;

	private player_movement pm;
	private Rigidbody2D rb;

	void Start () {
		pm = GetComponent<player_movement>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {

		if (Input.GetButtonDown("a_button")){
			print("button is down");
			available_cars = GameObject.FindGameObjectsWithTag("Car");

				float min_dist = 100f;
				float dist = 5f;
			if (available_cars.Length > 0){
				foreach (GameObject car in available_cars) {
					dist = Mathf.Sqrt(Mathf.Pow(car.transform.position.x - transform.position.x,2f) + Mathf.Pow(car.transform.position.y - transform.position.y,2f));
					if (dist < min_dist){
						min_dist = dist;
						target_car = car;
					}
	        	}
	        }

        	if (dist < 5f){
        		transform.position = target_car.transform.position;
        		transform.rotation = target_car.transform.rotation;
        		pm.gear = 1;
        		pm.speed = 0f;
        		//rigidbody2D.velocity = Vector3(0f,0f,0f);

        		Destroy(target_car);
        	}

		}

	
	}
}
