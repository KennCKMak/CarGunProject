using UnityEngine;
using System.Collections;

public class AircraftControl : MonoBehaviour {
	public float speed;
	public float rotateSpeed; 

	private GameObject bombPos;

	public GameObject rocket; //instantiate projectile;

	public float reloadTime;
	bool canFire = true;
	bool reloading = false;

	// Use this for initialization
	void Start () {
		bombPos = this.transform.FindChild ("BombSpawn").gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		transform.RotateAround (new Vector3(500, 0, 500), new Vector3 (0, 1, 0), rotateSpeed * Time.deltaTime);
		transform.Translate(speed* Time.deltaTime * Vector3.forward);
		Fire ();
	}


	void Fire(){
		if ((canFire == false) && (reloading == false)) {
			StartCoroutine (timer());
		}
		if ((canFire == true) && (reloading == false)) {
			GameObject Rocket = Instantiate (rocket, bombPos.transform.position, bombPos.transform.rotation) as GameObject;
			//Rocket.GetComponent<AirBombControl> ().speed = this.speed;
			Vector3 speedInitial = this.GetComponent<Rigidbody>().velocity;
			speedInitial.y = -Rocket.GetComponent<AirBombControl> ().fallSpeed;
			Rocket.GetComponent<Rigidbody> ().velocity = speedInitial;
			canFire = false;
		}

	}

	IEnumerator timer(){
		reloading = true;
		yield return new WaitForSeconds (reloadTime);
		canFire = true;
		reloading = false;
	}
}
