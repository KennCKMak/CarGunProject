using UnityEngine;
using System.Collections;

public class PlayerEntity : MonoBehaviour {

	private AudioManager audioManager;
	private GameManager gameManager;

	public GameObject carPrefab;
	public GameObject destroyedCarPrefab;

	public float health;
	public float maxHealth;
	public int curAmmo;
	public int maxAmmo;


	private UIMaster uiMaster;

	// Use this for initialization
	void Start () {
		audioManager = GameObject.Find ("GameManager").GetComponent<AudioManager> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		uiMaster = GameObject.Find ("UI").GetComponent<UIMaster> ();

		restoreHP ();
		restoreAmmo ();
	}
	
	// Update is called once per frame
	void Update () {
		uiMaster.updateHP (health, maxHealth);
		uiMaster.updateAmmo (curAmmo, maxAmmo);


		if (Input.GetKey (KeyCode.L)) 
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(0, transform.rotation.y, 0), 15 * Time.deltaTime);
		if (Input.GetKeyDown (KeyCode.K))
			takeDamage (10);
		if (Input.GetKeyDown (KeyCode.R))
			setAmmo (0);
		if (Input.GetKeyDown (KeyCode.J))
			healDamage (10);


		if (!isAlive ()) {
			audioManager.PlaySE_CarDestroyed ();
			uiMaster.updateHP (health, maxHealth);
			gameManager.Defeat ();
		}
	}

	public void spawnCarPrefab(int num){

		if (num == 1) {
			GameObject destroyedCar = Instantiate (carPrefab, transform.position, transform.rotation) as GameObject;
			destroyedCar.GetComponent<DestroyedCarClass> ().turnTurret (transform.FindChild ("CarTurret").transform.FindChild ("Turret").gameObject);
			destroyedCar.GetComponent<Rigidbody> ().velocity = transform.GetComponent<Rigidbody> ().velocity;
		}
		if (num == 2) {
			GameObject destroyedCar = Instantiate (destroyedCarPrefab, transform.position, transform.rotation) as GameObject;
			destroyedCar.GetComponent<DestroyedCarClass> ().turnTurret (transform.FindChild ("CarTurret").transform.FindChild ("Turret").gameObject);
			destroyedCar.GetComponent<Rigidbody> ().velocity = transform.GetComponent<Rigidbody> ().velocity;
	
		}
	}

	public bool isAlive(){
		if (health > 0)
			return true;
		else
			return false;
	}
	public void takeDamage(float num){
		health -= num;
		audioManager.PlaySE_CarGotHit ();
	}
	public void healDamage (float num){
		health += num;
	}
	public void restoreHP(){
		health = maxHealth;
	}



	public bool hasAmmo(){
		if (curAmmo > 0)
			return true;
		else
			return false;
	}

	public void useAmmo(int num){
		curAmmo -= num;
	}

	public void addAmmo(int num){
		curAmmo += num;
	}

	public void restoreAmmo(){
		curAmmo = maxAmmo;
	}

	public void setAmmo(int num){
		curAmmo = num;
	}


	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Boulder") {
			takeDamage(col.GetComponent<BoulderClass>().damage);
		}
	}
}
