using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwaperScript : MonoBehaviour {

    // Use this for initialization   
    public AudioClip bossMusic;
    private AudioSource musicOutput;
    public bool playBossMusic = false;
    private bool musicChanged = false;

	void Start () {
        musicOutput = GetComponent<AudioSource>();
	}

    IEnumerator swapMusic() {
        float originalVolume = musicOutput.volume;

        for (float f = originalVolume; f >= 0;f-= 0.01f) {
            musicOutput.volume = f;
            yield return new WaitForSeconds(.1f);
        }
        musicOutput.clip = bossMusic;
        musicOutput.volume = originalVolume;
        musicOutput.Play();
        musicChanged = true;
    }

	// Update is called once per frame
	void Update () {
        if (playBossMusic && !musicChanged) StartCoroutine("swapMusic");
	}
}
