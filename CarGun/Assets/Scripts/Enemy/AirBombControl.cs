using UnityEngine;
using System.Collections;

public class AirBombControl : MonoBehaviour {
	public float fallSpeed;
	public float existTime;
	public float damage;
	public float explosionRadius;
	public float explosiveForce;
	public GameObject explosionPrefab;
	public GameObject playerTarget;

	private AudioManager audioManager;


	private float height;

	private float timeToHit;
	private float rotSpeed;
	// Use this for initialization
	void Start () {
		audioManager = GameObject.Find ("GameManager").GetComponent<AudioManager> ();
		if (playerTarget == null)
			playerTarget = GameObject.Find ("Car").gameObject;

		height = this.transform.position.y;
		timeToHit = height / fallSpeed;//seconds it takes to hit
		existTime = timeToHit + 3; //falls past screen for 5 seconds
		rotSpeed = 90f / (timeToHit);
		Destroy (gameObject, existTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//transform.Translate(speed* Time.deltaTime * Vector3.forward);
		if (transform.eulerAngles.x <= 89)
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x + rotSpeed*Time.deltaTime, transform.eulerAngles.y, transform.eulerAngles.z);
		else
			rotSpeed = 0;
	}

	void OnCollisionEnter (Collision col){
		spawnExplosion ();
		audioManager.PlaySE_Explosion ();
		checkHitPlayer ();
		Destroy (gameObject);

	}


	void checkHitPlayer(){
		Vector3 dist = (playerTarget.transform.position - transform.position);
		if (dist.magnitude < explosionRadius) {
			playerTarget.GetComponent<PlayerEntity> ().takeDamage (damage);
			playerTarget.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce*500, transform.position, explosionRadius, 1f, ForceMode.Impulse);
		}
	}

	void spawnExplosion(){
		GameObject newExplosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		Destroy (newExplosion, 1f);
	}
}
