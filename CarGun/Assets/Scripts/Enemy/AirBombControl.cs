using UnityEngine;
using System.Collections;

public class AirBombControl : MonoBehaviour {
	public float fallSpeed;
	public float existTime;
	private float height;

	public float timeToHit;
	public float rotSpeed;
	// Use this for initialization
	void Start () {


		height = this.transform.position.y;
		timeToHit = height / fallSpeed;//seconds it takes to hit
		existTime = timeToHit + 5; //falls past screen for 5 seconds
		rotSpeed = 90f / (timeToHit);

		Destroy (gameObject, existTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//transform.Translate(speed* Time.deltaTime * Vector3.forward);
		if (transform.eulerAngles.x <= 89)
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x + rotSpeed*Time.deltaTime, transform.eulerAngles.y, transform.eulerAngles.z);
		else
			rotSpeed = 0;
	}
}
