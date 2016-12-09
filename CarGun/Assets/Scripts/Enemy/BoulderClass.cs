using UnityEngine;
using System.Collections;

public class BoulderClass : MonoBehaviour {
	public float timer;
	public float damage;
	// Use this for initialization
	void Start () {
		if (timer == 0)
			timer = 20;
		Destroy (gameObject, timer);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
