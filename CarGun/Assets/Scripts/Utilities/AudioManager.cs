using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
	private List<AudioSource> sources = new List<AudioSource>();

	public static AudioManager instance;

	public static AudioManager Instance{
		get {
			return instance;
		}
	}
	public float volumeSnd;

	[SerializeField] private AudioClip BGM_Menu;
	[SerializeField] private AudioClip BGM_Western;

	[SerializeField] private AudioClip BGM_Boss;

	[SerializeField] private AudioClip SE_BarrelFire;
	[SerializeField] private AudioClip SE_MineExplode;

	//private AudioSource source; //SE
	private AudioSource bgm; //BGM

	public void PlayBGM (AudioClip clip){
		Debug.Log ("playing new bgm");
		bgm.clip = clip;
		bgm.Play ();
	}

	public void PlayBossBGM(){
		bgm.clip = BGM_Boss;
		bgm.Play ();
	}

	public void PlaySE_BarrelFire(){
		PlaySound (SE_BarrelFire, 0.1f);
	}

	public void PlaySE_MineExplode(){
		PlaySound (SE_MineExplode, 0.1f);
	}


	void Awake() //see if one existed before
	{
		/*if (instance !=null)
		{
			if (instance != this)
			{
				Destroy (this.gameObject);
			}
		}
		else
		{
			instance= this;
			DontDestroyOnLoad(this);
		}*/
		bgm = transform.FindChild ("AudioBGM").GetComponent<AudioSource> ();
		//source = transform.FindChild("AudioSE").GetComponent<AudioSource> ();
		bgm.loop = true;

		if (SceneManager.GetActiveScene().name == "Menu")
			PlayBGM (BGM_Menu);

		if (SceneManager.GetActiveScene().name == "Main")
			PlayBGM (BGM_Western);

		if (SceneManager.GetActiveScene ().name == "TestingChamber")
			PlayBGM (BGM_Menu);
	}

	void FixedUpdate(){

	}

	public void PlaySound(AudioClip clip)
	{
		AudioSource source = GetAvailableSource ();
		source.clip = clip;
		source.volume = volumeSnd;
		source.Play ();
	}

	public void PlaySound(AudioClip clip, float volume)
	{
		AudioSource source = GetAvailableSource ();
		source.clip = clip;
		source.volume = volume;
		source.Play ();
	}

	/*private AudioSource GetAudioSource(){ //adding new audio source
		if (source == null)
		{
			source = this.gameObject.AddComponent<AudioSource> ();
			this.sources.Add(source);
		}
		return source;
	}*/

	[SerializeField] private int maxAudioSourceCount = 20;

	private AudioSource GetAvailableSource(){
		if (this.sources == null) {
			this.sources = new List<AudioSource> ();
			Debug.Log ("adding new source");
		}

		if (this.sources.Count == 0) {
			AudioSource firstSource = this.gameObject.AddComponent<AudioSource> ();
			this.sources.Add (firstSource);
			firstSource.volume = volumeSnd;
		}

		for (int i = 0; i < this.sources.Count; i++) {
			AudioSource source = this.sources [i];
			if (source.isPlaying == false) {
				return source;
			}
		}
		if (this.sources.Count < this.maxAudioSourceCount) {
			AudioSource newSource = this.gameObject.AddComponent<AudioSource> ();
			this.sources.Add (newSource);
			newSource.volume = volumeSnd;
			Debug.Log ("adding new source");
			return newSource;
		}
		return null;
	}

}