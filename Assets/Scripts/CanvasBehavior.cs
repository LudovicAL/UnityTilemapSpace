using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBehavior : MonoBehaviour {

	[SerializeField]
	private AudioClip boingAudioClip;
	[SerializeField]
	private AudioClip zouAudioClip;
	private AudioSource canvasAudioSource;
	private AudioSource musicAudioSource;
	private GameObject aboutPanel;

	private void Awake() {
		canvasAudioSource = this.GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
	void Start() {
		aboutPanel = transform.Find("Panel About").gameObject;
		musicAudioSource = transform.Find("Toggle Music").GetComponent<AudioSource>();
		ShowAboutPanel(false);

	}

    // Update is called once per frame
    void Update() {
	}

	//Shows or hide the About panel
	public void ShowAboutPanel(bool value) {
		if (value && !aboutPanel.activeSelf) {
			canvasAudioSource.clip = boingAudioClip;
			canvasAudioSource.Play();
		}
		aboutPanel.SetActive(value);
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
