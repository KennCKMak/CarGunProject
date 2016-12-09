using UnityEngine;
using System.Collections;

public class RockslideClass : MonoBehaviour {
	public int rockCount;
	public GameObject[] boulders;
	public GameObject triggerPoint;
	private GameObject player;

	public float dist;

	// Use this for initialization
	void Start () {
		boulders = new GameObject[rockCount];
		for (int i = 0; i < rockCount; i++) {
			boulders [i] = transform.GetChild (i).gameObject;

		}


		triggerPoint = transform.FindChild ("TriggerPoint").gameObject;
		player = GameObject.Find ("Car").gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		if ((player.transform.position - triggerPoint.transform.position).magnitude < dist) {
			for (int i = 0; i < boulders.Length; i++) {
				boulders [i].SetActive (true);
			}
		}
	}
}
