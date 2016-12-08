using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

	private UIMaster uiMaster;
	private GameObject pauseMenu;
	private bool paused;

	private GameObject bubbleshield;
	private GameObject TitanTurret;
	private GameObject LargeTurret0;
	private GameObject LargeTurret1;
	private GameObject LargeTurret2;



	public int turretsRemaining;
	public bool shieldUp;
	// Use this for initialization
	void Start () {
		shieldUp = true;
		turretsRemaining = 9;
		uiMaster = GameObject.Find ("UI").GetComponent<UIMaster> ();
		pauseMenu = GameObject.Find ("UI").transform.FindChild ("PauseMenu").gameObject;
		paused = false;
		if (SceneManager.GetActiveScene ().name == "Main") {
			bubbleshield = GameObject.Find ("ForceField").gameObject;
			TitanTurret = GameObject.Find ("TitanTurret").gameObject;
			LargeTurret0 = GameObject.Find ("LargeTurret (0)").gameObject;
			LargeTurret1 = GameObject.Find ("LargeTurret (1)").gameObject;
			LargeTurret2 = GameObject.Find ("LargeTurret (2)").gameObject;
		}
	}

	// Update is called once per frame
	void Update () {
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
	}

	public void TurretDied(){
		turretsRemaining--;
		uiMaster.updateTurretsRemaining (turretsRemaining);
		checkShield ();
	}
	public void checkShield(){
		if (turretsRemaining <= 3 && shieldUp)
			lowerShield ();
		
		if (turretsRemaining == 0) {

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
	}


	public void Victory(){

	}

	public void Defeat(){

	}


}
