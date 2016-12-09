using UnityEngine;
using System.Collections;

public class DestroyedCarClass : MonoBehaviour {
	public GameObject Turret;
	// Use this for initialization
	void Start () {

		Turret = transform.FindChild ("CarTurret").transform.FindChild("Turret").gameObject;
	}
	
	public void turnTurret(GameObject newTurret){
		Turret = transform.FindChild ("CarTurret").transform.FindChild("Turret").gameObject;
		Turret.transform.position = newTurret.transform.position;
		Turret.transform.rotation = newTurret.transform.rotation;
	}
}
