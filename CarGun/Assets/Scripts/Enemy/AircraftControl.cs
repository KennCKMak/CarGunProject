using UnityEngine;
using System.Collections;

public class AircraftControl : MonoBehaviour {
	public float speed;
	public float turnSpeed;
	private GameObject playerTarget;
	public Vector3 myPos;
	public Vector3 targetLoc;
	public float dist;
	public float hoverDistance;
	public float lockDistance;
	public float bombDistance;

	private GameObject bombPos;
	public GameObject rocket; //instantiate projectile;

	public float elapsedTime;
	public float reloadTime;
	public float sinceLastRun;
	public float runTime;

	public bool locked = false;
	public bool bombing = true;

	// Use this for initialization
	void Start () {
		bombPos = this.transform.FindChild ("BombSpawn").gameObject;
		playerTarget = GameObject.Find ("Car").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		myPos = new Vector3 (transform.position.x, 0, transform.position.z);

		transform.Translate(speed* Time.deltaTime * Vector3.forward);
		Moving ();
		if (bombing)
			Fire ();
		else
			elapsedTime = 0;
	}


	void Fire(){
		if (elapsedTime > reloadTime) {
			GameObject Rocket = Instantiate (rocket, bombPos.transform.position, bombPos.transform.rotation) as GameObject;
			//Rocket.GetComponent<AirBombControl> ().speed = this.speed;
			Vector3 speedInitial = this.GetComponent<Rigidbody>().velocity;
			speedInitial.y = -Rocket.GetComponent<AirBombControl> ().fallSpeed;
			Rocket.GetComponent<Rigidbody> ().velocity = speedInitial;
			Rocket.GetComponent<AirBombControl> ().playerTarget = this.playerTarget;
			elapsedTime = 0;
		}
		elapsedTime += Time.deltaTime;
	}

	void Moving(){
		dist = (targetLoc - myPos).magnitude;

		//not locked in straight line, rotating to target.
		if (sinceLastRun < runTime) {
			if (dist > hoverDistance) {
				targetLoc = playerTarget.transform.position;
				targetLoc.y = 0;
				Quaternion newRotation = Quaternion.LookRotation (targetLoc - myPos);
				transform.rotation = Quaternion.Lerp (transform.rotation, newRotation, turnSpeed/5 * Time.deltaTime);
			}
		} 
		if (sinceLastRun > runTime) {
			if (dist > lockDistance && dist > bombDistance && !bombing && !locked) {
				targetLoc = playerTarget.transform.position;
				targetLoc.y = 0;
				Quaternion newRotation = Quaternion.LookRotation(targetLoc - myPos);
				transform.rotation = Quaternion.Lerp (transform.rotation, newRotation, turnSpeed * Time.deltaTime);
			}
			//locked into position
			if (dist < lockDistance && dist > bombDistance && !bombing && !locked) {
				locked = true;
				transform.rotation = Quaternion.LookRotation (targetLoc - myPos);
			}
			//start bombing
			if (dist < lockDistance && dist < bombDistance && !bombing && locked)
				bombing = true;
			//stop bombing
			if (dist < lockDistance && dist > bombDistance && bombing && locked)
				bombing = false;
			//not locked in position anymore
			if (dist > lockDistance && dist > bombDistance && !bombing && locked) {
				locked = false;
				sinceLastRun = 0;
			}

		} else
			sinceLastRun += Time.deltaTime;
	}

}
