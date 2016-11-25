using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	


	public Texture2D crosshairTexture; 
	public Rect position; 
	static bool OriginalOn = true;

	public float size = 8;
	public float yOffset = 90;




	void Start() 
	{
		position = new Rect((Screen.width - crosshairTexture.width/size) / 2, (Screen.height - crosshairTexture.height/size) /2 - yOffset, crosshairTexture.width/size, crosshairTexture.height/size);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false; 

	}

	void Update () { 
	} 


	void OnGUI() 
	{
		if(OriginalOn == true) 
			GUI.DrawTexture(position, crosshairTexture); 
	}
}