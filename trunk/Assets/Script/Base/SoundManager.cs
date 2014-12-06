﻿using UnityEngine;
using System.Collections;

public class SoundManager : SingletonMono <SoundManager> {

	public AudioClip sndHorn;
	public AudioClip sndCrash;
	public AudioClip sndOutOfTime;

	void Start () {}
	void Update () {}

	public void PlayHorn () {
		audio.PlayOneShot (sndHorn);
	}

	public void PlayCrash () {
		audio.PlayOneShot (sndCrash);
	}

	public void PlayOutOfTime () {
		audio.PlayOneShot (sndOutOfTime);
	}
}
