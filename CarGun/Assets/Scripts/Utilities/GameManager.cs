using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	private GameObject pauseMenu;
	private bool paused;
	// Use this for initialization
	void Start () {
		pauseMenu = GameObject.Find ("UI").transform.FindChild ("PauseMenu").gameObject;
		paused = false;
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
}
