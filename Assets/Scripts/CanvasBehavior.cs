using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBehavior : MonoBehaviour {

	public AudioClip boingAudioClip;
	public AudioClip zouAudioClip;
	private AudioSource canvasAudioSource;
	private AudioSource musicAudioSource;
	private GameObject aboutPanel;

    // Start is called before the first frame update
    void Start() {
		canvasAudioSource = this.GetComponent<AudioSource>();
		aboutPanel = transform.Find("Panel About").gameObject;
		musicAudioSource = transform.Find("Toggle Music").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			canvasAudioSource.clip = boingAudioClip;
			canvasAudioSource.Play();
		}
		aboutPanel.SetActive(Input.GetKey(KeyCode.Escape));
	}

	//When a URL button is clicked
	public void OnLinkButtonClick(string url) {
		canvasAudioSource.clip = zouAudioClip;
		canvasAudioSource.Play();
		Application.OpenURL(url);
	}

	//When the music toggle button is clicked
	public void OnMusicToggleChanged(Toggle toggle) {
		if (toggle.isOn) {
			if (!musicAudioSource.isPlaying) {
				musicAudioSource.Play();
			}
		} else {
			if (musicAudioSource.isPlaying) {
				musicAudioSource.Pause();
			}
		}
	}
}
