using UnityEngine;
using System.Collections;

public class switch_cars : MonoBehaviour {

	GameObject[] available_cars;
	public GameObject target_car;

	private player_movement pm;

	void Start () {
		pm = GetComponent<player_movement>();
	}
	
	void Update () {

		if (Input.GetButtonDown("a_button")){
			print("button is down");
			available_cars = GameObject.FindGameObjectsWithTag("Car");

			float min_dist = 100f;
			float dist = 5f;

			if (available_cars.Length > 0){
				foreach (GameObject car in available_cars) {
					if (car != pm.player){
						dist = Mathf.Sqrt(Mathf.Pow(car.transform.position.x - pm.player.transform.position.x,2f) + Mathf.Pow(car.transform.position.y - pm.player.transform.position.y,2f));
						if (dist < min_dist){
							min_dist = dist;
							target_car = car;
						}
					}
	        	}
	        }

	        print(min_dist);
        	if (min_dist < 5f){
        		pm.SwitchCar(target_car);
        		pm.speed = 0f;

        		//Destroy(target_car);
        	}

		}

	
	}
}
