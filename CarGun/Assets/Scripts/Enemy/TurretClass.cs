using UnityEngine;
using System.Collections;

public class TurretClass : MonoBehaviour {

	public bool isEnabled;
	public float health;
	public float maxHealth;
	public GameObject destroyedPrefab;
	private TurretAttack turretAttack;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		turretAttack = transform.GetComponent<TurretAttack> ();
		setTurretEnable (isEnabled);
		restoreHP ();

		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	}


	void checkHP(){
		if (!isAlive()) {
			GameObject newPrefab = Instantiate (destroyedPrefab, transform.position, transform.rotation) as GameObject;
			newPrefab.GetComponent<DestroyedTurretClass> ().destroySelf (5f);
			gameManager.TurretDied ();
			Destroy (gameObject);
		}

	}

	public void setTurretEnable(bool var){
		if (var) {
			turretAttack.isEnabled = true;
			this.isEnabled = true;
		} else {
			turretAttack.isEnabled = false;
			this.isEnabled = false;
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
		checkHP ();
	}
	public void healDamage (float num){
		health += num;
	}
	public void restoreHP(){
		health = maxHealth;
	}
}
