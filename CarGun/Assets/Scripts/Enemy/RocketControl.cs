using UnityEngine;
using System.Collections;

public class RocketControl : MonoBehaviour {
	private GameObject playerTarget;
	public float speed;
	private float timeToHit;
	private float rotSpeed;
	private float existTime;
	public float randomValue = 25f;
	public float finalHelperValue;

	public float damage;
	public float explosionRadius;
	public float explosiveForce;
	private Vector3 distance;
	private float magnitude;

	// Use this for initialization
	void Start () {
		playerTarget = GameObject.Find ("Car").gameObject;

		randomValue = Random.Range (-randomValue, randomValue);
		magnitude = Vector3.Distance (playerTarget.transform.position, this.transform.position) + Random.Range(-1f, 1f)*(randomValue - finalHelperValue) -15f;

		timeToHit = magnitude / speed;
		existTime = timeToHit + 3; //falls past screen for x seconds
		rotSpeed = 24f / (timeToHit);
		Destroy (gameObject, existTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(speed* Time.deltaTime * Vector3.forward);
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x + rotSpeed*Time.deltaTime, transform.eulerAngles.y, transform.eulerAngles.z);
	}


	void OnCollisionEnter (Collision col){
		if (col.transform.tag == "Terrain") {
			checkHitPlayer ();
		} else if (col.transform.tag == "Player") {
			playerTarget.GetComponent<PlayerEntity> ().takeDamage (damage);
			playerTarget.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce*1000, transform.position, explosionRadius, 1f, ForceMode.Impulse);
		} 

		Destroy (gameObject);
	}


	void checkHitPlayer(){
		Vector3 dist = (playerTarget.transform.position - transform.position);
		if (dist.magnitude < explosionRadius) {
			playerTarget.GetComponent<PlayerEntity> ().takeDamage (damage);
			playerTarget.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce*1000, transform.position, explosionRadius, 1f, ForceMode.Impulse);
		}
	}
}
