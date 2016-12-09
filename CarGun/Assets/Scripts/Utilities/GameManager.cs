using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

	private UIMaster uiMaster;
	private AudioManager audioManager;
	private GameObject pauseMenu;
	private GameObject victoryMenu;
	private GameObject defeatMenu;
	private bool noEnd;
	private bool paused;

	private GameObject bubbleshield;
	private GameObject TitanTurret;
	private GameObject LargeTurret0;
	private GameObject LargeTurret1;
	private GameObject LargeTurret2;

	public float timeInSecs;

	public GameObject carPrefab;


	public int turretsRemaining;
	public bool shieldUp;
	// Use this for initialization
	void Start () {
		noEnd = true;
		shieldUp = true;
		turretsRemaining = 10;
		audioManager = transform.GetComponent<AudioManager> ();
		paused = false;
		if (SceneManager.GetActiveScene ().name == "Main") {
			bubbleshield = GameObject.Find ("ForceField").gameObject;
			TitanTurret = GameObject.Find ("TitanTurret").gameObject;
			LargeTurret0 = GameObject.Find ("LargeTurret (0)").gameObject;
			LargeTurret1 = GameObject.Find ("LargeTurret (1)").gameObject;
			LargeTurret2 = GameObject.Find ("LargeTurret (2)").gameObject;
		}
		if (SceneManager.GetActiveScene ().name != "Menu") {
			uiMaster = GameObject.Find ("UI").GetComponent<UIMaster> ();
			pauseMenu = GameObject.Find ("UI").transform.FindChild ("PauseMenu").gameObject;
			victoryMenu = GameObject.Find ("UI").transform.FindChild ("VictoryMenu").gameObject;
			defeatMenu = GameObject.Find ("UI").transform.FindChild ("DefeatMenu").gameObject;
		}
	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name!="Menu"){
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (!paused) {
					paused = !paused;
					pauseMenu.SetActive (true);
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true; 
					Time.timeScale = 0;
				} else {
					paused = !paused;
					pauseMenu.SetActive (false);
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false; 
					Time.timeScale = 1;
				}
			}


			if (timeInSecs > 0 && noEnd) {
				timeInSecs -= Time.deltaTime;
				uiMaster.updateTimer (timeInSecs);
			} else if (timeInSecs <= 0 && noEnd) {
				timeInSecs = 0;
				uiMaster.updateTimer (timeInSecs);
				Defeat ();

			}
		}
	}

	public void TurretDied(){
		turretsRemaining--;
		uiMaster.updateTurretsRemaining (turretsRemaining);
		checkShield ();
	}
	public void checkShield(){
		if (turretsRemaining <= 4 && shieldUp)
			lowerShield ();
		
		if (turretsRemaining == 0) {
			Victory ();
			//victory thing goes here
		}
	}
	public void lowerShield(){
		shieldUp = false;
		bubbleshield.SetActive (false);
		TitanTurret.GetComponent<TurretClass> ().setTurretEnable (true);
		LargeTurret0.GetComponent<TurretClass> ().setTurretEnable (true);
		LargeTurret1.GetComponent<TurretClass> ().setTurretEnable (true);
		LargeTurret2.GetComponent<TurretClass> ().setTurretEnable (true);
		audioManager.PlayBossBGM ();
	}


	public void Victory(){
		noEnd = false;
		audioManager.PlayVictoryBGM ();

		victoryMenu.SetActive (true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true; 
		Time.timeScale = 1f;
		GameObject.Find ("Car").gameObject.GetComponent<PlayerEntity> ().spawnCarPrefab (1);
		GameObject.Find ("Car").gameObject.SetActive (false);
	}

	public void Defeat(){
		noEnd = false;
		audioManager.stopBGM ();

		defeatMenu.SetActive (true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true; 
		Time.timeScale = 1f;
		GameObject.Find ("Car").gameObject.GetComponent<PlayerEntity> ().spawnCarPrefab (2);
		GameObject.Find ("Car").gameObject.SetActive (false);
	}

	void spawnCarAtLocation(GameObject car){

		GameObject destroyedCar = Instantiate (carPrefab, car.transform.position, car.transform.rotation) as GameObject;
		destroyedCar.GetComponent<DestroyedCarClass>().turnTurret(car.transform.FindChild ("CarTurret").transform.FindChild("Turret").gameObject);
		destroyedCar.GetComponent<Rigidbody> ().velocity = car.transform.GetComponent<Rigidbody> ().velocity;
	}


}
