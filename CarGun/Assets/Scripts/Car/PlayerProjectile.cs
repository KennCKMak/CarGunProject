using UnityEngine;
using System.Collections;

public class PlayerProjectile : MonoBehaviour {
	public float speed = 20.0f;
	public float wpnDmg = 20f;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = transform.GetComponent<Rigidbody> ();
		Destroy (gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = transform.forward * speed;
		//transform.Translate(speed* Time.deltaTime * Vector3.forward);
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Turret") {
			col.gameObject.GetComponent<TurretClass> ().takeDamage (wpnDmg);
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Terrain")
			Destroy (gameObject);
	}
}
