using UnityEngine;
using System.Collections;


//this script is for controlling the main camera.
public class CameraControl : MonoBehaviour {

	public float camMoveSpeed = 50f;
	public float rotateVSpeed;
	public float rotateHSpeed;
	public float maxVSensitivity = 6;
	public float maxHSensitivity = 8;
	public float minSensitivity = 3.5f; //speed is divided by this

	private GameObject CamPoint;
	private GameObject target;
	private GameObject camControl;

	void Start() {
		CamPoint = GameObject.Find ("Car").transform.FindChild ("CamPoint").gameObject	;
		target = GameObject.Find ("Car").gameObject.transform.FindChild ("CameraPosition").gameObject;
		target.transform.parent = null;
		camControl = GameObject.Find("Main Camera").gameObject;
		camControl.transform.localPosition = target.transform.FindChild ("UnzoomedPos").gameObject.transform.localPosition;
	}

	void FixedUpdate() {
		if (transform.GetComponent<PlayerEntity> ().isAlive ()) {
			target.transform.position = Vector3.Lerp (transform.position, CamPoint.transform.position, camMoveSpeed * Time.deltaTime);



			if (Input.GetMouseButton (1)) {
				rotateHSpeed = maxHSensitivity / minSensitivity;
				rotateVSpeed = maxVSensitivity / minSensitivity;
				camControl.transform.position = target.transform.FindChild ("ZoomedPos").gameObject.transform.position;
			} else {
				rotateHSpeed = maxHSensitivity;
				rotateVSpeed = maxVSensitivity;
				camControl.transform.position = target.transform.FindChild ("UnzoomedPos").gameObject.transform.position;
			}

			//this is used for calcluating the camera control
			float horizontal = Input.GetAxis ("Mouse X") * rotateHSpeed; //rotating horizontally
			target.transform.Rotate (0, horizontal, 0); 
			float vertical = Input.GetAxis ("Mouse Y") * rotateVSpeed; //rotating vertically
			target.transform.Rotate (-vertical, 0, 0);
			Vector3 newPos = new Vector3 (target.transform.eulerAngles.x, target.transform.eulerAngles.y, 0); 
			//camera limitations
			if (target.transform.eulerAngles.x <= 355 && target.transform.eulerAngles.x >= 270) {//going under from behind car
				newPos = new Vector3 (355.001f, target.transform.eulerAngles.y, 0);
			} else if (target.transform.eulerAngles.x <= 270 && target.transform.eulerAngles.x >= 85) { //prevents going over car
				newPos = new Vector3 (84.999f, target.transform.eulerAngles.y, 0);
			}
			target.transform.eulerAngles = newPos; //setting new camera position



		} else {
			target.transform.parent = null;
		}
	}
}
