using UnityEngine;
using System.Collections;

public class PlayerTurretControl : MonoBehaviour {

	bool canFire = true;
	bool reloading = false;

	public float turnSpeed = 25;
	public GameObject bulletPrefab;
	public float TwinBarrelDelay = 0.5f;

	private float yOffset; //value taken from crosshair position

	private GameObject Turret;
	private GameObject TwinBarrel;
	private int BarrelNum = 1;
	private GameObject TwinBarrelS1;
	private GameObject TwinBarrelS2;

	private AudioManager audioManager;

	// Use this for initialization
	void Start () {
		audioManager = GameObject.Find ("GameManager").GetComponent<AudioManager> ();
		yOffset = transform.GetComponent<Crosshair> ().yOffset;
		Turret = transform.FindChild ("CarTurret").transform.FindChild("Turret").gameObject;
		TwinBarrel = Turret.transform.FindChild ("TwinBarrel").gameObject;
		TwinBarrelS1 = TwinBarrel.transform.FindChild ("Spawn1").gameObject;
		TwinBarrelS2 = TwinBarrel.transform.FindChild ("Spawn2").gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		RotateToCrosshair ();


		if (Input.GetMouseButton (0)) {
			Firing ();
		}
	}

	void RotateToCrosshair(){
		Vector3 target;
		Ray ray = Camera.main.ScreenPointToRay (new Vector3 ((Screen.width) / 2, (Screen.height) /2 + yOffset, 0));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit))
			target = hit.point;
		else
			target = ray.GetPoint (1000f);
		Quaternion newRotation = Quaternion.LookRotation (target - Turret.transform.position);
		Turret.transform.rotation = Quaternion.Lerp (Turret.transform.rotation, newRotation, turnSpeed * Time.deltaTime);
	}

	void Firing(){
		if ((canFire == false) && (reloading == false)) {
			StartCoroutine (timer());
		}
		if ((canFire == true) && (reloading == false)) {
			switch (BarrelNum) {
			case(1):
				Instantiate (bulletPrefab, TwinBarrelS1.transform.position, TwinBarrelS1.transform.rotation);
				canFire = false;
				BarrelNum++;
				break;
			case(2):
				Instantiate (bulletPrefab, TwinBarrelS2.transform.position, TwinBarrelS2.transform.rotation);
				canFire = false;
				BarrelNum++;
				break;

			default:
				break;

			}
			audioManager.PlaySE_BarrelFire ();
			if (BarrelNum >2)
				BarrelNum = 1;
		}

	}

	IEnumerator timer(){
		reloading = true;
		yield return new WaitForSeconds (TwinBarrelDelay);
		canFire = true;
		reloading = false;
	}
}
