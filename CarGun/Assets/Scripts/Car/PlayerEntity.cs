using UnityEngine;
using System.Collections;

public class PlayerEntity : MonoBehaviour {


	public float health;
	public float maxHealth;
	public int curAmmo;
	public int maxAmmo;


	private UIMaster uiMaster;

	// Use this for initialization
	void Start () {
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



	}


	public bool isAlive(){
		if (health > 0)
			return true;
		else
			return false;
	}
	public void takeDamage(float num){
		health -= num;
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

}
