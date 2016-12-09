using UnityEngine;
using System.Collections;

public class RocketControl : MonoBehaviour {
	private GameObject playerTarget;
	public float speed;
	public float randomValue = 25f;
	private AudioManager audioManager;

	public float turnSpeed;
	public float elapsedTime;
	public float turnTime;
	public float lockTime;

	public float damage;
	public float explosionRadius;
	public float explosiveForce;
	public GameObject explosionPrefab;
	private Vector3 initDistance;
	private Vector3 distance;

	// Use this for initialization
	void Start () {
		if (GameObject.Find("Car") != null)
			playerTarget = GameObject.Find ("Car").gameObject;
		audioManager = GameObject.Find ("GameManager").GetComponent<AudioManager> ();
		randomValue = Random.Range (-randomValue, randomValue);


		Destroy (gameObject, 15f);
		initDistance = (playerTarget.transform.position - this.transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Moving ();
	}


	void OnCollisionEnter (Collision col){
		if (col.transform.tag == "Terrain") {
			//checkHitPlayer (); this is for AoE attacks
		} else if (col.transform.tag == "Player") {
			audioManager.PlaySE_Explosion (0.4f);
			playerTarget.GetComponent<PlayerEntity> ().takeDamage (damage);
			playerTarget.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce*800, transform.position, explosionRadius, 1f, ForceMode.Impulse);
		} 
		spawnExplosion ();
		Destroy (gameObject);
	}


	void checkHitPlayer(){
		Vector3 dist = (playerTarget.transform.position - transform.position);
		if (dist.magnitude < explosionRadius) {
			playerTarget.GetComponent<PlayerEntity> ().takeDamage (damage);
			playerTarget.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce*800, transform.position, explosionRadius, 1f, ForceMode.Impulse);
		}
	}


	void Moving(){
		transform.Translate (speed * Time.deltaTime * Vector3.forward);

		elapsedTime += Time.deltaTime;
		if (elapsedTime < turnTime) { //just exited
			return;
		} 

		if (elapsedTime > turnTime && elapsedTime < lockTime) { //turning now
			distance = (playerTarget.transform.position - this.transform.position);
			Quaternion newRotation = Quaternion.LookRotation (distance);
			transform.rotation = Quaternion.Lerp (transform.rotation, newRotation, turnSpeed * Time.deltaTime);
		} else if (elapsedTime > turnTime){
			if (elapsedTime > lockTime || distance.magnitude <= initDistance.magnitude / 2) {
				Quaternion newRotation = Quaternion.LookRotation (distance);
				transform.rotation = Quaternion.Lerp (transform.rotation, newRotation, turnSpeed * 2 * Time.deltaTime);
			}
		}
	}

	void spawnExplosion(){
		GameObject newExplosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		Destroy (newExplosion, 1f);
	}
}
