using UnityEngine;
using System.Collections;

public class HazardMine : MonoBehaviour {
	public float damage;
	public AudioManager audioManager;
	public float explosiveForce;
	//[SerializeField] private GameObject explosionFX;
	// Use this for initialization
	void Start () {
		audioManager = GameObject.Find ("GameManager").GetComponent<AudioManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){
		if (col.transform.tag == "Terrain")
			return;
		if (col.transform.tag == "Player") {
			if (col.gameObject.GetComponent<PlayerEntity> () != null) {
				col.gameObject.GetComponent<PlayerEntity> ().takeDamage (damage);
				PushCar (col.gameObject);
				audioManager.PlaySE_MineExplode ();
				Destroy (this.gameObject);
			}
		}
	}

	void PushCar(GameObject target){
		//target.GetComponent<Rigidbody> ().AddForce (targetVector * explosiveForce, ForceMode.VelocityChange); //(targetVector * 1000f, ForceMode.Force);
		target.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce*1000, transform.position, 5f, 10f, ForceMode.Impulse);
		//target.GetComponent<Rigidbody> ().velocity = (targetVector * explosiveForce);

	}
}
