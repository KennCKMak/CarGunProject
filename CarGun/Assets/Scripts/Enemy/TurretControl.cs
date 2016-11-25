using UnityEngine;
using System.Collections;

public class TurretControl : MonoBehaviour {
	public float reloadTime;
	public float turnSpeed;
	[SerializeField] private GameObject missile;

	private GameObject turretLauncher;
	private GameObject launch1;
	private GameObject launch2;
	private GameObject launch3;

	private int launchNum = 1;

	private GameObject target;
	private Vector3 targetLoc;
	private Vector3 spawnDisplacement;


	bool canFire = false;
	bool reloading = false;

	// Use this for initialization
	void Start () {
		turretLauncher = this.transform.FindChild ("TurretLauncher").gameObject;
		launch1 = turretLauncher.transform.FindChild ("Launch1").gameObject;
		launch2 = turretLauncher.transform.FindChild ("Launch2").gameObject;
		launch3 = turretLauncher.transform.FindChild ("Launch3").gameObject;
		target = GameObject.Find ("Car").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		targetLoc = new Vector3 (target.transform.position.x, this.transform.position.y, target.transform.position.z);
		//turretLauncher.transform.LookAt (targetLoc);
		//turretLauncher.transform.eulerAngles = new Vector3 (0, turretLauncher.transform.eulerAngles.y, 0);


		Quaternion newRotation = Quaternion.LookRotation (targetLoc - turretLauncher.transform.position);
		turretLauncher.transform.rotation = Quaternion.Lerp (turretLauncher.transform.rotation, newRotation, turnSpeed * Time.deltaTime);


		Fire ();
	}

	void Fire(){
		if ((canFire == false) && (reloading == false)) {
			StartCoroutine (timer());
		}
		if ((canFire == true) && (reloading == false)) {
			switch (launchNum) {
			case(1):
				spawnDisplacement = launch1.transform.position;
				spawnDisplacement.x -= 1.771164f;
				Instantiate (missile, spawnDisplacement, launch1.transform.rotation);
				canFire = false;
				launchNum++;
				break;
			case(2):
				spawnDisplacement = launch2.transform.position;
				spawnDisplacement.x -= 1.771164f;
				Instantiate (missile, spawnDisplacement, launch2.transform.rotation);
				canFire = false;
				launchNum++;
				break;
			case(3):
				spawnDisplacement = launch3.transform.position;
				spawnDisplacement.x -= 1.771164f;
				Instantiate (missile, spawnDisplacement, launch3.transform.rotation);
				canFire = false;
				launchNum++;
				break;
			default:
				break;

			}
			if (launchNum > 3)
				launchNum = 1;
		}

	}

	IEnumerator timer(){
		reloading = true;
		yield return new WaitForSeconds (reloadTime);
		canFire = true;
		reloading = false;
	}
}

