using UnityEngine;
using System.Collections;

public class RocketControl : MonoBehaviour {
	private GameObject target;
	public float speed;
	public float timeToHit;
	public float rotSpeed;
	public float existTime;
	public float randomValue = 25f;

	private Vector3 distance;
	private float magnitude;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Car").gameObject;

		randomValue = Random.Range (-randomValue, randomValue);
		magnitude = Vector3.Distance (target.transform.position, this.transform.position) + (randomValue) - 25f;

		timeToHit = magnitude / speed;
		rotSpeed = 24f / (timeToHit);
		Destroy (gameObject, existTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(speed* Time.deltaTime * Vector3.forward);
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x + rotSpeed*Time.deltaTime, transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
