using UnityEngine;
using System.Collections;

public class PlayerTurretControl : MonoBehaviour {

	public float turnSpeed = 25;
	public GameObject bulletPrefab;
	public float TwinBarrelDelay = 0.5f;
	public float elapsedTime;

	private float yOffset; //value taken from crosshair position

	private PlayerEntity player;


	private GameObject Turret;

	public float TwinBarrelBulletDamage;
	public float TwinBarrelReload;
	private GameObject TwinBarrel;
	private int BarrelNum = 1;
	private GameObject TwinBarrelS1;
	private GameObject TwinBarrelS2;
	private Vector3 barrelPos;
	private Quaternion barrelRot;

	private AudioManager audioManager;

	// Use this for initialization
	void Start () {
		player = transform.GetComponent<PlayerEntity> ();

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
		elapsedTime += Time.deltaTime;
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
		if (elapsedTime > TwinBarrelDelay && player.hasAmmo ()) {
			switch (BarrelNum) {
			case(1):
				barrelPos = TwinBarrelS1.transform.position;
				barrelRot = TwinBarrelS1.transform.rotation;
				BarrelNum++;
				break;
			case(2):
				barrelPos = TwinBarrelS2.transform.position;
				barrelRot = TwinBarrelS2.transform.rotation;
				BarrelNum++;
				break;
			default:
				break;

			}
			if (BarrelNum > 2)
				BarrelNum = 1;
			GameObject bullet = Instantiate (bulletPrefab, barrelPos, barrelRot) as GameObject;
			bullet.GetComponent<PlayerProjectile> ().wpnDmg = TwinBarrelBulletDamage;
			player.useAmmo (1);
			audioManager.PlaySE_BarrelFire ();
			elapsedTime = 0;
		} else if (!player.hasAmmo ()) {
			if (elapsedTime > TwinBarrelReload)
				player.restoreAmmo ();
		}
	}
}
