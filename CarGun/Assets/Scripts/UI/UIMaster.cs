using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMaster : MonoBehaviour {


	protected GameObject HealthBar; //access to the health bar
	protected GameObject HPImg;
	protected float leftMost;

	protected Text remainingText;

	protected Text ammoText;


	// Use this for initialization
	void Start () {
		HealthBar = transform.FindChild ("HealthBar").gameObject;
		HPImg = HealthBar.transform.GetChild (0).gameObject;
		leftMost = HealthBar.transform.GetChild (1).GetComponent<RectTransform> ().localPosition.x;

		remainingText = transform.FindChild ("TurretGroup").transform.FindChild ("TurretCount").gameObject.GetComponent<Text> ();
		ammoText = transform.FindChild ("AmmoImage").transform.FindChild ("AmmoText").gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateHP(float health, float maxHealth){
		float percentage = health / maxHealth;
		//Debug.Log (0.992 * percentage);
		Vector3 newScale = new Vector3 (0.992f * percentage, 0.90f, 1);
		HPImg.GetComponent<RectTransform> ().localScale = newScale; //scaling it.
		Vector3 newLoc = new Vector3 (leftMost + -leftMost * percentage, 0, 0);
		//leftMost + -leftMost*percentage //456.45
		HPImg.GetComponent<RectTransform> ().localPosition = newLoc;

		if (percentage > 0.50f)
			HPImg.GetComponent<RawImage> ().color = Color.Lerp (Color.green, Color.yellow, (maxHealth - health) / (maxHealth / 2));
		//i.e. @ 75hp, 100 - 75 = 25, divided by 50 gives you 0.5
		else if (percentage <= 0.50f)
			HPImg.GetComponent<RawImage> ().color = Color.Lerp (Color.yellow, Color.red, (maxHealth/2 - health) / (maxHealth / 2));
	}

	public void updateAmmo(int curAmmo, int maxAmmo){
		ammoText.text = "Ammo \n " + curAmmo.ToString () + "/" + maxAmmo.ToString ();
	}

	public void updateTurretsRemaining(int remaining){
		remainingText.text = "x" + remaining.ToString ();

	}
}
