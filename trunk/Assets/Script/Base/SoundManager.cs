using UnityEngine;
using System.Collections;

public class SoundManager : SingletonMono <SoundManager> {

	public AudioClip sndHorn;
	public AudioClip sndIdle;
	public AudioClip sndStartAndIdle;
	public AudioClip sndCrash;

	void Start () {}
	void Update () {}

	public void PlayHorn () {
		audio.PlayOneShot (sndHorn);
	}

	public void PlayStart () {
		audio.PlayOneShot (sndStartAndIdle);
	}

	public void PlayIdle () {
		audio.PlayOneShot (sndIdle);
	}

	public void PlayCrash () {
		audio.PlayOneShot (sndCrash);
	}
}
