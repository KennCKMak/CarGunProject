using UnityEngine;
using System.Collections;

public class TurretAttack : MonoBehaviour {
	[SerializeField] private GameObject missile;

	public bool isEnabled;

	private GameObject turretLauncher;
	private GameObject launch1;
	private GameObject launch2;
	private GameObject launch3;

	private int launchNum = 1;

	private GameObject target;
	private Vector3 targetLoc;
	private Vector3 spawnDisplacement;
	private	Quaternion rocketRotation;

	public float finalHelperValue;


	bool canFire = false;

	public float turnSpeed;
	public float damage;
	public float explosionRadius;
	public float minAtkRange;
	public float maxAtkRange;
	public float elapsedTime;
	public float fireRate;

	public bool barrage; //more than one at a time
	public int barrageNum; //number of times 
	public int barrageCount;
	public float barrageRate; //space between rockets in barrage
	public float barrageReload; //reload time

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
		if (isEnabled) {
			targetLoc = new Vector3 (target.transform.position.x, this.transform.position.y, target.transform.position.z);
			//turretLauncher.transform.LookAt (targetLoc);
			//turretLauncher.transform.eulerAngles = new Vector3 (0, turretLauncher.transform.eulerAngles.y, 0);
			if ((targetLoc - transform.position).magnitude < maxAtkRange) {
				Quaternion newRotation = Quaternion.LookRotation (targetLoc - turretLauncher.transform.position);
				turretLauncher.transform.rotation = Quaternion.Lerp (turretLauncher.transform.rotation, newRotation, turnSpeed * Time.deltaTime);
				canFire = true;
			} else {
				turretLauncher.transform.RotateAround (turretLauncher.transform.position, Vector3.up, turnSpeed / 5);
				canFire = false;
			}

			if (canFire && !barrage)
				Fire ();
			else if (canFire && barrage) {
				FireBarrage ();

			} else
				elapsedTime = 0;
		} else {
			turretLauncher.transform.RotateAround (turretLauncher.transform.position, Vector3.up, turnSpeed / 5);
		}
	}

	void Fire(){
		if (elapsedTime > fireRate){
			switch (launchNum) {
			case(1):
				spawnDisplacement = launch1.transform.position;
				spawnDisplacement.x -= 1.771164f;
				rocketRotation = launch1.transform.rotation;
				launchNum++;
				break;
			case(2):
				spawnDisplacement = launch2.transform.position;
				spawnDisplacement.x -= 1.771164f;
				rocketRotation = launch1.transform.rotation;
				launchNum++;
				break;
			case(3):
				spawnDisplacement = launch3.transform.position;
				spawnDisplacement.x -= 1.771164f;
				rocketRotation = launch1.transform.rotation;
				launchNum++;
				break;
			default:
				break;

			}

			GameObject rocket = Instantiate (missile, spawnDisplacement, rocketRotation) as GameObject;
			rocket.GetComponent<RocketControl> ().damage = damage;
			rocket.GetComponent<RocketControl> ().explosionRadius = explosionRadius;
			rocket.GetComponent<RocketControl> ().finalHelperValue = finalHelperValue;

			if (launchNum > 3)
				launchNum = 1;
			elapsedTime = 0;
		}
		elapsedTime += Time.deltaTime;
	}

	void FireBarrage(){
		if (elapsedTime > barrageRate && barrageCount > 0) {
			switch (launchNum) {
			case(1):
				spawnDisplacement = launch1.transform.position;
				spawnDisplacement.x -= 1.771164f;
				rocketRotation = launch1.transform.rotation;
				launchNum++;
				barrageCount--;
				break;
			case(2):
				spawnDisplacement = launch2.transform.position;
				spawnDisplacement.x -= 1.771164f;
				rocketRotation = launch2.transform.rotation;
				launchNum++;
				barrageCount--;
				break;
			case(3):
				spawnDisplacement = launch3.transform.position;
				spawnDisplacement.x -= 1.771164f;
				rocketRotation = launch3.transform.rotation;
				launchNum++;
				barrageCount--;
				break;
			default:
				break;
			}

			GameObject rocket = Instantiate (missile, spawnDisplacement, rocketRotation) as GameObject;
			rocket.GetComponent<RocketControl> ().damage = damage;
			rocket.GetComponent<RocketControl> ().explosionRadius = explosionRadius;
			rocket.GetComponent<RocketControl> ().finalHelperValue = finalHelperValue;

			if (launchNum > 3)
				launchNum = 1;
			elapsedTime = 0;
		}
		if (barrageCount == 0 && elapsedTime > barrageReload)
			barrageCount = barrageNum;
		elapsedTime += Time.deltaTime;
	}
}

