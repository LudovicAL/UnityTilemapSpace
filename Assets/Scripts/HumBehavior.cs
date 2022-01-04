using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumBehavior : MonoBehaviour {

	public float minTimeBetweenHums = 15.0f;
	public float maxTimeBetweenHums = 25.0f;
	public AudioClip[] humClips;
	private AudioSource audioSource;
	private float timeOfNextHum;

	// Start is called before the first frame update
	void Start() {
		audioSource = this.GetComponent<AudioSource>();
		SetTimeOfNextHum();
	}

	// Update is called once per frame
	void Update() {
		if (Time.time >= timeOfNextHum) {
			PlayRandomHum();
		}
    }

	//Plays a random Hum
	private void PlayRandomHum() {
		AudioClip clipToPlay = null;
		while (clipToPlay == audioSource.clip) {
			clipToPlay = humClips[Random.Range(0, humClips.Length)];
		}
		audioSource.clip = clipToPlay;
		audioSource.Play();
		SetTimeOfNextHum();
	}

	//Sets the time of the next hum
	private void SetTimeOfNextHum() {
		timeOfNextHum = Time.time + Random.Range(minTimeBetweenHums, maxTimeBetweenHums);
	}
}
