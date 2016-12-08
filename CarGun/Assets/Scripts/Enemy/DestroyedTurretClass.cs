using UnityEngine;
using System.Collections;

public class DestroyedTurretClass : MonoBehaviour {
	public void destroySelf(float num){
		Destroy (transform.FindChild ("TurretMesh").gameObject, num);
		Destroy (transform.FindChild ("Mesh1").gameObject, num);
		Destroy (transform.FindChild ("Mesh2").gameObject, num);
		Destroy (transform.FindChild ("Mesh3").gameObject, num);

	}
}
