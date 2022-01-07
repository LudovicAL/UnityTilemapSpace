using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumBehavior : MonoBehaviour {

	[SerializeField]
	private float minTimeBetweenHums = 15.0f;
	[SerializeField]
	private float maxTimeBetweenHums = 25.0f;
	[SerializeField]
	private AudioClip[] humClips;
	private AudioSource audioSource;
	private float timeOfNextHum;

	private void Awake() {
		audioSource = this.GetComponent<AudioSource>();
		this.GetComponent<PlayerInput>().actions["Talk"].started += OnTalk;
	}

	// Start is called before the first frame update
	void Start() {
		SetTimeOfNextHum();
	}

	// Update is called once per frame
	void Update() {
		if (Time.time >= timeOfNextHum) {
			PlayRandomHum();
		}
    }

	//When a move action is triggered
	public void OnTalk(InputAction.CallbackContext context) {
		PlayRandomHum();
	}

	//Plays a random Hum
	public void PlayRandomHum() {
		AudioClip clipToPlay = humClips[Random.Range(0, humClips.Length)];
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
