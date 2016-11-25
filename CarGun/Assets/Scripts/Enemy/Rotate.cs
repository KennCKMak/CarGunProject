using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public bool acting; //used to determine if it is rotation or not
	public float rotateSpeed; 
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		if(acting)
			this.transform.RotateAround (transform.position, new Vector3 (0, 1, 0), rotateSpeed * Time.deltaTime);
	}
}
