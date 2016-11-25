using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonsScript : MonoBehaviour {

	private bool howToPlayVisible = false;

	public void GoToScene(string destination){
		Time.timeScale = 1;
		SceneManager.LoadScene (destination);
	}

	public void HowToPlay(){
		if (!howToPlayVisible) {
			transform.parent.FindChild ("HowToPlayImage").gameObject.SetActive (true);
			transform.parent.FindChild ("Title").gameObject.SetActive (false);
		} else {
			transform.parent.FindChild ("HowToPlayImage").gameObject.SetActive (false);
			transform.parent.FindChild ("Title").gameObject.SetActive (true);
		}
		howToPlayVisible = !howToPlayVisible;
	
	}

	public void quitGame(){ 
		Debug.Log ("quitting game");
		Application.Quit ();
	}
}
