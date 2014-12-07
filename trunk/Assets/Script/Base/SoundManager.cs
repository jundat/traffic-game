using UnityEngine;
using System.Collections;

public class SoundManager : SingletonMono <SoundManager> {

	public AudioClip sndHorn;
	public AudioClip sndCrash;
	public AudioClip sndStart;
	public AudioClip sndOutOfTime;
	public AudioClip sndCheckpoint;
	public AudioClip sndCompleteGame;
	public AudioClip sndClick;

	void Start () {}
	void Update () {}

	public void PlayStart () {
		audio.PlayOneShot (sndStart);
	}

	public void PlayHorn () {
		audio.PlayOneShot (sndHorn);
	}

	public void PlayCrash () {
		audio.PlayOneShot (sndCrash);
	}

	public void PlayOutOfTime () {
		audio.PlayOneShot (sndOutOfTime);
	}
	
	public void PlayCheckpoint () {
		audio.PlayOneShot (sndCheckpoint);
	}

	public void PlayCompleteGame () {
		audio.PlayOneShot (sndCompleteGame);
	}
	
	public void PlayClick () {
		audio.PlayOneShot (sndClick);
	}
}
